using Api.Contracts.Requests.ExamTask;
using Api.Contracts.Responses.ExamTask;
using Application.Abstract;
using Application.Dto.ExamTask;
using AutoMapper;
using Core.Models;

namespace Api.Endpoints.DataEndpoints;

public static class ExamTaskEndpoints
{
    public static IEndpointRouteBuilder MapExamTaskEndpoints(this IEndpointRouteBuilder app)
    {
        IEndpointRouteBuilder endpoints = app.MapGroup("api/tasks");

        // POST: api/tasks/create
        endpoints.MapPost("create", CreateExamTaskAsync);

        endpoints.MapGet("get", GetAllAsync);

        endpoints.MapGet("get/{id:int}", GetByIdAsync);
        
        endpoints.MapPut("update", UpdateAsync);

        endpoints.MapDelete("delete/{id:int}", DeleteAsync);
        
        
        return endpoints;
    }

    private static async Task UpdateAsync(UpdateExamTaskRequest request, IExamTaskService examTaskService, 
        IAuthorizationService authorizationService, HttpContext httpContext, IMapper mapper)
    {
        int clientId = authorizationService.GetUserIdFromJwt(httpContext);
        UpdateExamTaskDto contract = GenerateUpdateExamTaskDtoObject(request, clientId, mapper);

        await examTaskService.UpdateAsync(contract);
    }

    private static async Task DeleteAsync(int id, IExamTaskService examTaskService, 
        IAuthorizationService authorizationService, HttpContext httpContext)
    {
        int clientId = authorizationService.GetUserIdFromJwt(httpContext);
        DeleteExamTaskDto contract = new DeleteExamTaskDto(clientId: clientId, examTaskId: id);

        await examTaskService.DeleteAsync(contract);
    }

    private static async Task<GetExamTaskResponse?> GetByIdAsync(int id, IExamTaskService examTaskService, 
        IMapper mapper)
    {
        ExamTask examTask = await examTaskService.GetByIdAsync(id)
            ?? throw new Exception("No such a exam task with this id");

        return GenerateGetExamTaskResponseObject(examTask, mapper);
    }

    private static async Task<IEnumerable<GetExamTaskResponse>> GetAllAsync(IExamTaskService examTaskService, IMapper mapper)
    {
        IEnumerable<ExamTask> examTasks = await examTaskService.GetAllAsync();
         
        return examTasks.Select(task => GenerateGetExamTaskResponseObject(task, mapper));
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

    private static GetExamTaskResponse GenerateGetExamTaskResponseObject(ExamTask model, IMapper mapper)
    {
        GetExamTaskResponse response = mapper.Map<ExamTask, GetExamTaskResponse>(model);
        response.SubjectName = model.Subject.Name;
        response.AuthorId = model.Author.Id ?? -1;

        return response;
    }

    private static UpdateExamTaskDto GenerateUpdateExamTaskDtoObject(UpdateExamTaskRequest model, int clientId,
        IMapper mapper)
    {
        UpdateExamTaskDto dto = mapper.Map<UpdateExamTaskRequest, UpdateExamTaskDto>(model);
        dto.ClientId = clientId;

        return dto;
    }
}