namespace GPMLoginGlobal.Models;

/// <summary>
/// Optional query parameters passed to the start-profile endpoint.
/// </summary>
public sealed class StartProfileOptions
{
    /// <summary>
    /// Chrome remote debugging port to use. When <see langword="null"/> the server picks a free port.
    /// </summary>
    public int? RemoteDebuggingPort { get; init; }

    /// <summary>
    /// DPI scaling factor for the browser window, e.g. <c>1.0</c> for 100 %.
    /// </summary>
    public double? WindowScale { get; init; }

    /// <summary>
    /// Initial window position in the format <c>"x,y"</c>, e.g. <c>"0,0"</c>.
    /// </summary>
    public string? WindowPos { get; init; }

    /// <summary>
    /// Initial window size in the format <c>"width,height"</c>, e.g. <c>"1280,800"</c>.
    /// </summary>
    public string? WindowSize { get; init; }

    /// <summary>
    /// Additional Chrome command-line arguments passed verbatim to the browser process.
    /// </summary>
    public string? AdditionArgs { get; init; }
}
