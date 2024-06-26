using System.Net;
using Api.Contracts;
using Api.Contracts.Requests;
using Api.Contracts.Requests.School;
using Api.Contracts.Responses;
using Application.Abstract;
using Application.Dto;
using Application.Dto.School;
using AutoMapper;
using Core.Models;
using Core.Structures;
using Infrastructure.Abstract;

namespace Api.Endpoints;

public static class SchoolEndpoints
{
    public static IEndpointRouteBuilder MapSchoolEndpoints(this IEndpointRouteBuilder app)
    {
        IEndpointRouteBuilder endpoints = app.MapGroup("api/school");
        
       
        // GET: api/school/get
        endpoints.MapGet("get", GetAsync);
        
        // POST: api/school/create
        endpoints.MapPost("create", CreateSchoolAsync);
        
        // PUT: api/school/update
        endpoints.MapPut("update", UpdateSchoolAsync);

        // DELETE: api/school/delete
        endpoints.MapDelete("delete", DeleteSchoolAsync);
        

        // POST: api/school/join/{schoolId}
        endpoints.MapPost("join/{schoolId}", JoinSchoolAsync);
        
        // POST: api/school/leave
        endpoints.MapPost("leave", LeaveSchoolAsync);
        
        
        
        return endpoints;
    }

    private static async Task<IResult> LeaveSchoolAsync(IAuthorizationService authorizationService, HttpContext context,
        ISchoolService schoolService)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        
        await schoolService.LeaveSchool(userId);

        return TypedResults.Ok();
    }
    
    private static async Task<IResult> UpdateSchoolAsync(UpdateSchoolRequest request, IAuthorizationService authorizationService, HttpContext context,
        ISchoolService schoolService, IMapper mapper)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        UpdateSchoolRequestDto contract = GenerateUpdateSchoolRequestDtoObject(request, userId, mapper);

        await schoolService.UpdateSchool(contract);

        return TypedResults.Ok();
    }

    private static async Task<IResult> DeleteSchoolAsync(IAuthorizationService authorizationService,
        HttpContext context, ISchoolService schoolService)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        
        DeleteSchoolRequestDto contract = DeleteSchoolRequestDto.Create(
            userId: userId);

        await schoolService.DeleteSchool(contract);

        return TypedResults.Ok();
    }
    
    private static async Task<IResult> GetAsync(IAuthorizationService authorizationService, HttpContext context,
        ISchoolService schoolService, IMapper mapper)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        School? school = await schoolService.GetByUserIdAsync(userId);
        
        
        GetSchoolResponse schoolResponse = mapper.Map<School, GetSchoolResponse>(school);
        
        return TypedResults.Ok(schoolResponse);
    }
    
    private static async Task<IResult> JoinSchoolAsync(int schoolId, IAuthorizationService authorizationService, HttpContext context,
        ISchoolService schoolService)
    {
        int authorId = authorizationService.GetUserIdFromJwt(context);
        JoinSchoolRequestDto contract = new JoinSchoolRequestDto(SchoolId: schoolId, UserId: authorId);

        await schoolService.JoinSchoolAsync(contract);

        return TypedResults.Ok();
    }
    private static async Task<IResult> CreateSchoolAsync(CreateSchoolRequest request, IMapper mapper, IAuthorizationService authorizationService,
        HttpContext context, IJwtProvider provider, ISchoolService schoolService)
    {
        int authorId = authorizationService.GetUserIdFromJwt(context);
        CreateSchoolRequestDto contract = GenerateCreateSchoolRequestDtoObject(request, authorId, mapper);

        await schoolService.CreateSchoolAsync(contract);

        return TypedResults.Ok();
    }
    
    private static CreateSchoolRequestDto GenerateCreateSchoolRequestDtoObject(CreateSchoolRequest request, int authorId, IMapper mapper)
    {
        CreateSchoolRequestDto contract = mapper.Map<CreateSchoolRequest, CreateSchoolRequestDto>(request);
        contract.AuthorId = authorId;

        return contract;
    }
    
    private static UpdateSchoolRequestDto GenerateUpdateSchoolRequestDtoObject(UpdateSchoolRequest request, int userId,
        IMapper mapper)
    {
        UpdateSchoolRequestDto contract = mapper.Map<UpdateSchoolRequest, UpdateSchoolRequestDto>(request);
        contract.UserId = userId;

        return contract;
    }
}