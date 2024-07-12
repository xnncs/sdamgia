using Api.Contracts.Requests.ExamTask;
using Application.Abstract;
using Application.Dto.ExamTask;
using AutoMapper;

namespace Api.Endpoints.DataEndpoints;

public static class ExamTaskEndpoints
{
    public static IEndpointRouteBuilder MapExamTaskEndpoints(this IEndpointRouteBuilder app)
    {
        IEndpointRouteBuilder endpoints = app.MapGroup("api/tasks");

        // POST: api/tasks/create
        endpoints.MapPost("create", CreateExamTaskAsync);
        
        
        return endpoints;
    }

    private static async Task CreateExamTaskAsync(CreateExamTaskRequest request, IExamTaskService examTaskService, 
        IMapper mapper, IAuthorizationService authorizationService, HttpContext httpContext)
    {
        int userId = authorizationService.GetUserIdFromJwt(httpContext);

        CreateExamTaskDto contract = GenerateCreateExamTaskRequestDtoObject(request, userId, mapper);

        await examTaskService.CreateExamTaskAsync(contract);
    }

    private static CreateExamTaskDto GenerateCreateExamTaskRequestDtoObject(CreateExamTaskRequest request, int userId,
        IMapper mapper)
    {
        CreateExamTaskDto contract = mapper.Map<CreateExamTaskRequest, CreateExamTaskDto>(request);
        contract.UserId = userId;

        return contract;
    }
}