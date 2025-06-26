using System.Runtime.InteropServices;

namespace HttpInspector.Dtos;

public class EnvironmentInfoDto
{
    private EnvironmentInfoDto()
    {
        //Prevents instantiation by other types
    }

    public string MachineName { get; init; } = default!;
    public string OSDescription { get; init; } = default!;
    public string? AppVersion { get; init; }
    public string? AppName { get; init; }

    public static readonly EnvironmentInfoDto Singleton = Create();

    private static EnvironmentInfoDto Create()
    {
        var assembly = typeof(Program).Assembly;
        var assemblyName = assembly.GetName();

        return new EnvironmentInfoDto
        {
            MachineName = Environment.MachineName,
            OSDescription = RuntimeInformation.OSDescription,
            AppVersion = assemblyName.Version?.ToString(),
            AppName = assemblyName.Name,
        };
    }
}