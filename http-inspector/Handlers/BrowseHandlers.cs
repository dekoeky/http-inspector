using HttpInspector.Dtos;
using HttpInspector.Http;
using HttpInspector.Http.HttpResults;

namespace HttpInspector.Handlers;

internal static class BrowseHandlers
{
    public static IResult ListMappedEndpoints(HttpContext context, EndpointDataSource endpointDataSource)
    {
        var endpoints = endpointDataSource.Endpoints
                .OfType<RouteEndpoint>()
                .Where(e => e.RoutePattern.RawText?.EndsWith(".js") != true) //skip *.js files
                .Select(e =>
                {
                    var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";
                    var route = e.RoutePattern.RawText;
                    var url = $"{baseUrl}{e.RoutePattern.RawText}";

                    //Handle special case for MapFallback
                    if (route == "{*path:nonfile}")
                    {
                        route = "Fallback for all other requests that are not files";
                        url = $"{baseUrl}/any-other-path";
                    }

                    var methods = e.Metadata
                        .OfType<HttpMethodMetadata>()
                        .FirstOrDefault()
                        ?.HttpMethods ?? ["ALL"];

                    return new RouteEndpointDto
                    {
                        Route = route,
                        Methods = string.Join(", ", methods),
                        Url = url,
                    };
                });

        return context.Request.AcceptsHtml()
            ? Results.Extensions.EndpointsHtml(endpoints)
            : Results.Json(endpoints, AppJsonSerializerContext.Default.IEnumerableRouteEndpointDto);
    }
}