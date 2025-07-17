using HttpInspector.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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

        await using var jsonWriter = new Utf8JsonWriter(httpContext.Response.BodyWriter, ExplainWriterOptions);

        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("status", healthReport.Status.ToString());
        jsonWriter.WriteStartObject("results");

        foreach (var (key, entry) in healthReport.Entries)
        {
            jsonWriter.WriteStartObject(key);
            jsonWriter.WriteString("status", entry.Status.ToString());
            if (entry.Description is not null)
                jsonWriter.WriteString("description", entry.Description);
            jsonWriter.WriteString("duration", entry.Duration.ToString("g"));

            //jsonWriter.TryToSerialize("exception", entry.Exception);

            jsonWriter.WriteStartArray("tags");
            foreach (var tag in entry.Tags) jsonWriter.WriteStringValue(tag);
            jsonWriter.WriteEndArray();

            if (entry.Data.Any())
            {
                jsonWriter.WriteStartObject("data");

                foreach (var item in entry.Data)
                    jsonWriter.TryToSerialize(item.Key, item.Value);

                jsonWriter.WriteEndObject();
            }
            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();
        await jsonWriter.FlushAsync();
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