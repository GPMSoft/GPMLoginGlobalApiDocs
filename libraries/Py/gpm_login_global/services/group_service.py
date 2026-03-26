"""
group_service.py
~~~~~~~~~~~~~~~~

Service for managing profile groups via the GPMLogin Global API.
"""

from __future__ import annotations

from gpm_login_global.models import Group


def _ensure_success(response: dict) -> None:
    """Raise if the API response indicates failure.

    Args:
        response: Parsed API response dict.

    Raises:
        RuntimeError: When ``response["success"]`` is falsy.
    """
    if not response.get("success"):
        raise RuntimeError(f"GPMLogin API error: {response.get('message', 'unknown error')}")


class GroupService:
    """CRUD operations for profile groups.

    Args:
        http: Shared ``_Http`` helper from :mod:`gpm_login_global.client`.

    Example::

        client = GPMLoginGlobalClient()
        groups = client.groups.get_all()
        for g in groups:
            print(g.id, g.name)
    """

    def __init__(self, http) -> None:
        self._http = http

    def get_all(self) -> list[Group]:
        """Retrieve all profile groups.

        Returns:
            List of :class:`~gpm_login_global.models.Group` objects.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get("groups")
        _ensure_success(raw)
        return [Group.from_dict(item) for item in (raw.get("data") or [])]

    def get_by_id(self, id: str) -> Group:
        """Retrieve a single group by its identifier.

        Args:
            id: The group identifier.

        Returns:
            The matching :class:`~gpm_login_global.models.Group`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get(f"groups/{id}")
        _ensure_success(raw)
        return Group.from_dict(raw["data"])

    def create(self, name: str, order: int | None = None) -> Group:
        """Create a new profile group.

        Args:
            name: Display name for the group.
            order: Optional sort order. When ``None``, the server assigns a default.

        Returns:
            The newly created :class:`~gpm_login_global.models.Group`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.

        Example::

            group = client.groups.create("My Group", order=1)
        """
        body: dict = {"name": name}
        if order is not None:
            body["order"] = order
        raw = self._http.post("groups/create", body)
        _ensure_success(raw)
        return Group.from_dict(raw["data"])

    def update(self, id: str, name: str, order: int | None = None) -> Group:
        """Update an existing profile group.

        Args:
            id: The group identifier.
            name: New display name.
            order: New sort order. When ``None``, the existing order is unchanged.

        Returns:
            The updated :class:`~gpm_login_global.models.Group`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        body: dict = {"name": name}
        if order is not None:
            body["order"] = order
        raw = self._http.post(f"groups/update/{id}", body)
        _ensure_success(raw)
        return Group.from_dict(raw["data"])

    def delete(self, id: str) -> None:
        """Delete a group by its identifier.

        Args:
            id: The group identifier.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get(f"groups/delete/{id}")
        _ensure_success(raw)
