using System.Diagnostics;
using System.Runtime.InteropServices;

namespace HttpInspector.Dtos;

public class AboutDto
{
    /// <summary>
    /// Users of this class should use <see cref="Singleton"/>.
    /// </summary>
    private AboutDto()
    {

    }
    public string MachineName { get; init; } = default!;
    public int ProcessId { get; init; }
    public string OSDescription { get; init; } = default!;
    public string? AppVersion { get; init; }
    public string? AppName { get; init; }
    public string? ProcessArchitecture { get; init; }
    public string? OSArchitecture { get; init; }
    public string? FrameworkDescription { get; init; }
    public string? OSVersion { get; init; }
    public int ProcessorCount { get; init; }
    public bool Is64BitOperatingSystem { get; init; }
    public bool Is64BitProcess { get; init; }
    public bool IsPrivilegedProcess { get; init; }
    public DateTime StartTime { get; init; }




    public static readonly AboutDto Singleton = Create();

    private static AboutDto Create()
    {
        var assembly = typeof(Program).Assembly;
        var assemblyName = assembly.GetName();

        return new AboutDto
        {
            MachineName = Environment.MachineName,
            ProcessorCount = Environment.ProcessorCount,

            ProcessId = Environment.ProcessId,
            StartTime = Process.GetCurrentProcess().StartTime,
            Is64BitProcess = Environment.Is64BitProcess,
            IsPrivilegedProcess = Environment.IsPrivilegedProcess,
            ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),

            OSDescription = RuntimeInformation.OSDescription,
            OSVersion = Environment.OSVersion.ToString(),
            OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
            Is64BitOperatingSystem = Environment.Is64BitOperatingSystem,


            AppVersion = assemblyName.Version?.ToString(),
            AppName = assemblyName.Name,


            FrameworkDescription = RuntimeInformation.FrameworkDescription,
        };
    }
}