namespace HttpInspector.Services;

public static class GettingStartedMessage
{
    private const string LoggerCategory = "Getting Started";
    private const string Message =
        """
        =========== Getting Started ===================
        http-inspector is now listening for requests.
        To get started, open one of the following urls
        for a quick demonstration: 
        {DemoUrls}
        ===============================================
        """;

    /// <summary>
    /// Registers a Welcome Message in the logging that gets new users started quickly.
    /// </summary>
    /// <param name="app"></param>
    public static void RegisterGettingStartedMessage(this WebApplication app) =>
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            //Acquire a logger instance
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(LoggerCategory);

            //Grab first configured url as base URL
            var baseUrl = app.Urls.FirstOrDefault() ?? "http://localhost:8080";

            //Create an enumeration of demo urls to print
            string[] paths = [
                "/",
                "/LandingPage?UserName=John",
                "/some/path" +
                "?hello=world" +
                "&TimeOfStartup=2025-07-04T12%3A13%3A14.8689932%2B02%3A00",

                HealthChecks.Endpoints.Health,
                HealthChecks.Endpoints.HealthLive,
                HealthChecks.Endpoints.HealthReady,
                HealthChecks.Endpoints.HealthExplain,

                Endpoints.Patterns.About,
                Endpoints.Patterns.Browse,
                ];
            var urls = paths.Select(p => $"{baseUrl}{p}");

            //Parse the urls into a single string
            var demoUrls = string.Join(Environment.NewLine, urls);

            //Log the Getting Started Message
            logger.LogInformation(Message, demoUrls);
        });
}