using System.Net.Http.Json;
using GPMLoginGlobal.Models;

namespace GPMLoginGlobal.Services;

/// <summary>
/// Provides methods for managing proxies via the GPMLogin Global API.
/// </summary>
public sealed class ProxyService(HttpClient http)
{
    /// <summary>
    /// Retrieves a paginated list of proxies.
    /// </summary>
    /// <param name="page">Page number (1-based). Defaults to <c>1</c>.</param>
    /// <param name="pageSize">Number of items per page. Defaults to <c>30</c>.</param>
    /// <param name="search">Optional search term to filter proxies.</param>
    /// <param name="sort">Optional sort expression.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A <see cref="PagedData{T}"/> containing <see cref="Proxy"/> items.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<PagedData<Proxy>> GetAllAsync(
        int page = 1,
        int pageSize = 30,
        string? search = null,
        string? sort = null,
        CancellationToken cancellationToken = default)
    {
        string url = BuildQuery("proxies", page, pageSize, search, sort);

        ApiResponse<PagedData<Proxy>>? response =
            await http.GetFromJsonAsync<ApiResponse<PagedData<Proxy>>>(url, cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Retrieves a single proxy by its identifier.
    /// </summary>
    /// <param name="id">The proxy identifier.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The matching <see cref="Proxy"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Proxy> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ApiResponse<Proxy>? response =
            await http.GetFromJsonAsync<ApiResponse<Proxy>>($"proxies/{id}", cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Creates a new proxy entry.
    /// </summary>
    /// <param name="request">The proxy details.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The newly created <see cref="Proxy"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Proxy> CreateAsync(ProxyRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponse =
            await http.PostAsJsonAsync("proxies/create", request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        ApiResponse<Proxy>? response =
            await httpResponse.Content.ReadFromJsonAsync<ApiResponse<Proxy>>(cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Updates an existing proxy entry.
    /// </summary>
    /// <param name="id">The proxy identifier.</param>
    /// <param name="request">The updated proxy details.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The updated <see cref="Proxy"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Proxy> UpdateAsync(string id, ProxyRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponse =
            await http.PostAsJsonAsync($"proxies/update/{id}", request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        ApiResponse<Proxy>? response =
            await httpResponse.Content.ReadFromJsonAsync<ApiResponse<Proxy>>(cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Deletes a proxy entry by its identifier.
    /// </summary>
    /// <param name="id">The proxy identifier.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        ApiResponse<object>? response =
            await http.GetFromJsonAsync<ApiResponse<object>>($"proxies/delete/{id}", cancellationToken);

        EnsureSuccess(response);
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    private static string BuildQuery(string endpoint, int page, int pageSize, string? search, string? sort)
    {
        string url = $"{endpoint}?page={page}&page_size={pageSize}";
        if (!string.IsNullOrEmpty(search)) url += $"&search={Uri.EscapeDataString(search)}";
        if (!string.IsNullOrEmpty(sort))   url += $"&sort={Uri.EscapeDataString(sort)}";
        return url;
    }

    private static void EnsureSuccess<T>(ApiResponse<T>? response)
    {
        if (response is null)
            throw new InvalidOperationException("The API returned an empty response.");

        if (!response.Success)
            throw new InvalidOperationException($"GPMLogin API error: {response.Message}");
    }
}
