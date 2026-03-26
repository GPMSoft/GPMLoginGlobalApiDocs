"""
client.py
~~~~~~~~~

Core HTTP client and main entry point for the GPMLogin Global Local API.
"""

from __future__ import annotations

import requests

from gpm_login_global.services.group_service import GroupService
from gpm_login_global.services.proxy_service import ProxyService
from gpm_login_global.services.profile_service import ProfileService
from gpm_login_global.services.extension_service import ExtensionService


class _Http:
    """Thin wrapper around :class:`requests.Session` used by all services.

    Args:
        base_url: Full base URL including the ``/api/v1`` path segment.
        session: Pre-configured :class:`requests.Session` (optional).
    """

    def __init__(self, base_url: str, session: requests.Session | None = None) -> None:
        self._base_url = base_url.rstrip("/")
        self._session = session or requests.Session()

    def get(self, path: str, params: dict | None = None) -> dict:
        """Perform a GET request and return the parsed JSON body.

        Args:
            path: Path appended to the base URL (no leading slash).
            params: Optional query-string parameters.

        Returns:
            Parsed JSON response as a Python dict.

        Raises:
            requests.HTTPError: On a non-2xx HTTP status code.
        """
        response = self._session.get(f"{self._base_url}/{path}", params=params)
        response.raise_for_status()
        return response.json()

    def post(self, path: str, body: dict) -> dict:
        """Perform a POST request with a JSON body and return the parsed JSON.

        Args:
            path: Path appended to the base URL (no leading slash).
            body: Request payload serialised as JSON.

        Returns:
            Parsed JSON response as a Python dict.

        Raises:
            requests.HTTPError: On a non-2xx HTTP status code.
        """
        response = self._session.post(f"{self._base_url}/{path}", json=body)
        response.raise_for_status()
        return response.json()


class GPMLoginGlobalClient:
    """Main client for the GPMLogin Global Local API.

    Args:
        base_url: Base URL of the GPMLogin Global application.
            Defaults to ``http://127.0.0.1:9495``. Do **not** include a trailing
            slash or the ``/api/v1`` path segment.
        session: Optional pre-configured :class:`requests.Session` – useful for
            setting custom headers, timeouts, or proxies at the transport level.

    Example::

        client = GPMLoginGlobalClient()
        profiles = client.profiles.get_all()
        for p in profiles.data:
            print(p.name)
    """

    def __init__(
        self,
        base_url: str = "http://127.0.0.1:9495",
        session: requests.Session | None = None,
    ) -> None:
        api_base = f"{base_url.rstrip('/')}/api/v1"
        http = _Http(api_base, session)

        #: Service for managing profile groups.
        self.groups: GroupService = GroupService(http)

        #: Service for managing proxies.
        self.proxies: ProxyService = ProxyService(http)

        #: Service for managing browser profiles.
        self.profiles: ProfileService = ProfileService(http)

        #: Service for managing extensions.
        self.extensions: ExtensionService = ExtensionService(http)
