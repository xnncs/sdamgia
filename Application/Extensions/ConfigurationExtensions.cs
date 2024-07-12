using Application.Abstract;
using Application.Abstract.StaticDataServices;
using Application.Helpers;
using Application.Services;
using Application.Services.StaticDataServices;
using Core.StaticInfoModels;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
    {
        // adding business logic services 
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<ISchoolService, SchoolService>();
        
        services.AddScoped<IExamTaskService, ExamTaskService>();

        services.AddScoped<ISchoolPermissionsHelper, SchoolPermissionsHelper>();

        services.AddScoped<ISubjectService, SubjectService>();
        
        return services;
    }
}