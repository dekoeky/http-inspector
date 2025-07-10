namespace HttpInspector.Dtos;

public class RouteEndpointDto
{
    public string? Route { get; init; }
    public string? Methods { get; init; }

    /// <summary>
    /// Example Url.
    /// </summary>
    public string? Url { get; init; }

    public string? DisplayName { get; init; }
}