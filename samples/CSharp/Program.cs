// GPMLogin Global API – C# sample
// Demonstrates the most common workflows: list profiles, create a profile,
// start the browser, then stop and clean up.

using GPMLoginGlobal;
using GPMLoginGlobal.Models;

// ── 1. Create the client ──────────────────────────────────────────────────────
// The default constructor connects to http://127.0.0.1:9495.
// Pass a custom URL if GPMLogin is running on a different host/port.
using GPMLoginGlobalClient client = new();

// ── 2. List groups ────────────────────────────────────────────────────────────
Console.WriteLine("=== Groups ===");
IReadOnlyList<Group> groups = await client.Groups.GetAllAsync();
foreach (Group group in groups)
    Console.WriteLine($"  [{group.Id}] {group.Name}  (order: {group.Order})");

// ── 3. Create a group ─────────────────────────────────────────────────────────
Group newGroup = await client.Groups.CreateAsync(new GroupRequest { Name = "Sample Group", Order = 99 });
Console.WriteLine($"\nCreated group: {newGroup.Id} – {newGroup.Name}");

// ── 4. List proxies ───────────────────────────────────────────────────────────
Console.WriteLine("\n=== Proxies (page 1) ===");
PagedData<Proxy> proxies = await client.Proxies.GetAllAsync(page: 1, pageSize: 10);
Console.WriteLine($"  Total proxies: {proxies.Total}");
foreach (Proxy proxy in proxies.Data)
    Console.WriteLine($"  [{proxy.Id}] {proxy.RawProxy}");

// ── 5. List profiles ──────────────────────────────────────────────────────────
Console.WriteLine("\n=== Profiles (page 1) ===");
PagedData<Profile> profiles = await client.Profiles.GetAllAsync(page: 1, perPage: 10);
Console.WriteLine($"  Total profiles: {profiles.Total}");
foreach (Profile profile in profiles.Data)
    Console.WriteLine($"  [{profile.Id}] {profile.Name}");

// ── 6. Create a profile ───────────────────────────────────────────────────────
ProfileRequest profileRequest = new()
{
    Name            = "Sample Profile",
    GroupId         = newGroup.Id,
    BrowserType     = 1,
    BrowserVersion  = "120.0.6099.109",
    OsType          = 1,
    TimezoneBaseOnIp  = true,
    IsLanguageBaseOnIp = true
};

Profile newProfile = await client.Profiles.CreateAsync(profileRequest);
Console.WriteLine($"\nCreated profile: {newProfile.Id} – {newProfile.Name}");

// ── 7. Start the browser ──────────────────────────────────────────────────────
Console.WriteLine($"\nStarting profile {newProfile.Id} …");
StartProfileResult startResult = await client.Profiles.StartAsync(
    newProfile.Id,
    new StartProfileOptions { WindowSize = "1280,800" });

Console.WriteLine($"  Remote debugging port : {startResult.RemoteDebuggingPort}");
Console.WriteLine($"  ChromeDriver path     : {startResult.DriverPath}");
Console.WriteLine($"  Browser process ID    : {startResult.AdditionInfo?.ProcessId}");

// At this point you can connect Selenium / Playwright using the debugging port.
// e.g.:
//   var options = new ChromeOptions();
//   options.DebuggerAddress = $"127.0.0.1:{startResult.RemoteDebuggingPort}";
//   var driver = new RemoteWebDriver(new Uri("http://localhost:9515"), options);

Console.WriteLine("\nPress any key to stop the browser and clean up …");
Console.ReadKey(intercept: true);

// ── 8. Stop the browser ───────────────────────────────────────────────────────
await client.Profiles.StopAsync(newProfile.Id);
Console.WriteLine("Browser stopped.");

// ── 9. Delete the sample profile and group ────────────────────────────────────
await client.Profiles.DeleteAsync(newProfile.Id, mode: "hard");
await client.Groups.DeleteAsync(newGroup.Id);
Console.WriteLine("Cleanup complete.");
