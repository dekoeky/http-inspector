using BenchmarkDotNet.Attributes;
using HttpInspector.HealthChecks.ResponseWriters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Collections.ObjectModel;

namespace HttpInspector.Benchmarks.HealthChecks.ResponseWriters;

[ShortRunJob]
[BenchmarkCategory(Categories.Development)]
[MemoryDiagnoser]
public class HealthCheckResponseWritersBenchmarks
{
    private DefaultHttpContext _context = null!;
    private HealthReport _result = null!;

    [Params(HealthStatus.Healthy, HealthStatus.Unhealthy)]
    public HealthStatus Status;

    [GlobalSetup]
    public void Setup()
    {
        _context = new DefaultHttpContext
        {
            Response =
            {
                Body = new MemoryStream()
            }
        };
        _result = new HealthReport(
            ReadOnlyDictionary<string, HealthReportEntry>.Empty,
            Status,
            TimeSpan.FromMilliseconds(10));
    }

    [Benchmark]
    public async Task WriteReadyPlaintext()
    {
        _context.Response.Body.SetLength(0); // Reset
        await HealthCheckResponseWriters.WriteReadyPlaintext(_context, _result);
    }

    [Benchmark]
    public async Task WriteLivePlaintext()
    {
        _context.Response.Body.SetLength(0); // Reset
        await HealthCheckResponseWriters.WriteLivePlaintext(_context, _result);
    }
}