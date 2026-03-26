using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Request body used to create or update a proxy entry.
/// </summary>
public sealed class ProxyRequest
{
    /// <summary>
    /// Raw proxy connection string.
    /// Supported formats: <c>http://user:pass@host:port</c>,
    /// <c>socks5://user:pass@host:port</c>, etc.
    /// </summary>
    [JsonPropertyName("raw_proxy")]
    public string RawProxy { get; init; } = string.Empty;
}
