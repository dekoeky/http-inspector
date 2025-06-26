using HttpInspector.Dtos;

namespace HttpInspector.Handlers;

internal static class InspectorHandlers
{
    public static IResult Inspect(HttpContext context)
    {
        var info = new RequestInfoDto
        {
            Request = GetRequestInfo(context.Request),
            Connection = GetConnectionInfo(context),
            Environment = EnvironmentInfoDto.Singleton,
            Diagnostics = GetDiagnosticsInfo(context)
        };

        return Results.Json(info, AppJsonSerializerContext.Default.RequestInfoDto);
    }

    private static RequestMetadataDto GetRequestInfo(HttpRequest request) => new()
    {
        Method = request.Method,
        Scheme = request.Scheme,
        Host = request.Host.ToString(),
        Path = request.Path,
        QueryString = request.QueryString.ToString(),
        Protocol = request.Protocol,
        ContentType = request.ContentType,
        ContentLength = request.ContentLength,
        Headers = request.Headers.ToDictionary(h => h.Key, v => v.Value.ToString()),
        Cookies = request.Cookies.ToDictionary(c => c.Key, c => c.Value),
        Query = request.Query.ToDictionary(q => q.Key, q => q.Value.ToString())
    };

    private static DiagnosticInfoDto GetDiagnosticsInfo(HttpContext context) => new()
    {
        TraceId = context.TraceIdentifier,
        Timestamp = DateTimeOffset.UtcNow,
    };

    private static ConnectionInfoDto GetConnectionInfo(HttpContext context) => new()
    {
        RemoteIp = context.Connection.RemoteIpAddress?.ToString(),
        RemotePort = context.Connection.RemotePort,
        LocalIp = context.Connection.LocalIpAddress?.ToString(),
        IsHttps = context.Request.IsHttps,
    };
}