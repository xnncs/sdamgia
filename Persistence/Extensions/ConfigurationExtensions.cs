using Microsoft.Extensions.DependencyInjection;
using Persistence.Abstract;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services)
    {
        // adding repositories
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<ISchoolRepository, SchoolRepository>();

        services.AddScoped<IPostRepository, PostRepository>();

        services.AddScoped<IExamTaskRepository, ExamTaskRepository>();

        return services;
    }
}