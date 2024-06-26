using Application.Dto;
using Application.Dto.Auth;
using Infrastructure.Abstract;
using Microsoft.AspNetCore.Http;

namespace Application.Abstract;

public interface IAuthorizationService
{
    Task RegisterAsync(RegisterUserRequestDto request);
    
    // returns jwt token
    Task<string> LoginAsync(LoginUserRequestDto request);
    
    int GetUserIdFromJwt(HttpContext httpContext);
}
