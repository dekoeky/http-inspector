namespace HttpInspector.Dtos;

public class DiagnosticInfoDto
{
    public string TraceId { get; init; } = default!;
    public DateTimeOffset Timestamp { get; init; }
}