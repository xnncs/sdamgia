using System.Reflection;
using Api.Mapping;
using Api.Validation;
using Api.Validation.Behaviors;
using Application.Extensions;
using FluentValidation;
using Infrastructure.Extensions;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Extensions;

namespace Api.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.ConfigureOptions(configuration);

        // adding mapping
        services.AddAutoMapper(typeof(AppMappingProfile));

        services.AddBusinessLogicServices();

        services.AddInfrastructureServices();

        services.AddDbContexts(configuration);
        services.AddPersistenceServices();
        

        // adding validation
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        services.AddApiAuthentication(configuration);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddLogging();

        return services;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        // adding middlewares
        app.UseCustomExceptionHandler();

        // adding swagger if app is in development mode
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseHttpsRedirection();

        app.AddMappedEndpoints();

        app.UseCookiePolicy(new CookiePolicyOptions()
        {
            MinimumSameSitePolicy = SameSiteMode.Strict,
            HttpOnly = HttpOnlyPolicy.Always,
            Secure = CookieSecurePolicy.Always
        });

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    private static IServiceCollection ConfigureOptions(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // configuring options 
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.Configure<PasswordHashOptions>(configuration.GetSection(nameof(PasswordHashOptions)));

        return services;
    }
}