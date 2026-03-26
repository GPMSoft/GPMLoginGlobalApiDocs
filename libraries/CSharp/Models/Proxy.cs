using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Represents a proxy entry returned by the API.
/// </summary>
public sealed class Proxy
{
    /// <summary>Unique identifier of the proxy.</summary>
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Raw proxy connection string, e.g. <c>http://user:pass@host:port</c>.
    /// </summary>
    [JsonPropertyName("raw_proxy")]
    public string RawProxy { get; init; } = string.Empty;

    /// <summary>Tags associated with this proxy.</summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<ProxyTag> Tags { get; init; } = [];
}
