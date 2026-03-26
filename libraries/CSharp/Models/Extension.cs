using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Represents a browser extension managed by GPMLogin Global.
/// </summary>
public sealed class Extension
{
    /// <summary>Unique identifier of the extension.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>Display name of the extension.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Whether the extension is currently active/enabled.</summary>
    [JsonPropertyName("active")]
    public bool Active { get; init; }
}
