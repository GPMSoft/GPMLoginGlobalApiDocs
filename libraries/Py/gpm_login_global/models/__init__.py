"""
models
~~~~~~

Dataclasses representing the entities returned by the GPMLogin Global API.
"""

from __future__ import annotations

from dataclasses import dataclass, field
from typing import Any, Generic, TypeVar

T = TypeVar("T")


# ---------------------------------------------------------------------------
# Generic response wrappers
# ---------------------------------------------------------------------------


@dataclass
class ApiResponse(Generic[T]):
    """Standard envelope returned by every API endpoint.

    Attributes:
        success: Whether the request succeeded.
        data: The response payload; ``None`` on failure.
        message: Human-readable status message, e.g. ``"OK"``.
        sender: Server identifier, e.g. ``"GPMLoginGlobal v1.0.0"``.
    """

    success: bool
    data: T | None
    message: str
    sender: str

    @classmethod
    def from_dict(cls, raw: dict, data_factory=None) -> "ApiResponse":
        """Deserialise a raw API response dict.

        Args:
            raw: Parsed JSON dict from the API.
            data_factory: Callable that converts ``raw["data"]`` to the desired
                type. If ``None``, ``data`` is kept as a plain dict.

        Returns:
            An :class:`ApiResponse` instance.
        """
        data = raw.get("data")
        if data_factory is not None and data is not None:
            data = data_factory(data)
        return cls(
            success=raw.get("success", False),
            data=data,
            message=raw.get("message", ""),
            sender=raw.get("sender", ""),
        )


@dataclass
class PagedData(Generic[T]):
    """Wraps a paginated list of items.

    Attributes:
        current_page: Current page number (1-based).
        per_page: Items per page.
        total: Total number of items across all pages.
        last_page: Index of the last page.
        data: Items on the current page.
    """

    current_page: int
    per_page: int
    total: int
    last_page: int
    data: list[T]

    @classmethod
    def from_dict(cls, raw: dict, item_factory) -> "PagedData":
        """Deserialise a paginated response dict.

        Args:
            raw: Parsed JSON dict with pagination fields.
            item_factory: Callable that converts each raw item dict to a model.

        Returns:
            A :class:`PagedData` instance.
        """
        return cls(
            current_page=raw.get("current_page", 1),
            per_page=raw.get("per_page", 30),
            total=raw.get("total", 0),
            last_page=raw.get("last_page", 1),
            data=[item_factory(item) for item in raw.get("data", [])],
        )


# ---------------------------------------------------------------------------
# Domain models
# ---------------------------------------------------------------------------


@dataclass
class Group:
    """A profile group.

    Attributes:
        id: Unique identifier.
        name: Display name.
        order: Sort order in the UI.
    """

    id: str
    name: str
    order: int

    @classmethod
    def from_dict(cls, raw: dict) -> "Group":
        """Create a :class:`Group` from a raw API dict."""
        return cls(id=raw["id"], name=raw["name"], order=raw.get("order", 0))


@dataclass
class ProxyTag:
    """A label associated with a proxy.

    Attributes:
        id: Unique identifier.
        name: Tag label.
    """

    id: str
    name: str

    @classmethod
    def from_dict(cls, raw: dict) -> "ProxyTag":
        """Create a :class:`ProxyTag` from a raw API dict."""
        return cls(id=raw["id"], name=raw["name"])


@dataclass
class Proxy:
    """A proxy entry.

    Attributes:
        id: Unique identifier.
        raw_proxy: Connection string, e.g. ``http://user:pass@host:port``.
        tags: Labels associated with the proxy.
    """

    id: str
    raw_proxy: str
    tags: list[ProxyTag] = field(default_factory=list)

    @classmethod
    def from_dict(cls, raw: dict) -> "Proxy":
        """Create a :class:`Proxy` from a raw API dict."""
        return cls(
            id=raw["id"],
            raw_proxy=raw.get("raw_proxy", ""),
            tags=[ProxyTag.from_dict(t) for t in raw.get("tags", [])],
        )


