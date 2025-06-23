using HttpInspector.Dtos;
using HttpInspector.Handlers;

namespace HttpInspector.Endpoints;

public static class InspectorEndpoints
{
    public static void MapInspectorEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.Map("/", (HttpContext context) => Results.Json(
            InspectorHandlers.Inspect(context),
            AppJsonSerializerContext.Default.RequestInfoDto));

        routes.MapGet("/about", () => Results.Json(
            AboutDto.Singleton,
            AppJsonSerializerContext.Default.AboutDto));

        //Map this handler again, for all other endpoints, in order to be able to debug with different paths
        routes.MapFallback((HttpContext context) => Results.Json(
            InspectorHandlers.Inspect(context),
            AppJsonSerializerContext.Default.RequestInfoDto));
    }
}