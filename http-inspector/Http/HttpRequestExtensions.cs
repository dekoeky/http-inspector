namespace HttpInspector.Http;

/// <summary>
/// <see cref="HttpRequest"/> extensions.
/// </summary>
public static class HttpRequestExtensions
{
    /// <summary>
    /// Checks if the request accepts HTML content.
    /// </summary>
    public static bool AcceptsHtml(this HttpRequest request)
    {
        foreach (var item in request.Headers.Accept)
        {
            if (item is null) continue;
            if (item.Contains("text/html")) return true;
            if (item.Contains("*/*")) return true;
        }

        return false;
    }
}