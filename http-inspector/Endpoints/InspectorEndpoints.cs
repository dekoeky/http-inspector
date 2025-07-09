using HttpInspector.Dtos;
using HttpInspector.Handlers;

namespace HttpInspector.Endpoints;

public static class InspectorEndpoints
{
    public static void MapInspector(this IEndpointRouteBuilder routes)
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
}