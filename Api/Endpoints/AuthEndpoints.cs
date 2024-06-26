using Api.Contracts;
using Api.Contracts.Requests;
using Api.Contracts.Requests.Auth;
using Application.Abstract;
using Application.Dto;
using Application.Dto.Auth;
using AutoMapper;
using Core.Structures;

namespace Api.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        IEndpointRouteBuilder endpoints = app.MapGroup("api/auth");
        
        // POST: api/auth/register
        endpoints.MapPost("register", RegisterAsync);
        
        // POST: api/auth/login
        endpoints.MapPost("login", LoginAsync);

        return endpoints;
    }
    
    private static async Task<IResult> RegisterAsync(RegisterUserRequest request, IAuthorizationService authorizationService, IMapper mapper)
    {
        RegisterUserRequestDto contract = CreateRegisterRequestContract(request, mapper);

        await authorizationService.RegisterAsync(contract);
        
        return TypedResults.Ok();
    }

    private static RegisterUserRequestDto CreateRegisterRequestContract(RegisterUserRequest request, IMapper mapper)
    {
        RegisterUserRequestDto contract = mapper.Map<RegisterUserRequest, RegisterUserRequestDto>(request);
        contract.Role = (Roles)request.RoleStudentOrTeacher;
        
        return contract;
    }
    
    private static async Task<IResult> LoginAsync(LoginUserRequest request, IAuthorizationService authorizationService, HttpContext context, IMapper mapper)
    {
        LoginUserRequestDto contract = mapper.Map<LoginUserRequest, LoginUserRequestDto>(request);
        
        string token = await authorizationService.LoginAsync(contract);
        context.Response.Cookies.Append("tasty-cookies", token);
        
        return TypedResults.Ok();
    }
}