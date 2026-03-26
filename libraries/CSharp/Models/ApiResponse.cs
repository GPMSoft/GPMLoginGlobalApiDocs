using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Represents the standard envelope returned by every GPMLogin Global API endpoint.
/// </summary>
/// <typeparam name="T">The type of the <see cref="Data"/> payload.</typeparam>
public sealed class ApiResponse<T>
{
    /// <summary>Indicates whether the request succeeded.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    /// <summary>The response payload. May be <see langword="null"/> on failure.</summary>
    [JsonPropertyName("data")]
    public T? Data { get; init; }

    /// <summary>A human-readable status message, e.g. <c>"OK"</c>.</summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>The API server identifier, e.g. <c>"GPMLoginGlobal v1.0.0"</c>.</summary>
    [JsonPropertyName("sender")]
    public string Sender { get; init; } = string.Empty;
}
