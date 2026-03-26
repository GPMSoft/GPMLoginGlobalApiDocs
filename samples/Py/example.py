"""
example.py
~~~~~~~~~~

GPMLogin Global API – Python usage example.

Install the library first::

    pip install -e ../../libraries/Py

Then run::

    python example.py
"""

import sys
import time

# Add the library source to the path when running without installation.
sys.path.insert(0, "../../libraries/Py")

from gpm_login_global import GPMLoginGlobalClient

# ── 1. Create the client ──────────────────────────────────────────────────────
# The default constructor connects to http://127.0.0.1:9495.
# Pass a custom URL if GPMLogin is running on a different host/port.
client = GPMLoginGlobalClient()

# ── 2. List groups ────────────────────────────────────────────────────────────
print("=== Groups ===")
groups = client.groups.get_all()
for group in groups:
    print(f"  [{group.id}] {group.name}  (order: {group.order})")

# ── 3. Create a group ─────────────────────────────────────────────────────────
new_group = client.groups.create("Sample Group", order=99)
print(f"\nCreated group: {new_group.id} – {new_group.name}")

# ── 4. List proxies ───────────────────────────────────────────────────────────
print("\n=== Proxies (page 1) ===")
proxies_page = client.proxies.get_all(page=1, page_size=10)
print(f"  Total proxies: {proxies_page.total}")
for proxy in proxies_page.data:
    print(f"  [{proxy.id}] {proxy.raw_proxy}")

# ── 5. List profiles ──────────────────────────────────────────────────────────
print("\n=== Profiles (page 1) ===")
profiles_page = client.profiles.get_all(page=1, per_page=10)
print(f"  Total profiles: {profiles_page.total}")
for profile in profiles_page.data:
    print(f"  [{profile.id}] {profile.name}")

# ── 6. Create a profile ───────────────────────────────────────────────────────
new_profile = client.profiles.create(
    "Sample Profile",
    group_id=new_group.id,
    browser_type=1,
    browser_version="120.0.6099.109",
    os_type=1,
    timezone_base_on_ip=True,
    is_language_base_on_ip=True,
)
print(f"\nCreated profile: {new_profile.id} – {new_profile.name}")

# ── 7. Start the browser ──────────────────────────────────────────────────────
print(f"\nStarting profile {new_profile.id} …")
start_result = client.profiles.start(new_profile.id, window_size="1280,800")

print(f"  Remote debugging port : {start_result.remote_debugging_port}")
print(f"  ChromeDriver path     : {start_result.driver_path}")
print(f"  Browser process ID    : {start_result.addition_info.process_id if start_result.addition_info else 'N/A'}")

# At this point you can attach Selenium using the debugging port:
#
#   from selenium import webdriver
#   from selenium.webdriver.chrome.options import Options
#   from selenium.webdriver.chrome.service import Service
#
#   options = Options()
#   options.debugger_address = f"127.0.0.1:{start_result.remote_debugging_port}"
#   service = Service(executable_path=start_result.driver_path)
#   driver = webdriver.Chrome(service=service, options=options)
#   print(driver.title)

print("\nWaiting 5 seconds, then stopping …")
time.sleep(5)

# ── 8. Stop the browser ───────────────────────────────────────────────────────
client.profiles.stop(new_profile.id)
print("Browser stopped.")

# ── 9. List extensions ────────────────────────────────────────────────────────
print("\n=== Extensions ===")
extensions = client.extensions.get_all()
for ext in extensions:
    print(f"  [{ext.id}] {ext.name}  active={ext.active}")

# ── 10. Cleanup ───────────────────────────────────────────────────────────────
client.profiles.delete(new_profile.id, mode="hard")
client.groups.delete(new_group.id)
print("\nCleanup complete.")
