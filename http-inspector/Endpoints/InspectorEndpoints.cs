using HttpInspector.Handlers;

namespace HttpInspector.Endpoints;

public static class InspectorEndpoints
{
    public static void MapInspectorEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.Map("/", (HttpContext ctx) => Results.Json(
            InspectorHandlers.Inspect(ctx),
            AppJsonSerializerContext.Default.RequestInfoDto));
    }
}