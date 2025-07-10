using BenchmarkDotNet.Attributes;
using HttpInspector.Dtos;
using HttpInspector.Json;
using System.Text.Json;

namespace HttpInspector.Benchmarks;

[MemoryDiagnoser]
public class AppJsonSerializerContextBenchmarks
{
    private readonly AboutDto _dto = AboutDto.Singleton;
    private readonly AppJsonSerializerContext _ctx = AppJsonSerializerContext.Default;

    [Benchmark]
    public string SerializeToString() => JsonSerializer.Serialize(_dto, _ctx.AboutDto);

    [Benchmark]
    public byte[] SerializeToBytes() => JsonSerializer.SerializeToUtf8Bytes(_dto, _ctx.AboutDto);
}