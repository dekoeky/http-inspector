using HttpInspector.Dtos;

namespace HttpInspector.Handlers;

internal static class EndpointInformationHandlers
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

                    return new RouteEndpointDto
                    {
                        Route = route,
                        Methods = string.Join(", ", e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods ?? ["ALL"]),
                        Url = url,
                    };
                });

        return Results.Json(endpoints, AppJsonSerializerContext.Default.IEnumerableRouteEndpointDto);
    }
}