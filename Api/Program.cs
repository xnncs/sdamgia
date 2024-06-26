using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.ConfigureDependencyInjection(configuration);

var app = builder.Build();

app.ConfigureApplication();

app.Run();

