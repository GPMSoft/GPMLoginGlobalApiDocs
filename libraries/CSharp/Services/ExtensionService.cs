using System.Net.Http.Json;
using GPMLoginGlobal.Models;

namespace GPMLoginGlobal.Services;

/// <summary>
/// Provides methods for managing browser extensions via the GPMLogin Global API.
/// </summary>
public sealed class ExtensionService(HttpClient http)
{
    /// <summary>
    /// Retrieves all installed extensions.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A read-only list of <see cref="Extension"/> objects.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<IReadOnlyList<Extension>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        ApiResponse<IReadOnlyList<Extension>>? response =
            await http.GetFromJsonAsync<ApiResponse<IReadOnlyList<Extension>>>("extensions", cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Enables or disables a specific extension.
    /// </summary>
    /// <param name="id">The extension identifier.</param>
    /// <param name="active"><see langword="true"/> to enable the extension; <see langword="false"/> to disable it.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task UpdateStateAsync(string id, bool active, CancellationToken cancellationToken = default)
    {
        string activeStr = active ? "true" : "false";

        ApiResponse<object>? response =
            await http.GetFromJsonAsync<ApiResponse<object>>(
                $"extensions/update-state/{id}?active={activeStr}",
                cancellationToken);

        EnsureSuccess(response);
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    private static void EnsureSuccess<T>(ApiResponse<T>? response)
    {
        if (response is null)
            throw new InvalidOperationException("The API returned an empty response.");

        if (!response.Success)
            throw new InvalidOperationException($"GPMLogin API error: {response.Message}");
    }
}
