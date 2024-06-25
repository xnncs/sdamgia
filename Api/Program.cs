using System.Formats.Asn1;
using System.Reflection;
using Api.Extensions;
using Api.Mapping;
using Api.Validation;
using Api.Validation.Behaviors;
using Application.Abstract;
using Application.Services;
using MediatR;
using FluentValidation;
using Infrastructure.Abstract;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Abstract;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


// configuring options 
services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
services.Configure<PasswordHashOptions>(configuration.GetSection(nameof(PasswordHashOptions)));


// adding mapping
services.AddAutoMapper(typeof(AppMappingProfile));


// adding helpers 
services.AddSingleton<IPasswordHasher, PasswordHasher>();
services.AddSingleton<IJwtProvider, JwtProvider>();


// adding business logic services 
services.AddScoped<IAuthorizationService, AuthorizationService>();
services.AddScoped<ISchoolService, SchoolService>();


// adding repositories
services.AddScoped<IUserRepository, UserRepository>();

services.AddScoped<ITeacherRepository, TeacherRepository>();
services.AddScoped<IStudentRepository, StudentRepository>();

services.AddScoped<ISchoolRepository, SchoolRepository>();


// adding db contexts
services.AddDbContext<ApplicationDbContext>(options =>
{
    string? connectionString = configuration.GetConnectionString(nameof(ApplicationDbContext))
        ?? throw new Exception("Connection string does not exist");

    options.UseNpgsql(connectionString);
});


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


var app = builder.Build();

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

app.Run();

