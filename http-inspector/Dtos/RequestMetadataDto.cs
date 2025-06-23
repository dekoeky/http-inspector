namespace HttpInspector.Dtos;

public class RequestMetadataDto
{
    public string Method { get; init; } = default!;
    public string Scheme { get; init; } = default!;
    public string Host { get; init; } = default!;
    public string Path { get; init; } = default!;
    public string QueryString { get; init; } = default!;
    public string Protocol { get; init; } = default!;
    public string? ContentType { get; init; }
    public long? ContentLength { get; init; }
    public Dictionary<string, string> Headers { get; init; } = new();
    public Dictionary<string, string> Cookies { get; init; } = new();
    public Dictionary<string, string> Query { get; init; } = new();
}