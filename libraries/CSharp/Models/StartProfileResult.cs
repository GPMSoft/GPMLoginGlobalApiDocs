using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Data returned by the start-profile endpoint once the browser process is running.
/// </summary>
public sealed class StartProfileResult
{
    /// <summary>The profile identifier that was started.</summary>
    [JsonPropertyName("profile_id")]
    public string ProfileId { get; init; } = string.Empty;

    /// <summary>Absolute path to the ChromeDriver executable for this browser version.</summary>
    [JsonPropertyName("driver_path")]
    public string DriverPath { get; init; } = string.Empty;

    /// <summary>The Chrome DevTools Protocol port the browser is listening on.</summary>
    [JsonPropertyName("remote_debugging_port")]
    public int RemoteDebuggingPort { get; init; }

    /// <summary>Additional runtime information about the launched process.</summary>
    [JsonPropertyName("addition_info")]
    public StartProfileAdditionInfo? AdditionInfo { get; init; }
}

/// <summary>
/// Extra runtime details included in the start-profile response.
/// </summary>
public sealed class StartProfileAdditionInfo
{
    /// <summary>OS process ID of the browser process.</summary>
    [JsonPropertyName("process_id")]
    public int ProcessId { get; init; }

    /// <summary>Display name of the started profile.</summary>
    [JsonPropertyName("profile_name")]
    public string ProfileName { get; init; } = string.Empty;

    /// <summary>Native window handle of the browser window (Windows HWND cast to long).</summary>
    [JsonPropertyName("window_handle")]
    public long WindowHandle { get; init; }
}
