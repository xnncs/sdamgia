using Api.Contracts.Requests.Subject;
using Application.Abstract.StaticDataServices;
using Application.Dto.Subject;
using AutoMapper;
using Core.StaticInfoModels;

namespace Api.Endpoints.StaticDataEndpoints;

public static class SubjectEndpoints
{
    public static IEndpointRouteBuilder MapSubjectsEndpoints(this IEndpointRouteBuilder app)
    {
        IEndpointRouteBuilder endpoints = app.MapGroup("api/subjects");

        endpoints.MapGet("get", GetAllAsync);

        endpoints.MapPost("create", CreateAsync);
        
        return endpoints;
    }

    private static async Task CreateAsync(CreateSubjectRequest request, ISubjectService subjectService, IMapper mapper)
    {
        CreateSubjectDto contract = mapper.Map<CreateSubjectRequest, CreateSubjectDto>(request);

        await subjectService.CreateAsync(contract);
    }

    private static async Task<IEnumerable<Subject>> GetAllAsync(ISubjectService subjectService)
    {
        return await subjectService.GetAllAsync();
    }
}