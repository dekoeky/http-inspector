namespace HttpInspector.Dtos;

public class ConnectionInfoDto
{
    public string? RemoteIp { get; init; }
    public int RemotePort { get; init; }
    public string? LocalIp { get; init; }
    public bool IsHttps { get; init; }
    //TODO: Add Client Certificate Information
}