using HttpInspector.Handlers;

namespace HttpInspector.Endpoints;

public static class BrowseEndpoints
{
    public static void MapBrowseEndpoints(this IEndpointRouteBuilder routes) =>
        routes.MapGet(Patterns.Browse, BrowseHandlers.BrowseMappedEndpoints)
            .WithName("BrowseEndpoints")
            .WithDescription("Lists all mapped endpoints")
            .WithSummary("Browse Endpoints")
            .WithDisplayName("Endpoint Browser");
}