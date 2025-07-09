using HttpInspector.Dtos;
using HttpInspector.Handlers;

namespace HttpInspector.Endpoints;

public static class AboutEndpoints
{
    public static void MapAbout(this IEndpointRouteBuilder routes) =>
        routes.MapGet("/about", AboutHandlers.About)
            .Produces<AboutDto>()
            .WithDisplayName("About")
            .WithSummary("About")
            .WithName("About")
            .WithDescription("Returns information about the application");
}