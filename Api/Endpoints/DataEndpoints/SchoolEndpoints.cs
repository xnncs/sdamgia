using Api.Contracts.Requests.School;
using Api.Contracts.Responses;
using Application.Abstract;
using Application.Dto.School;
using AutoMapper;
using Core.Models;
using Infrastructure.Abstract;

namespace Api.Endpoints.DataEndpoints;

public static class SchoolEndpoints
{
    public static IEndpointRouteBuilder MapSchoolEndpoints(this IEndpointRouteBuilder app)
    {
        IEndpointRouteBuilder endpoints = app.MapGroup("api/schools");
        
       
        // GET: api/schools/get
        endpoints.MapGet("get", GetAsync);
        
        // POST: api/schools/create
        endpoints.MapPost("create", CreateSchoolAsync);
        
        // PUT: api/schools/update
        endpoints.MapPut("update", UpdateSchoolAsync);

        // DELETE: api/schools/delete
        endpoints.MapDelete("delete", DeleteSchoolAsync);
        

        // POST: api/schools/join/{schoolId}
        endpoints.MapPost("join/{schoolId}", JoinSchoolAsync);
        
        // POST: api/schools/leave
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
        UpdateSchoolDto contract = GenerateUpdateSchoolRequestDtoObject(request, userId, mapper);

        await schoolService.UpdateSchool(contract);

        return TypedResults.Ok();
    }

    private static async Task<IResult> DeleteSchoolAsync(IAuthorizationService authorizationService,
        HttpContext context, ISchoolService schoolService)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        
        DeleteSchoolDto contract = DeleteSchoolDto.Create(
            userId: userId);

        await schoolService.DeleteSchool(contract);

        return TypedResults.Ok();
    }
    
    private static async Task<IResult> GetAsync(IAuthorizationService authorizationService, HttpContext context,
        ISchoolService schoolService, IMapper mapper)
    {
        int userId = authorizationService.GetUserIdFromJwt(context);
        School? school = await schoolService.GetByUserIdAsync(userId);
        if (school is null)
        {
            throw new Exception("You have no school");
        }

        GetSchoolResponse schoolResponse = GenerateGetSchoolResponseObject(school, mapper);
        
        return TypedResults.Ok(schoolResponse);
    }
    
    private static async Task<IResult> JoinSchoolAsync(int schoolId, IAuthorizationService authorizationService, HttpContext context,
        ISchoolService schoolService)
    {
        int authorId = authorizationService.GetUserIdFromJwt(context);
        JoinSchoolDto contract = new JoinSchoolDto(SchoolId: schoolId, UserId: authorId);

        await schoolService.JoinSchoolAsync(contract);

        return TypedResults.Ok();
    }
    private static async Task<IResult> CreateSchoolAsync(CreateSchoolRequest request, IMapper mapper, IAuthorizationService authorizationService,
        HttpContext context, IJwtProvider provider, ISchoolService schoolService)
    {
        int authorId = authorizationService.GetUserIdFromJwt(context);
        CreateSchoolDto contract = GenerateCreateSchoolRequestDtoObject(request, authorId, mapper);

        await schoolService.CreateSchoolAsync(contract);

        return TypedResults.Ok();
    }
    
    private static CreateSchoolDto GenerateCreateSchoolRequestDtoObject(CreateSchoolRequest request, int authorId, IMapper mapper)
    {
        CreateSchoolDto contract = mapper.Map<CreateSchoolRequest, CreateSchoolDto>(request);
        contract.AuthorId = authorId;

        return contract;
    }
    
    private static UpdateSchoolDto GenerateUpdateSchoolRequestDtoObject(UpdateSchoolRequest request, int userId,
        IMapper mapper)
    {
        UpdateSchoolDto contract = mapper.Map<UpdateSchoolRequest, UpdateSchoolDto>(request);
        contract.UserId = userId;

        return contract;
    }

    private static GetSchoolResponse GenerateGetSchoolResponseObject(School school, IMapper mapper)
    {
        GetSchoolResponse response = mapper.Map<School, GetSchoolResponse>(school);
        response.SubjectName = school.Subject.Name;
        response.StudentsNumber = school.Students.Count;
        response.AuthorId = school.Author.Id ?? -1;

        return response;
    }
}