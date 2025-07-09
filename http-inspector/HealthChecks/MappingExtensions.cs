using HttpInspector.HealthChecks.ResponseWriters;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace HttpInspector.HealthChecks;

public static class MappingExtensions
{
    /// <summary>
    /// Maps the default health checks.
    /// </summary>
    /// <seealso cref="MapAllHealthChecks"/>
    /// <seealso cref="MapExplainHealthCheck"/>
    /// <seealso cref="MapLiveHealthCheck"/>
    /// <seealso cref="MapReadyHealthCheck"/>
    public static void MapDefaultHealthChecks(this WebApplication app)
    {
        app.MapAllHealthChecks();
        if (app.Environment.IsDevelopment())
            app.MapExplainHealthCheck();
        app.MapLiveHealthCheck();
        app.MapReadyHealthCheck();
    }

    /// <summary>
    /// Maps a health check endpoint that indicates if the service is ready (to handle traffic)
    /// </summary>
    public static void MapReadyHealthCheck(this IEndpointRouteBuilder endpoints, string pattern = Endpoints.HealthReady)
    {
        endpoints.MapHealthChecks(pattern, new HealthCheckOptions
        {
            Predicate = hc => hc.Tags.Contains(Tags.Ready),
            ResponseWriter = HealthCheckResponseWriters.WriteReadyPlaintext,
        });
    }

    /// <summary>
    /// Maps a health check endpoint that indicates if the service is live (a.k.a. is the service running)
    /// </summary>
    public static void MapLiveHealthCheck(this IEndpointRouteBuilder endpoints, string pattern = Endpoints.HealthLive)
    {
        endpoints.MapHealthChecks(pattern, new HealthCheckOptions
        {
            //Includes only HealthChecks, tagged with the live tag (or none, if none have this tag)
            Predicate = hc => hc.Tags.Contains(Tags.Live),
            ResponseWriter = HealthCheckResponseWriters.WriteLivePlaintext,
        });
    }

    /// <summary>
    /// Maps a health check endpoint that explains the result of each health check, in json format
    /// </summary>
    public static void MapExplainHealthCheck(this IEndpointRouteBuilder endpoints, string pattern = Endpoints.HealthExplain)
    {
        endpoints.MapHealthChecks(pattern, new HealthCheckOptions
        {
            ResponseWriter = HealthCheckResponseWriters.WriteExplainResponse,
        });
    }

    /// <summary>
    /// Maps a health check endpoint that combines all health checks.
    /// </summary>
    public static void MapAllHealthChecks(this IEndpointRouteBuilder endpoints, string pattern = Endpoints.Health)
    {
        endpoints.MapHealthChecks(pattern);
    }
}