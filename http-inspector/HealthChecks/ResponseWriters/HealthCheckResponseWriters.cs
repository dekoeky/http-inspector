using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;
using System.Text.Json;

namespace HttpInspector.HealthChecks.ResponseWriters;

/// <summary>
/// Delegates for use in <see cref="HealthCheckOptions.ResponseWriter"/>.
/// </summary>
public static class HealthCheckResponseWriters
{
    private static readonly JsonWriterOptions ExplainWriterOptions = new()
    {
        Indented = true
    };

    /// <summary>
    /// Writes a json formatted response, briefly summarizing all results.
    /// </summary>
    public static async Task WriteExplainResponse(HttpContext httpContext, HealthReport healthReport)
    {
        httpContext.Response.ContentType = "application/json; charset=utf-8";

        using var memoryStream = new MemoryStream();
        await using var jsonWriter = new Utf8JsonWriter(memoryStream, ExplainWriterOptions);

        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("status", healthReport.Status.ToString());
        jsonWriter.WriteStartObject("results");

        foreach (var healthReportEntry in healthReport.Entries)
        {
            jsonWriter.WriteStartObject(healthReportEntry.Key);
            jsonWriter.WriteString("status", healthReportEntry.Value.Status.ToString());
            if (healthReportEntry.Value.Description is not null)
                jsonWriter.WriteString("description", healthReportEntry.Value.Description);
            if (healthReportEntry.Value.Data.Any())
            {
                jsonWriter.WriteStartObject("data");

                foreach (var item in healthReportEntry.Value.Data)
                {
                    jsonWriter.WritePropertyName(item.Key);

                    JsonSerializer.Serialize(jsonWriter, item.Value,
                        item.Value.GetType());
                }

                jsonWriter.WriteEndObject();
            }
            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();
        await jsonWriter.FlushAsync();

        var json = Encoding.UTF8.GetString(memoryStream.ToArray());

        //TODO: Write directly in the body ?
        await httpContext.Response.WriteAsync(json);
    }

    private static readonly byte[] LiveBytes = "live"u8.ToArray();
    private static readonly byte[] NotLiveBytes = "dead"u8.ToArray();
    private static readonly byte[] ReadyBytes = "ready"u8.ToArray();
    private static readonly byte[] UnReadyBytes = "unready"u8.ToArray();

    public static Task WriteLivePlaintext(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = "text/plain; charset=utf-8";

        if (result.Status == HealthStatus.Healthy)
        {
            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            return httpContext.Response.Body.WriteAsync(LiveBytes).AsTask();
        }

        httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        return httpContext.Response.Body.WriteAsync(NotLiveBytes).AsTask();
    }

    public static Task WriteReadyPlaintext(HttpContext httpContext, HealthReport result)
    {
        httpContext.Response.ContentType = "text/plain; charset=utf-8";

        if (result.Status == HealthStatus.Healthy)
        {
            httpContext.Response.StatusCode = StatusCodes.Status200OK;
            return httpContext.Response.Body.WriteAsync(ReadyBytes).AsTask();
        }

        httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
        return httpContext.Response.Body.WriteAsync(UnReadyBytes).AsTask();
    }
}