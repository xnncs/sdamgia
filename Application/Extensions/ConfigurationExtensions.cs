using Application.Abstract;
using Application.Helpers;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureBusinessLogicServices(this IServiceCollection services)
    {
        // adding business logic services 
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<ISchoolService, SchoolService>();
        
        services.AddScoped<IExamTaskService, ExamTaskService>();

        services.AddScoped<ISchoolPermissionsHelper, SchoolPermissionsHelper>();
        
        return services;
    }
}