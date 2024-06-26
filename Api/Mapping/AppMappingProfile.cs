using Api.Contracts.Requests;
using Api.Contracts.Requests.Auth;
using Api.Contracts.Requests.ExamTask;
using Api.Contracts.Requests.Post;
using Api.Contracts.Requests.School;
using Api.Contracts.Responses;
using Api.Contracts.Responses.ResponseHelpingModels;
using Application.Dto;
using Application.Dto.Auth;
using Application.Dto.ExamTask;
using Application.Dto.Post;
using Application.Dto.School;
using AutoMapper;
using Core.Models;
using Persistence.Entities;
using Persistence.Models;

namespace Api.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateEntityToModelMaps();

        CreateRequestToRequestDtoMaps();
        
        CreateResponsesMaps();

        CreateAllAnotherMaps();
    }

    private void CreateEntityToModelMaps()
    {
        // entity to model
        CreateMap<UserEntity, User>().ReverseMap();

        CreateMap<StudentEntity, Student>().ReverseMap();
        CreateMap<TeacherEntity, Teacher>().ReverseMap();
        
        CreateMap<SchoolEntity, School>().ReverseMap();
        
        CreateMap<PostEntity, Post>().ReverseMap();
        CreateMap<PageEntity, Page>().ReverseMap();
        
        CreateMap<ExamTaskEntity, ExamTask>().ReverseMap();
        CreateMap<ExamOptionEntity, ExamOption>().ReverseMap();
    }

    private void CreateRequestToRequestDtoMaps()
    {
        // requests to requestsDto
        CreateMap<LoginUserRequest, LoginUserRequestDto>();
        CreateMap<RegisterUserRequest, RegisterUserRequestDto>();

        CreateMap<CreateSchoolRequest, CreateSchoolRequestDto>();

        CreateMap<UpdateSchoolRequest, UpdateSchoolRequestDto>();

        CreateMap<CreatePostRequest, CreatePostRequestDto>();
        CreateMap<EditPostRequest, EditPostRequestDto>();
        
        CreateMap<CreateExamTaskRequest, CreateExamTaskRequestDto>();
    }

    private void CreateResponsesMaps()
    {
        // responses
        CreateMap<School, GetSchoolResponse>();

        CreateMap<Page, PageHelperResponseModel>();
    }

    private void CreateAllAnotherMaps()
    {
        CreateMap<UpdateSchoolRequestDto, SchoolUpdatingModel>();
    }
}