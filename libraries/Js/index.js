/**
 * @file index.js
 * GPMLogin Global JavaScript client library – public surface.
 *
 * @module gpm-login-global
 * @example
 * import { GPMLoginGlobalClient } from 'gpm-login-global';
 *
 * const client = new GPMLoginGlobalClient();
 * const profiles = await client.profiles.getAll();
 */

export { GPMLoginGlobalClient } from './src/client.js';
export { GroupService }     from './src/services/GroupService.js';
export { ProxyService }     from './src/services/ProxyService.js';
export { ProfileService }   from './src/services/ProfileService.js';
export { ExtensionService } from './src/services/ExtensionService.js';
