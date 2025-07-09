using HttpInspector.Handlers;

namespace HttpInspector.Endpoints;

public static class BrowseEndpoints
{
    public static void MapBrowseEndpoints(this IEndpointRouteBuilder routes) =>
        routes.MapGet("/endpoints", BrowseHandlers.BrowseMappedEndpoints)
            .WithName("BrowseEndpoints")
            .WithDescription("Lists all mapped endpoints")
            .WithSummary("Browse Endpoints");
}