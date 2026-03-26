"""
extension_service.py
~~~~~~~~~~~~~~~~~~~~

Service for managing browser extensions via the GPMLogin Global API.
"""

from __future__ import annotations

from gpm_login_global.models import Extension


def _ensure_success(response: dict) -> None:
    """Raise if the API response indicates failure.

    Args:
        response: Parsed API response dict.

    Raises:
        RuntimeError: When ``response["success"]`` is falsy.
    """
    if not response.get("success"):
        raise RuntimeError(f"GPMLogin API error: {response.get('message', 'unknown error')}")


class ExtensionService:
    """Operations for listing and toggling browser extensions.

    Args:
        http: Shared ``_Http`` helper from :mod:`gpm_login_global.client`.

    Example::

        client = GPMLoginGlobalClient()
        extensions = client.extensions.get_all()
        for ext in extensions:
            print(ext.name, ext.active)
    """

    def __init__(self, http) -> None:
        self._http = http

    def get_all(self) -> list[Extension]:
        """Retrieve all installed browser extensions.

        Returns:
            List of :class:`~gpm_login_global.models.Extension` objects.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get("extensions")
        _ensure_success(raw)
        return [Extension.from_dict(item) for item in (raw.get("data") or [])]

    def update_state(self, id: str, active: bool) -> None:
        """Enable or disable a specific extension.

        Args:
            id: The extension identifier.
            active: ``True`` to enable the extension; ``False`` to disable it.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.

        Example::

            client.extensions.update_state("ext-id-123", active=True)
        """
        active_str = "true" if active else "false"
        raw = self._http.get(f"extensions/update-state/{id}?active={active_str}")
        _ensure_success(raw)
