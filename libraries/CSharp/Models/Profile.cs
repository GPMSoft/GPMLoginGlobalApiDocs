using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Represents a browser profile returned by the API.
/// </summary>
public sealed class Profile
{
    /// <summary>Unique identifier of the profile.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>Display name of the profile.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Identifier of the group this profile belongs to, or <see langword="null"/>.</summary>
    [JsonPropertyName("group_id")]
    public string? GroupId { get; init; }

    /// <summary>Raw proxy string assigned to this profile, or <see langword="null"/>.</summary>
    [JsonPropertyName("raw_proxy")]
    public string? RawProxy { get; init; }

    /// <summary>
    /// Browser type identifier.
    /// Consult GPMLogin documentation for the list of valid values.
    /// </summary>
    [JsonPropertyName("browser_type")]
    public int BrowserType { get; init; }

    /// <summary>Pinned browser version string, e.g. <c>"120.0.6099.109"</c>.</summary>
    [JsonPropertyName("browser_version")]
    public string BrowserVersion { get; init; } = string.Empty;

    /// <summary>Operating system type identifier.</summary>
    [JsonPropertyName("os_type")]
    public int OsType { get; init; }

    /// <summary>Custom User-Agent string, or <see langword="null"/> to use the generated one.</summary>
    [JsonPropertyName("custom_user_agent")]
    public string? CustomUserAgent { get; init; }

    /// <summary>Custom title shown in the browser task bar, or <see langword="null"/>.</summary>
    [JsonPropertyName("task_bar_title")]
    public string? TaskBarTitle { get; init; }

    /// <summary>WebRTC leak prevention mode.</summary>
    [JsonPropertyName("webrtc_mode")]
    public int? WebRtcMode { get; init; }

    /// <summary>Fixed public IP reported via WebRTC when <see cref="WebRtcMode"/> requires it.</summary>
    [JsonPropertyName("fixed_webrtc_public_ip")]
    public string FixedWebRtcPublicIp { get; init; } = string.Empty;

    /// <summary>Geolocation spoofing mode.</summary>
    [JsonPropertyName("geolocation_mode")]
    public int? GeolocationMode { get; init; }

    /// <summary>Canvas fingerprint protection mode.</summary>
    [JsonPropertyName("canvas_mode")]
    public int? CanvasMode { get; init; }

    /// <summary>ClientRects fingerprint protection mode.</summary>
    [JsonPropertyName("client_rect_mode")]
    public int? ClientRectMode { get; init; }

    /// <summary>WebGL image fingerprint protection mode.</summary>
    [JsonPropertyName("webgl_image_mode")]
    public int? WebGlImageMode { get; init; }

    /// <summary>WebGL metadata fingerprint protection mode.</summary>
    [JsonPropertyName("webgl_metadata_mode")]
    public int? WebGlMetadataMode { get; init; }

    /// <summary>Audio fingerprint protection mode.</summary>
    [JsonPropertyName("audio_mode")]
    public int? AudioMode { get; init; }

    /// <summary>Font enumeration fingerprint protection mode.</summary>
    [JsonPropertyName("font_mode")]
    public int? FontMode { get; init; }

    /// <summary>Whether the timezone should be derived from the proxy IP address.</summary>
    [JsonPropertyName("timezone_base_on_ip")]
    public bool TimezoneBaseOnIp { get; init; }

    /// <summary>Fixed timezone string (e.g. <c>"America/New_York"</c>) used when <see cref="TimezoneBaseOnIp"/> is <see langword="false"/>.</summary>
    [JsonPropertyName("timezone")]
    public string? Timezone { get; init; }

    /// <summary>Whether the browser language should be derived from the proxy IP address.</summary>
    [JsonPropertyName("is_language_base_on_ip")]
    public bool IsLanguageBaseOnIp { get; init; }

    /// <summary>Fixed language tag (e.g. <c>"en-US"</c>) used when <see cref="IsLanguageBaseOnIp"/> is <see langword="false"/>.</summary>
    [JsonPropertyName("fixed_language")]
    public string? FixedLanguage { get; init; }
}
