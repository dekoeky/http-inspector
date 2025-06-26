using HttpInspector.Dtos;

namespace HttpInspector.Handlers;

internal static class AboutHandlers
{
    public static IResult About() =>
        Results.Json(AboutDto.Singleton, AppJsonSerializerContext.Default.AboutDto);
}