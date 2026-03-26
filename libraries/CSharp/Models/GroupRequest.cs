using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Request body used to create or update a profile group.
/// </summary>
public sealed class GroupRequest
{
    /// <summary>Display name of the group. Required.</summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Optional sort order. When <see langword="null"/> the server assigns a default order.
    /// </summary>
    [JsonPropertyName("order")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Order { get; init; }
}
