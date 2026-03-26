using System.Text.Json.Serialization;

namespace GPMLoginGlobal.Models;

/// <summary>
/// Wraps a paginated list of items returned by list endpoints.
/// </summary>
/// <typeparam name="T">The element type of the <see cref="Data"/> collection.</typeparam>
public sealed class PagedData<T>
{
    /// <summary>The current page number (1-based).</summary>
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; init; }

    /// <summary>Number of items per page.</summary>
    [JsonPropertyName("per_page")]
    public int PerPage { get; init; }

    /// <summary>Total number of items across all pages.</summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }

    /// <summary>Index of the last available page.</summary>
    [JsonPropertyName("last_page")]
    public int LastPage { get; init; }

    /// <summary>The items on the current page.</summary>
    [JsonPropertyName("data")]
    public IReadOnlyList<T> Data { get; init; } = [];
}
