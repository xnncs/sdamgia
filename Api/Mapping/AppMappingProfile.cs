using Api.Contracts.Requests;
using Api.Contracts.Responses;
using Api.Contracts.Responses.ResponseHelpingModels;
using Application.Dto;
using AutoMapper;
using Core.Models;
using Persistence.Entities;
using Persistence.Models;

namespace Api.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
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


        // requests to requestsDto
        CreateMap<LoginUserRequest, LoginUserRequestDto>();
        CreateMap<RegisterUserRequest, RegisterUserRequestDto>();

        CreateMap<CreateSchoolRequest, CreateSchoolRequestDto>();

        CreateMap<UpdateSchoolRequest, UpdateSchoolRequestDto>();

        CreateMap<CreatePostRequest, CreatePostRequestDto>();
        CreateMap<EditPostRequest, EditPostRequestDto>();


        CreateMap<UpdateSchoolRequestDto, SchoolUpdatingModel>();
        
        
        // responses
        CreateMap<School, GetSchoolResponse>();

        CreateMap<Page, PageHelperResponseModel>();
    }
}