using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Represents a profile group returned by the API.
/// </summary>
public sealed class Group
{
    /// <summary>Unique identifier of the group.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>Display name of the group.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>Sort order of the group in the UI.</summary>
    [JsonPropertyName("order")]
    public int Order { get; init; }
}
