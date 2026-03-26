using System.Net.Http.Json;
using GPMLoginGlobal.Models;

namespace GPMLoginGlobal.Services;

/// <summary>
/// Provides methods for managing profile groups via the GPMLogin Global API.
/// </summary>
public sealed class GroupService(HttpClient http)
{
    /// <summary>
    /// Retrieves all groups.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A read-only list of <see cref="Group"/> objects.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<IReadOnlyList<Group>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        ApiResponse<IReadOnlyList<Group>>? response =
            await http.GetFromJsonAsync<ApiResponse<IReadOnlyList<Group>>>("groups", cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Retrieves a single group by its identifier.
    /// </summary>
    /// <param name="id">The group identifier.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The matching <see cref="Group"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Group> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ApiResponse<Group>? response =
            await http.GetFromJsonAsync<ApiResponse<Group>>($"groups/{id}", cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Creates a new group.
    /// </summary>
    /// <param name="request">The group details.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The newly created <see cref="Group"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Group> CreateAsync(GroupRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponse =
            await http.PostAsJsonAsync("groups/create", request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        ApiResponse<Group>? response =
            await httpResponse.Content.ReadFromJsonAsync<ApiResponse<Group>>(cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Updates an existing group.
    /// </summary>
    /// <param name="id">The group identifier.</param>
    /// <param name="request">The updated group details.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The updated <see cref="Group"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Group> UpdateAsync(string id, GroupRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponse =
            await http.PostAsJsonAsync($"groups/update/{id}", request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        ApiResponse<Group>? response =
            await httpResponse.Content.ReadFromJsonAsync<ApiResponse<Group>>(cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Deletes a group by its identifier.
    /// </summary>
    /// <param name="id">The group identifier.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        ApiResponse<object>? response =
            await http.GetFromJsonAsync<ApiResponse<object>>($"groups/delete/{id}", cancellationToken);

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
