using HttpInspector.Dtos;
using HttpInspector.Http;
using HttpInspector.Http.HttpResults;
using HttpInspector.Json;

namespace HttpInspector.Handlers;

internal static class BrowseHandlers
{
    public static IResult BrowseMappedEndpoints(HttpContext context, EndpointDataSource endpointDataSource)
    {
        var endpoints = endpointDataSource.Endpoints
                .OfType<RouteEndpoint>()
                .Where(e => e.RoutePattern.RawText?.EndsWith(".js") != true) //skip *.js files
                .Select(e =>
                {
                    var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";
                    var route = e.RoutePattern.RawText;

                    //Handle special case for MapFallback
                    var examplePath = route == "{*path:nonfile}"
                        ? "any-other-path"
                        : e.RoutePattern.RawText;

                    var methods = e.Metadata
                            .OfType<HttpMethodMetadata>()
                            .FirstOrDefault()
                            ?.HttpMethods ?? ["ALL"];

                    return new RouteEndpointDto
                    {
                        Route = route,
                        Methods = string.Join(", ", methods),
                        Url = $"{baseUrl}{examplePath}",
                        DisplayName = e.DisplayName,
                    };
                });

        return context.Request.AcceptsHtml()
            ? Results.Extensions.EndpointsHtml(endpoints)
            : Results.Json(endpoints, AppJsonSerializerContext.Default.IEnumerableRouteEndpointDto);
    }
}