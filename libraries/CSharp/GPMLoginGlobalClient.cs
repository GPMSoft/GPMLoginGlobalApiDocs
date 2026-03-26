using GPMLoginGlobal.Services;

namespace GPMLoginGlobal;

/// <summary>
/// Main entry point for the GPMLogin Global Local API client.
/// Provides access to all service endpoints.
/// </summary>
public sealed class GPMLoginGlobalClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private bool _disposed;

    /// <summary>Gets the service for managing profile groups.</summary>
    public GroupService Groups { get; }

    /// <summary>Gets the service for managing proxies.</summary>
    public ProxyService Proxies { get; }

    /// <summary>Gets the service for managing browser profiles.</summary>
    public ProfileService Profiles { get; }

    /// <summary>Gets the service for managing extensions.</summary>
    public ExtensionService Extensions { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="GPMLoginGlobalClient"/> with the default base URL.
    /// </summary>
    public GPMLoginGlobalClient() : this("http://127.0.0.1:9495") { }

    /// <summary>
    /// Initializes a new instance of <see cref="GPMLoginGlobalClient"/> with a custom base URL.
    /// </summary>
    /// <param name="baseUrl">
    /// The base URL of the GPMLogin Global Local API, e.g. <c>http://127.0.0.1:9495</c>.
    /// Do not include a trailing slash or the <c>/api/v1</c> path segment.
    /// </param>
    public GPMLoginGlobalClient(string baseUrl)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/api/v1/")
        };

        Groups     = new GroupService(_httpClient);
        Proxies    = new ProxyService(_httpClient);
        Profiles   = new ProfileService(_httpClient);
        Extensions = new ExtensionService(_httpClient);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_disposed) return;
        _httpClient.Dispose();
        _disposed = true;
    }
}
