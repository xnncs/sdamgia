using Application.Abstract;
using Application.Dto;
using Application.Dto.Auth;
using Core.Exceptions;
using Core.Models;
using Core.Structures;
using Infrastructure.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence.Abstract;

namespace Application.Services;

public class AuthorizationService : IAuthorizationService
{
    public AuthorizationService(IPasswordHasher passwordHasher, IUserRepository userRepository, IJwtProvider jwtProvider, 
        IStudentRepository studentRepository, ITeacherRepository teacherRepository)
    {
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
        
        _userRepository = userRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
    }
    
    
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    
    private readonly IUserRepository _userRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    
    public async Task RegisterAsync(RegisterUserRequestDto request)
    {
        if (await _userRepository.ContainsByLoginAsync(request.Email))
        {
            throw new DuplicateException();
        }
        
        string passwordHash = _passwordHasher.HashPassword(request.Password);
        User user = User.Create(request.Email, passwordHash, request.Username, request.FirstName, request.LastName, request.Role);
        
        // adds user to Students/Teachers belongs on its roles
        await AddAsRoleEntity(user);
    }

    private async Task AddAsRoleEntity(User user)
    {
        if (user.Roles.Contains(Roles.Student))
        {
            Student student = Student.CreateBasedOnUserAndModifyIt(user);
            
            await _studentRepository.AddAsync(student);
        }

        if (user.Roles.Contains(Roles.Teacher))
        {
            Teacher teacher = Teacher.CreateBasedOnUserAndModifyIt(user);

            await _teacherRepository.AddAsync(teacher);
        }
    }

    public async Task<string> LoginAsync(LoginUserRequestDto request)
    {
        User user = await _userRepository.GetByLoginAsync(request.Email) ?? throw new NoSuchAccountException();

        PasswordVerificationResult state = _passwordHasher.VerifyHashedPassword(
            providedPassword: request.Password,
            hashedPassword: user.PasswordHash);
        
        if (state == PasswordVerificationResult.Failed)
        {
            throw new IncorrectPasswordException();
        }
            
        return _jwtProvider.GenerateToken(user);
    }

    public int GetUserIdFromJwt(HttpContext httpContext)
    {
        string jwtTokenFromCookies = httpContext.Request.Cookies["tasty-cookies"]
                                     ?? throw new JwtAuthenticationException();

        string stringResult = _jwtProvider.GetIdFromClaims(jwtTokenFromCookies);
        
        bool state = Int32.TryParse(stringResult, out var value);
        if (!state)
        {
            throw new Exception("failed to convert jwt cookies value to int");
        }

        return value;
    }
}