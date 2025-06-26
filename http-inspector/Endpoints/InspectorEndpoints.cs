using HttpInspector.Dtos;
using HttpInspector.Handlers;

namespace HttpInspector.Endpoints;

public static class InspectorEndpoints
{
    public static void MapInspectorEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.Map("/", InspectorHandlers.Inspect)
            .Produces<RequestInfoDto>()
            .WithDisplayName("Http-Inspect")
            .WithSummary("Http-Inspect")
            .WithName("Http-Inspect")
            .WithDescription("Returns information about the http request");

        //Map this handler again, for all other endpoints, in order to be able to debug with different paths
        routes.MapFallback(InspectorHandlers.Inspect);
    }

    public static void MapAboutEndpoint(this IEndpointRouteBuilder routes) =>
        routes.MapGet("/about", AboutHandlers.About)
            .Produces<AboutDto>()
            .WithDisplayName("About")
            .WithSummary("About")
            .WithName("About")
            .WithDescription("Returns information about the application");

    public static void MapBrowseEndpoints(this IEndpointRouteBuilder routes) =>
        routes.MapGet("/endpoints", EndpointInformationHandlers.ListMappedEndpoints)
            .WithName("BrowseEndpoints")
            .WithDescription("Lists all mapped endpoints")
            .WithSummary("Browse Endpoints");
}