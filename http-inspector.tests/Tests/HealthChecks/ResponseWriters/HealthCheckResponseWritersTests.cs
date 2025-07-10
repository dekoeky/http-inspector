using HttpInspector.HealthChecks.ResponseWriters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HttpInspector.Tests.HealthChecks.ResponseWriters;

[TestClass]
public class HealthCheckResponseWritersTests
{
    [DataTestMethod]
    [DataRow(HealthStatus.Unhealthy, "dead")]
    [DataRow(HealthStatus.Degraded, "dead")]
    [DataRow(HealthStatus.Healthy, "live")]
    public Task WriteLivePlaintext(HealthStatus status, string expectedBody)
        => Test(status, expectedBody, HealthCheckResponseWriters.WriteLivePlaintext);

    [DataTestMethod]
    [DataRow(HealthStatus.Unhealthy, "unready")]
    [DataRow(HealthStatus.Degraded, "unready")]
    [DataRow(HealthStatus.Healthy, "ready")]
    public Task WriteReadyPlaintext(HealthStatus status, string expectedBody)
        => Test(status, expectedBody, HealthCheckResponseWriters.WriteReadyPlaintext);

    private static async Task Test(HealthStatus status, string expectedBody, Func<HttpContext, HealthReport, Task> writeResponse)
    {
        // ---------- ARRANGE ----------
        var expectOkStatus = status == HealthStatus.Healthy;
        var context = new DefaultHttpContext();
        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        var report = new HealthReport(new Dictionary<string, HealthReportEntry>(),
            status,
            TimeSpan.FromMilliseconds(10));

        // ---------- ACT --------------
        await writeResponse(context, report);

        // ---------- ASSERT -----------
        responseStream.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(responseStream);
        var body = await reader.ReadToEndAsync();
        Assert.AreEqual("text/plain; charset=utf-8", context.Response.ContentType);
        Assert.AreEqual(expectedBody, body);
        Assert.AreEqual(expectOkStatus, context.Response.StatusCode is >= 200 and < 300);
    }
}