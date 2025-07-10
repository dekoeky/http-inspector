using HttpInspector.Dtos;
using HttpInspector.Json;

namespace HttpInspector.Handlers;

internal static class AboutHandlers
{
    public static IResult About() =>
        Results.Json(AboutDto.Singleton, AppJsonSerializerContext.Default.AboutDto);
}