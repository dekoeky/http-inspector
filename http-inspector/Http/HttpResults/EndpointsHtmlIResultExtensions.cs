using HttpInspector.Dtos;
using System.Net;
using System.Text;

namespace HttpInspector.Http.HttpResults;

public static class EndpointsHtmlIResultExtensions
{
    public static IResult EndpointsHtml(this IResultExtensions extensions, IEnumerable<RouteEndpointDto> endpoints)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<!DOCTYPE html>");
        sb.AppendLine("<html><head><meta charset=\"utf-8\"><title>Route Overview</title>");
        sb.AppendLine("<style>");
        sb.AppendLine("body { font-family: sans-serif; padding: 1rem; }");
        sb.AppendLine("table { border-collapse: collapse; width: 100%; }");
        sb.AppendLine("th, td { border: 1px solid #ccc; padding: 8px; text-align: left; }");
        sb.AppendLine("th { background-color: #f4f4f4; }");
        sb.AppendLine("</style></head><body>");
        sb.AppendLine("<h1>Available Endpoints</h1>");
        sb.AppendLine("<table>");
        sb.AppendLine("<thead><tr><th>Methods</th><th>Route</th><th>URL</th></tr></thead><tbody>");

        foreach (var e in endpoints)
        {
            sb.AppendLine("<tr>");
            sb.AppendLine($"<td>{WebUtility.HtmlEncode(e.Methods)}</td>");
            sb.AppendLine($"<td>{WebUtility.HtmlEncode(e.Route)}</td>");
            sb.AppendLine($"<td><a href=\"{WebUtility.HtmlEncode(e.Url)}\">{System.Net.WebUtility.HtmlEncode(e.Url)}</a></td>");
            sb.AppendLine("</tr>");
        }

        sb.AppendLine("</tbody></table></body></html>");

        return Results.Content(sb.ToString(), "text/html; charset=utf-8");
    }
}