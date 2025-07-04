namespace HttpInspector;

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

            //Grab all configured urls to print them in the message
            //If no urls are configured, use a default one to generate the demo urls
            var defaultUrls = app.Urls.Count > 0
                ? app.Urls.ToList()
                : ["http://localhost:8080"];

            //Use the first url for our demo urls
            var first = defaultUrls.First();

            //Create an enumeration of demo urls to print
            var allUrls = defaultUrls
                .Append($"{first}/LandingPage?UserName=John")
                .Append($"{first}/some/path" +
                        "?hello=world" +
                        "&TimeOfStartup=2025-07-04T12%3A13%3A14.8689932%2B02%3A00")
                ;

            //Parse the urls into a single string
            var demoUrls = string.Join(Environment.NewLine, allUrls);

            //Log the Getting Started Message
            logger.LogInformation(Message, demoUrls);
        });
}