/**
 * @file example.js
 * GPMLogin Global API – JavaScript usage example.
 *
 * Run with:
 *   npm install
 *   node example.js
 */

import { GPMLoginGlobalClient } from 'gpm-login-global';

// ── 1. Create and initialise the client ───────────────────────────────────────
// The default constructor connects to http://127.0.0.1:9495.
const client = new GPMLoginGlobalClient();

// Wait for the internal fetch implementation to resolve (required for Node < 18).
await client.ready;

// ── 2. List groups ────────────────────────────────────────────────────────────
console.log('=== Groups ===');
const groups = await client.groups.getAll();
for (const group of groups) {
  console.log(`  [${group.id}] ${group.name}  (order: ${group.order})`);
}

// ── 3. Create a group ─────────────────────────────────────────────────────────
const newGroup = await client.groups.create({ name: 'Sample Group', order: 99 });
console.log(`\nCreated group: ${newGroup.id} – ${newGroup.name}`);

// ── 4. List proxies ───────────────────────────────────────────────────────────
console.log('\n=== Proxies (page 1) ===');
const proxiesPage = await client.proxies.getAll({ page: 1, pageSize: 10 });
console.log(`  Total proxies: ${proxiesPage.total}`);
for (const proxy of proxiesPage.data) {
  console.log(`  [${proxy.id}] ${proxy.raw_proxy}`);
}

// ── 5. List profiles ──────────────────────────────────────────────────────────
console.log('\n=== Profiles (page 1) ===');
const profilesPage = await client.profiles.getAll({ page: 1, perPage: 10 });
console.log(`  Total profiles: ${profilesPage.total}`);
for (const profile of profilesPage.data) {
  console.log(`  [${profile.id}] ${profile.name}`);
}

// ── 6. Create a profile ───────────────────────────────────────────────────────
const newProfile = await client.profiles.create({
  name: 'Sample Profile',
  group_id: newGroup.id,
  browser_type: 1,
  browser_version: '120.0.6099.109',
  os_type: 1,
  timezone_base_on_ip: true,
  is_language_base_on_ip: true,
});
console.log(`\nCreated profile: ${newProfile.id} – ${newProfile.name}`);

// ── 7. Start the browser ──────────────────────────────────────────────────────
console.log(`\nStarting profile ${newProfile.id} …`);
const startResult = await client.profiles.start(newProfile.id, {
  windowSize: '1280,800',
});

console.log(`  Remote debugging port : ${startResult.remote_debugging_port}`);
console.log(`  ChromeDriver path     : ${startResult.driver_path}`);
console.log(`  Browser process ID    : ${startResult.addition_info?.process_id}`);

// At this point you can connect Playwright (CDP) or Selenium:
//   const browser = await playwright.chromium.connectOverCDP(
//     `http://127.0.0.1:${startResult.remote_debugging_port}`
//   );

console.log('\nWaiting 5 seconds, then stopping …');
await new Promise((resolve) => setTimeout(resolve, 5000));

// ── 8. Stop the browser ───────────────────────────────────────────────────────
await client.profiles.stop(newProfile.id);
console.log('Browser stopped.');

// ── 9. List extensions ────────────────────────────────────────────────────────
console.log('\n=== Extensions ===');
const extensions = await client.extensions.getAll();
for (const ext of extensions) {
  console.log(`  [${ext.id}] ${ext.name}  active=${ext.active}`);
}

// ── 10. Cleanup ───────────────────────────────────────────────────────────────
await client.profiles.delete(newProfile.id, 'hard');
await client.groups.delete(newGroup.id);
console.log('\nCleanup complete.');
