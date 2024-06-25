using Api.Endpoints;

namespace Api.Extensions;

public static class ApiEndpointsExtensions
{
    public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapSchoolEndpoints();
        app.MapPostEndpoints();
    }
}