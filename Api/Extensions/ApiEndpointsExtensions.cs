using Api.Endpoints;
using Api.Endpoints.DataEndpoints;
using Api.Endpoints.StaticDataEndpoints;

namespace Api.Extensions;

public static class ApiEndpointsExtensions
{
    public static void AddMappedEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAuthEndpoints();
        app.MapSchoolEndpoints();
        app.MapPostEndpoints();
        app.MapExamTaskEndpoints();

        app.MapSubjectsEndpoints();
    }
}