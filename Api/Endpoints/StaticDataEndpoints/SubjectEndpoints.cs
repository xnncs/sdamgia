using System.Security.Cryptography;
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

        endpoints.MapPut("update", UpdateAsync);

        endpoints.MapDelete("delete", DeleteAsync);
        
        return endpoints;
    }

    private static async Task DeleteAsync(int id, ISubjectService subjectService)
    {
        await subjectService.DeleteAsync(id);
    }

    private static async Task UpdateAsync(UpdateSubjectRequest request, ISubjectService subjectService, IMapper mapper)
    {
        UpdateSubjectDto contract = mapper.Map<UpdateSubjectRequest, UpdateSubjectDto>(request);

        await subjectService.UpdateAsync(contract);
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