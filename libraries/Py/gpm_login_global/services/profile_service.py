"""
profile_service.py
~~~~~~~~~~~~~~~~~~

Service for managing browser profiles and controlling their lifecycle
via the GPMLogin Global API.
"""

from __future__ import annotations

from gpm_login_global.models import PagedData, Profile, StartProfileResult


def _ensure_success(response: dict) -> None:
    """Raise if the API response indicates failure.

    Args:
        response: Parsed API response dict.

    Raises:
        RuntimeError: When ``response["success"]`` is falsy.
    """
    if not response.get("success"):
        raise RuntimeError(f"GPMLogin API error: {response.get('message', 'unknown error')}")


class ProfileService:
    """CRUD operations and lifecycle control for browser profiles.

    Args:
        http: Shared ``_Http`` helper from :mod:`gpm_login_global.client`.

    Example::

        client = GPMLoginGlobalClient()
        page = client.profiles.get_all(page=1, per_page=10)
        for p in page.data:
            print(p.id, p.name)
    """

    def __init__(self, http) -> None:
        self._http = http

    def get_all(
        self,
        page: int = 1,
        per_page: int = 30,
        search: str | None = None,
        sort: str | None = None,
    ) -> PagedData[Profile]:
        """Retrieve a paginated list of browser profiles.

        Args:
            page: Page number (1-based). Defaults to ``1``.
            per_page: Number of items per page. Defaults to ``30``.
            search: Optional search term to filter profiles by name.
            sort: Optional sort expression.

        Returns:
            A :class:`~gpm_login_global.models.PagedData` of
            :class:`~gpm_login_global.models.Profile` objects.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        params: dict = {"page": page, "per_page": per_page}
        if search:
            params["search"] = search
        if sort:
            params["sort"] = sort
        raw = self._http.get("profiles", params=params)
        _ensure_success(raw)
        return PagedData.from_dict(raw["data"], Profile.from_dict)

    def get_by_id(self, id: str) -> Profile:
        """Retrieve a single profile by its identifier.

        Args:
            id: The profile identifier.

        Returns:
            The matching :class:`~gpm_login_global.models.Profile`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get(f"profiles/{id}")
        _ensure_success(raw)
        return Profile.from_dict(raw["data"])

    def create(
        self,
        name: str,
        *,
        group_id: str | None = None,
        raw_proxy: str | None = None,
        browser_type: int = 1,
        browser_version: str = "",
        os_type: int = 1,
        custom_user_agent: str | None = None,
        task_bar_title: str | None = None,
        webrtc_mode: int | None = None,
        fixed_webrtc_public_ip: str = "",
        geolocation_mode: int | None = None,
        canvas_mode: int | None = None,
        client_rect_mode: int | None = None,
        webgl_image_mode: int | None = None,
        webgl_metadata_mode: int | None = None,
        audio_mode: int | None = None,
        font_mode: int | None = None,
        timezone_base_on_ip: bool = True,
        timezone: str | None = None,
        is_language_base_on_ip: bool = True,
        fixed_language: str | None = None,
    ) -> Profile:
        """Create a new browser profile.

        Args:
            name: Display name for the profile. Required.
            group_id: Group to assign the profile to. Defaults to ``None``.
            raw_proxy: Proxy connection string. Defaults to ``None``.
            browser_type: Browser type identifier. Defaults to ``1``.
            browser_version: Pinned browser version string.
            os_type: Operating system type identifier. Defaults to ``1``.
            custom_user_agent: Custom User-Agent string. Defaults to ``None``.
            task_bar_title: Browser window title. Defaults to ``None``.
            webrtc_mode: WebRTC protection mode. Defaults to ``None``.
            fixed_webrtc_public_ip: Fixed public IP for WebRTC. Defaults to ``""``.
            geolocation_mode: Geolocation spoofing mode. Defaults to ``None``.
            canvas_mode: Canvas fingerprint protection mode. Defaults to ``None``.
            client_rect_mode: ClientRects protection mode. Defaults to ``None``.
            webgl_image_mode: WebGL image protection mode. Defaults to ``None``.
            webgl_metadata_mode: WebGL metadata protection mode. Defaults to ``None``.
            audio_mode: Audio fingerprint protection mode. Defaults to ``None``.
            font_mode: Font fingerprint protection mode. Defaults to ``None``.
            timezone_base_on_ip: Derive timezone from proxy IP. Defaults to ``True``.
            timezone: Fixed timezone string (e.g. ``"America/New_York"``).
            is_language_base_on_ip: Derive language from proxy IP. Defaults to ``True``.
            fixed_language: Fixed language tag (e.g. ``"en-US"``).

        Returns:
            The newly created :class:`~gpm_login_global.models.Profile`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.

        Example::

            profile = client.profiles.create(
                "My Profile",
                browser_type=1,
                os_type=1,
                timezone_base_on_ip=True,
                is_language_base_on_ip=True,
            )
        """
        body: dict = {
            "name": name,
            "browser_type": browser_type,
            "browser_version": browser_version,
            "os_type": os_type,
            "fixed_webrtc_public_ip": fixed_webrtc_public_ip,
            "timezone_base_on_ip": timezone_base_on_ip,
            "is_language_base_on_ip": is_language_base_on_ip,
        }
        # Include optional fields only when provided to avoid overriding defaults.
        if group_id is not None:
            body["group_id"] = group_id
        if raw_proxy is not None:
            body["raw_proxy"] = raw_proxy
        if custom_user_agent is not None:
            body["custom_user_agent"] = custom_user_agent
        if task_bar_title is not None:
            body["task_bar_title"] = task_bar_title
        if webrtc_mode is not None:
            body["webrtc_mode"] = webrtc_mode
        if geolocation_mode is not None:
            body["geolocation_mode"] = geolocation_mode
        if canvas_mode is not None:
            body["canvas_mode"] = canvas_mode
        if client_rect_mode is not None:
            body["client_rect_mode"] = client_rect_mode
        if webgl_image_mode is not None:
            body["webgl_image_mode"] = webgl_image_mode
        if webgl_metadata_mode is not None:
            body["webgl_metadata_mode"] = webgl_metadata_mode
        if audio_mode is not None:
            body["audio_mode"] = audio_mode
        if font_mode is not None:
            body["font_mode"] = font_mode
        if timezone is not None:
            body["timezone"] = timezone
        if fixed_language is not None:
            body["fixed_language"] = fixed_language

        raw = self._http.post("profiles/create", body)
        _ensure_success(raw)
        return Profile.from_dict(raw["data"])

    def update(self, id: str, **kwargs) -> Profile:
        """Update an existing browser profile.

        Accepts the same keyword arguments as :meth:`create`, minus ``name``
        which is required positionally here.

        Args:
            id: The profile identifier.
            **kwargs: Profile fields to update (same as :meth:`create` kwargs).

        Returns:
            The updated :class:`~gpm_login_global.models.Profile`.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.post(f"profiles/update/{id}", kwargs)
        _ensure_success(raw)
        return Profile.from_dict(raw["data"])

    def delete(self, id: str, mode: str = "soft") -> None:
        """Delete a browser profile.

        Args:
            id: The profile identifier.
            mode: Deletion mode – ``"soft"`` moves to trash; ``"hard"`` permanently
                removes the profile. Defaults to ``"soft"``.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get(f"profiles/delete/{id}?mode={mode}")
        _ensure_success(raw)

    def start(
        self,
        id: str,
        *,
        remote_debugging_port: int | None = None,
        window_scale: float | None = None,
        window_pos: str | None = None,
        window_size: str | None = None,
        addition_args: str | None = None,
    ) -> StartProfileResult:
        """Start a browser profile and return automation connection details.

        Args:
            id: The profile identifier.
            remote_debugging_port: Chrome DevTools Protocol port. When ``None``
                the server picks a free port automatically.
            window_scale: DPI scaling factor, e.g. ``1.0`` for 100 %.
            window_pos: Initial window position ``"x,y"``, e.g. ``"0,0"``.
            window_size: Initial window size ``"w,h"``, e.g. ``"1280,800"``.
            addition_args: Extra Chrome command-line arguments.

        Returns:
            A :class:`~gpm_login_global.models.StartProfileResult` containing the
            ``remote_debugging_port`` and ``driver_path`` needed to attach a
            WebDriver session.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.

        Example::

            result = client.profiles.start(profile_id, window_size="1280,800")
            print(f"Connect to port {result.remote_debugging_port}")
            print(f"ChromeDriver: {result.driver_path}")
        """
        params: dict = {}
        if remote_debugging_port is not None:
            params["remote_debugging_port"] = remote_debugging_port
        if window_scale is not None:
            params["window_scale"] = window_scale
        if window_pos is not None:
            params["window_pos"] = window_pos
        if window_size is not None:
            params["window_size"] = window_size
        if addition_args is not None:
            params["addition_args"] = addition_args

        raw = self._http.get(f"profiles/start/{id}", params=params or None)
        _ensure_success(raw)
        return StartProfileResult.from_dict(raw["data"])

    def stop(self, id: str) -> None:
        """Stop a running browser profile.

        Args:
            id: The profile identifier.

        Raises:
            RuntimeError: On API failure.
            requests.HTTPError: On a non-2xx HTTP response.
        """
        raw = self._http.get(f"profiles/stop/{id}")
        _ensure_success(raw)
