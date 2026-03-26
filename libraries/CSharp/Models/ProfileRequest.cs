using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Request body used to create or update a browser profile.
/// All properties are optional except <see cref="Name"/>.
/// </summary>
public sealed class ProfileRequest
{
    /// <summary>Display name of the profile. Required.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>ID of the group to assign the profile to, or <see langword="null"/>.</summary>
    [JsonPropertyName("group_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GroupId { get; init; }

    /// <summary>
    /// Raw proxy string to assign to the profile, or <see langword="null"/> to use no proxy.
    /// Supported formats: <c>http://user:pass@host:port</c>, <c>socks5://host:port</c>, etc.
    /// </summary>
    [JsonPropertyName("raw_proxy")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RawProxy { get; init; }

    /// <summary>Browser type identifier. Refer to GPMLogin documentation for valid values.</summary>
    [JsonPropertyName("browser_type")]
    public int BrowserType { get; init; }

    /// <summary>Pinned browser version string, e.g. <c>"120.0.6099.109"</c>.</summary>
    [JsonPropertyName("browser_version")]
    public string BrowserVersion { get; init; } = string.Empty;

    /// <summary>Operating system type identifier.</summary>
    [JsonPropertyName("os_type")]
    public int OsType { get; init; }

    /// <summary>Custom User-Agent string, or <see langword="null"/> to auto-generate.</summary>
    [JsonPropertyName("custom_user_agent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CustomUserAgent { get; init; }

    /// <summary>Custom browser task-bar title, or <see langword="null"/>.</summary>
    [JsonPropertyName("task_bar_title")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TaskBarTitle { get; init; }

    /// <summary>WebRTC leak prevention mode.</summary>
    [JsonPropertyName("webrtc_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? WebRtcMode { get; init; }

    /// <summary>Fixed public IP to report via WebRTC when the mode requires it.</summary>
    [JsonPropertyName("fixed_webrtc_public_ip")]
    public string FixedWebRtcPublicIp { get; init; } = string.Empty;

    /// <summary>Geolocation spoofing mode.</summary>
    [JsonPropertyName("geolocation_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? GeolocationMode { get; init; }

    /// <summary>Canvas fingerprint protection mode.</summary>
    [JsonPropertyName("canvas_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CanvasMode { get; init; }

    /// <summary>ClientRects fingerprint protection mode.</summary>
    [JsonPropertyName("client_rect_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ClientRectMode { get; init; }

    /// <summary>WebGL image fingerprint protection mode.</summary>
    [JsonPropertyName("webgl_image_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? WebGlImageMode { get; init; }

    /// <summary>WebGL metadata fingerprint protection mode.</summary>
    [JsonPropertyName("webgl_metadata_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? WebGlMetadataMode { get; init; }

    /// <summary>Audio fingerprint protection mode.</summary>
    [JsonPropertyName("audio_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? AudioMode { get; init; }

    /// <summary>Font enumeration fingerprint protection mode.</summary>
    [JsonPropertyName("font_mode")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? FontMode { get; init; }

    /// <summary>When <see langword="true"/>, the timezone is derived from the proxy IP.</summary>
    [JsonPropertyName("timezone_base_on_ip")]
    public bool TimezoneBaseOnIp { get; init; } = true;

    /// <summary>Fixed timezone string used when <see cref="TimezoneBaseOnIp"/> is <see langword="false"/>.</summary>
    [JsonPropertyName("timezone")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Timezone { get; init; }

    /// <summary>When <see langword="true"/>, the browser language is derived from the proxy IP.</summary>
    [JsonPropertyName("is_language_base_on_ip")]
    public bool IsLanguageBaseOnIp { get; init; } = true;

    /// <summary>Fixed language tag used when <see cref="IsLanguageBaseOnIp"/> is <see langword="false"/>.</summary>
    [JsonPropertyName("fixed_language")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FixedLanguage { get; init; }
}
