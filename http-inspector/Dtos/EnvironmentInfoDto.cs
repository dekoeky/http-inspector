using System.Runtime.InteropServices;

namespace http_inspector.Dtos;

public class EnvironmentInfoDto
{
    /// <summary>
    /// Users of this class should use <see cref="Singleton"/>.
    /// </summary>
    private EnvironmentInfoDto()
    {

    }

    public string MachineName { get; init; } = default!;
    public int ProcessId { get; init; }
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
            ProcessId = Environment.ProcessId,
            OSDescription = RuntimeInformation.OSDescription,
            AppVersion = assemblyName.Version?.ToString(),
            AppName = assemblyName.Name,
        };
    }
}