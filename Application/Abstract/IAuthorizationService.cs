using Application.Dto;
using Application.Dto.Auth;
using Infrastructure.Abstract;
using Microsoft.AspNetCore.Http;

namespace Application.Abstract;

public interface IAuthorizationService
{
    Task RegisterAsync(RegisterUserDto request);
    
    // returns jwt token
    Task<string> LoginAsync(LoginUserDto request);
    
    int GetUserIdFromJwt(HttpContext httpContext);
}