@dataclass
class Profile:
    """A browser profile.

    Attributes:
        id: Unique identifier.
        name: Display name.
        group_id: Assigned group identifier, or ``None``.
        raw_proxy: Proxy connection string, or ``None``.
        browser_type: Browser type identifier.
        browser_version: Pinned browser version string.
        os_type: Operating system type identifier.
        custom_user_agent: Custom User-Agent, or ``None``.
        task_bar_title: Browser window title, or ``None``.
        webrtc_mode: WebRTC protection mode.
        fixed_webrtc_public_ip: Fixed public IP for WebRTC.
        geolocation_mode: Geolocation spoofing mode.
        canvas_mode: Canvas fingerprint protection mode.
        client_rect_mode: ClientRects protection mode.
        webgl_image_mode: WebGL image protection mode.
        webgl_metadata_mode: WebGL metadata protection mode.
        audio_mode: Audio fingerprint protection mode.
        font_mode: Font fingerprint protection mode.
        timezone_base_on_ip: Derive timezone from proxy IP.
        timezone: Fixed timezone string.
        is_language_base_on_ip: Derive language from proxy IP.
        fixed_language: Fixed language tag.
    """

    id: str
    name: str
    group_id: str | None = None
    raw_proxy: str | None = None
    browser_type: int = 1
    browser_version: str = ""
    os_type: int = 1
    custom_user_agent: str | None = None
    task_bar_title: str | None = None
    webrtc_mode: int | None = None
    fixed_webrtc_public_ip: str = ""
    geolocation_mode: int | None = None
    canvas_mode: int | None = None
    client_rect_mode: int | None = None
    webgl_image_mode: int | None = None
    webgl_metadata_mode: int | None = None
    audio_mode: int | None = None
    font_mode: int | None = None
    timezone_base_on_ip: bool = True
    timezone: str | None = None
    is_language_base_on_ip: bool = True
    fixed_language: str | None = None

    @classmethod
    def from_dict(cls, raw: dict) -> "Profile":
        """Create a :class:`Profile` from a raw API dict."""
        return cls(
            id=raw["id"],
            name=raw.get("name", ""),
            group_id=raw.get("group_id"),
            raw_proxy=raw.get("raw_proxy"),
            browser_type=raw.get("browser_type", 1),
            browser_version=raw.get("browser_version", ""),
            os_type=raw.get("os_type", 1),
            custom_user_agent=raw.get("custom_user_agent"),
            task_bar_title=raw.get("task_bar_title"),
            webrtc_mode=raw.get("webrtc_mode"),
            fixed_webrtc_public_ip=raw.get("fixed_webrtc_public_ip", ""),
            geolocation_mode=raw.get("geolocation_mode"),
            canvas_mode=raw.get("canvas_mode"),
            client_rect_mode=raw.get("client_rect_mode"),
            webgl_image_mode=raw.get("webgl_image_mode"),
            webgl_metadata_mode=raw.get("webgl_metadata_mode"),
            audio_mode=raw.get("audio_mode"),
            font_mode=raw.get("font_mode"),
            timezone_base_on_ip=raw.get("timezone_base_on_ip", True),
            timezone=raw.get("timezone"),
            is_language_base_on_ip=raw.get("is_language_base_on_ip", True),
            fixed_language=raw.get("fixed_language"),
        )


@dataclass
class StartProfileAdditionInfo:
    """Extra runtime details included in the start-profile response.

    Attributes:
        process_id: OS process ID of the browser.
        profile_name: Display name of the started profile.
        window_handle: Native window handle (Windows HWND).
    """

    process_id: int
    profile_name: str
    window_handle: int

    @classmethod
    def from_dict(cls, raw: dict) -> "StartProfileAdditionInfo":
        """Create from a raw dict."""
        return cls(
            process_id=raw.get("process_id", 0),
            profile_name=raw.get("profile_name", ""),
            window_handle=raw.get("window_handle", 0),
        )


@dataclass
class StartProfileResult:
    """Data returned after a profile is successfully started.

    Attributes:
        profile_id: The profile that was started.
        driver_path: Absolute path to the matching ChromeDriver executable.
        remote_debugging_port: Chrome DevTools Protocol port.
        addition_info: Extra runtime details.
    """

    profile_id: str
    driver_path: str
    remote_debugging_port: int
    addition_info: StartProfileAdditionInfo | None = None

    @classmethod
    def from_dict(cls, raw: dict) -> "StartProfileResult":
        """Create from a raw dict."""
        addition_raw = raw.get("addition_info")
        return cls(
            profile_id=raw.get("profile_id", ""),
            driver_path=raw.get("driver_path", ""),
            remote_debugging_port=raw.get("remote_debugging_port", 0),
            addition_info=(
                StartProfileAdditionInfo.from_dict(addition_raw)
                if addition_raw
                else None
            ),
        )


@dataclass
class Extension:
    """A browser extension managed by GPMLogin Global.

    Attributes:
        id: Unique identifier.
        name: Display name.
        active: Whether the extension is currently enabled.
    """

    id: str
    name: str
    active: bool

    @classmethod
    def from_dict(cls, raw: dict) -> "Extension":
        """Create from a raw dict."""
        return cls(id=raw["id"], name=raw.get("name", ""), active=raw.get("active", False))


__all__ = [
    "ApiResponse",
    "PagedData",
    "Group",
    "ProxyTag",
    "Proxy",
    "Profile",
    "StartProfileAdditionInfo",
    "StartProfileResult",
    "Extension",
]
