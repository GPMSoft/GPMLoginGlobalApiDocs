"""
gpm_login_global
~~~~~~~~~~~~~~~~

Official Python client library for the GPMLogin Global Local API.

Basic usage::

    from gpm_login_global import GPMLoginGlobalClient

    client = GPMLoginGlobalClient()

    # List all profiles
    page = client.profiles.get_all(page=1, per_page=10)
    for profile in page.data:
        print(profile.id, profile.name)

    # Start a profile
    result = client.profiles.start(profile.id)
    print(result.remote_debugging_port)
"""

from gpm_login_global.client import GPMLoginGlobalClient

__all__ = ["GPMLoginGlobalClient"]
__version__ = "1.0.0"
