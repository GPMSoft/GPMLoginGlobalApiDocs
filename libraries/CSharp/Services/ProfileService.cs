using System.Net.Http.Json;
using GPMLoginGlobal.Models;

namespace GPMLoginGlobal.Services;

/// <summary>
/// Provides methods for managing browser profiles and controlling their lifecycle
/// via the GPMLogin Global API.
/// </summary>
public sealed class ProfileService(HttpClient http)
{
    /// <summary>
    /// Retrieves a paginated list of profiles.
    /// </summary>
    /// <param name="page">Page number (1-based). Defaults to <c>1</c>.</param>
    /// <param name="perPage">Number of items per page. Defaults to <c>30</c>.</param>
    /// <param name="search">Optional search term to filter profiles by name.</param>
    /// <param name="sort">Optional sort expression.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>A <see cref="PagedData{T}"/> containing <see cref="Profile"/> items.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<PagedData<Profile>> GetAllAsync(
        int page = 1,
        int perPage = 30,
        string? search = null,
        string? sort = null,
        CancellationToken cancellationToken = default)
    {
        string url = $"profiles?page={page}&per_page={perPage}";
        if (!string.IsNullOrEmpty(search)) url += $"&search={Uri.EscapeDataString(search)}";
        if (!string.IsNullOrEmpty(sort))   url += $"&sort={Uri.EscapeDataString(sort)}";

        ApiResponse<PagedData<Profile>>? response =
            await http.GetFromJsonAsync<ApiResponse<PagedData<Profile>>>(url, cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Retrieves a single profile by its identifier.
    /// </summary>
    /// <param name="id">The profile identifier.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The matching <see cref="Profile"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Profile> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        ApiResponse<Profile>? response =
            await http.GetFromJsonAsync<ApiResponse<Profile>>($"profiles/{id}", cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Creates a new browser profile.
    /// </summary>
    /// <param name="request">The profile configuration.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The newly created <see cref="Profile"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Profile> CreateAsync(ProfileRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponse =
            await http.PostAsJsonAsync("profiles/create", request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        ApiResponse<Profile>? response =
            await httpResponse.Content.ReadFromJsonAsync<ApiResponse<Profile>>(cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Updates an existing browser profile.
    /// </summary>
    /// <param name="id">The profile identifier.</param>
    /// <param name="request">The updated profile configuration.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>The updated <see cref="Profile"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<Profile> UpdateAsync(string id, ProfileRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage httpResponse =
            await http.PostAsJsonAsync($"profiles/update/{id}", request, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        ApiResponse<Profile>? response =
            await httpResponse.Content.ReadFromJsonAsync<ApiResponse<Profile>>(cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Deletes a browser profile.
    /// </summary>
    /// <param name="id">The profile identifier.</param>
    /// <param name="mode">
    /// Deletion mode: <c>"soft"</c> moves the profile to trash; <c>"hard"</c> permanently removes it.
    /// Defaults to <c>"soft"</c>.
    /// </param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task DeleteAsync(string id, string mode = "soft", CancellationToken cancellationToken = default)
    {
        ApiResponse<object>? response =
            await http.GetFromJsonAsync<ApiResponse<object>>($"profiles/delete/{id}?mode={mode}", cancellationToken);

        EnsureSuccess(response);
    }

    /// <summary>
    /// Starts a browser profile and returns connection details for automation.
    /// </summary>
    /// <param name="id">The profile identifier.</param>
    /// <param name="options">Optional launch parameters (port, window size, extra args, etc.).</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <returns>
    /// A <see cref="StartProfileResult"/> containing the ChromeDriver path and remote debugging port
    /// needed to attach a WebDriver session.
    /// </returns>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task<StartProfileResult> StartAsync(
        string id,
        StartProfileOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        string url = $"profiles/start/{id}";
        List<string> queryParams = [];

        if (options is not null)
        {
            if (options.RemoteDebuggingPort.HasValue)
                queryParams.Add($"remote_debugging_port={options.RemoteDebuggingPort.Value}");

            if (options.WindowScale.HasValue)
                queryParams.Add($"window_scale={options.WindowScale.Value}");

            if (!string.IsNullOrEmpty(options.WindowPos))
                queryParams.Add($"window_pos={Uri.EscapeDataString(options.WindowPos)}");

            if (!string.IsNullOrEmpty(options.WindowSize))
                queryParams.Add($"window_size={Uri.EscapeDataString(options.WindowSize)}");

            if (!string.IsNullOrEmpty(options.AdditionArgs))
                queryParams.Add($"addition_args={Uri.EscapeDataString(options.AdditionArgs)}");
        }

        if (queryParams.Count > 0)
            url += "?" + string.Join("&", queryParams);

        ApiResponse<StartProfileResult>? response =
            await http.GetFromJsonAsync<ApiResponse<StartProfileResult>>(url, cancellationToken);

        EnsureSuccess(response);
        return response!.Data!;
    }

    /// <summary>
    /// Stops a running browser profile.
    /// </summary>
    /// <param name="id">The profile identifier.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    /// <exception cref="InvalidOperationException">Thrown when the API returns <c>success: false</c>.</exception>
    public async Task StopAsync(string id, CancellationToken cancellationToken = default)
    {
        ApiResponse<object>? response =
            await http.GetFromJsonAsync<ApiResponse<object>>($"profiles/stop/{id}", cancellationToken);

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
