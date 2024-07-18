using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Abstract;
using Persistence.Database;
using Persistence.Repositories;

namespace Persistence.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        // adding repositories
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddScoped<ISchoolRepository, SchoolRepository>();

        services.AddScoped<IPostRepository, PostRepository>();

        services.AddScoped<IExamTaskRepository, ExamTaskRepository>();

        services.AddScoped<ISubjectRepository, SubjectRepository>();

        return services;
    }

    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfigurationManager configuration)
    {
        // adding db contexts
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            string connectionString = configuration.GetConnectionString(nameof(ApplicationDbContext))
                                       ?? throw new Exception("Connection string does not exist");

            options.UseNpgsql(connectionString);
        });

        return services;
    }
}