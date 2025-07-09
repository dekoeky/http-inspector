namespace HttpInspector.HealthChecks;

internal static class Endpoints
{
    public const string Health = "health";
    public const string HealthExplain = $"{Health}/explain";
    public const string HealthLive = $"{Health}/live";
    public const string HealthReady = $"{Health}/ready";
}