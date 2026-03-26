"""
services
~~~~~~~~

Service classes that wrap individual GPMLogin Global API resource groups.
"""

from gpm_login_global.services.group_service import GroupService
from gpm_login_global.services.proxy_service import ProxyService
from gpm_login_global.services.profile_service import ProfileService
from gpm_login_global.services.extension_service import ExtensionService

__all__ = ["GroupService", "ProxyService", "ProfileService", "ExtensionService"]
