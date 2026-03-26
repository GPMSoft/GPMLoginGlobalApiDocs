using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// A label/tag associated with a proxy entry.
/// </summary>
public sealed class ProxyTag
{
    /// <summary>Unique identifier of the tag.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>Human-readable tag name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}
