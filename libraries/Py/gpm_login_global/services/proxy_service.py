"""
proxy_service.py
~~~~~~~~~~~~~~~~

Service for managing proxies via the GPMLogin Global API.
"""

from __future__ import annotations

from gpm_login_global.models import PagedData, Proxy


def _ensure_success(response: dict) -> None:
    """Raise if the API response indicates failure.

    Args:
        response: Parsed API response dict.

    Raises:
        RuntimeError: When ``response["success"]`` is falsy.
    """
    if not response.get("success"):
        raise RuntimeError(f"GPMLogin API error: {response.get('message', 'unknown error')}")


class ProxyService:
    """CRUD operations for proxy entries.

    Args:
        http: Shared ``_Http`` helper from :mod:`gpm_login_global.client`.

    Example::

        client = GPMLoginGlobalClient()
        page = client.proxies.get_all(page=1, page_size=20)
        for proxy in page.data:
            print(proxy.raw_proxy)
    """

    def __init__(self, http) -> None:
        self._http = http

    def get_all(
        self,
        page: int = 1,
        page_size: int = 30,
        search: str | None = None,
        sort: str | None = None,
    ) -> PagedData[Proxy]:
        """Retrieve a paginated list of proxies.

        Args:
            page: Page number (1-based). Defaults to ``1``.
            page_size: Number of items per page. Defaults to ``30``.
            search: Optional search term to filter proxies.
            sort: Optional sort expression.

        Returns:
            A :class:`~gpm_login_global.models.PagedData` of
            :class:`~gpm_login_global.models.Proxy` objects.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        params: dict = {"page": page, "page_size": page_size}
        if search:
            params["search"] = search
        if sort:
            params["sort"] = sort
        raw = self._http.get("proxies", params=params)
        _ensure_success(raw)
        return PagedData.from_dict(raw["data"], Proxy.from_dict)

    def get_by_id(self, id: str) -> Proxy:
        """Retrieve a single proxy by its identifier.

        Args:
            id: The proxy identifier.

        Returns:
            The matching :class:`~gpm_login_global.models.Proxy`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get(f"proxies/{id}")
        _ensure_success(raw)
        return Proxy.from_dict(raw["data"])

    def create(self, raw_proxy: str) -> Proxy:
        """Create a new proxy entry.

        Args:
            raw_proxy: Connection string, e.g. ``http://user:pass@host:port``
                or ``socks5://host:port``.

        Returns:
            The newly created :class:`~gpm_login_global.models.Proxy`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.

        Example::

            proxy = client.proxies.create("http://user:pass@1.2.3.4:8080")
        """
        raw = self._http.post("proxies/create", {"raw_proxy": raw_proxy})
        _ensure_success(raw)
        return Proxy.from_dict(raw["data"])

    def update(self, id: str, raw_proxy: str) -> Proxy:
        """Update an existing proxy entry.

        Args:
            id: The proxy identifier.
            raw_proxy: New connection string.

        Returns:
            The updated :class:`~gpm_login_global.models.Proxy`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.post(f"proxies/update/{id}", {"raw_proxy": raw_proxy})
        _ensure_success(raw)
        return Proxy.from_dict(raw["data"])

    def delete(self, id: str) -> None:
        """Delete a proxy entry by its identifier.

        Args:
            id: The proxy identifier.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get(f"proxies/delete/{id}")
        _ensure_success(raw)
