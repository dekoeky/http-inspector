namespace http_inspector.Dtos;
public class RequestInfoDto
{
    public RequestMetadataDto Request { get; init; } = default!;
    public ConnectionInfoDto Connection { get; init; } = default!;
    public EnvironmentInfoDto Environment { get; init; } = default!;
    public DiagnosticInfoDto Diagnostics { get; init; } = default!;
}