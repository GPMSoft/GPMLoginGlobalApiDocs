/**
 * @file client.js
 * Core HTTP client and main entry point for the GPMLogin Global API.
 */

import { GroupService }     from './services/GroupService.js';
import { ProxyService }     from './services/ProxyService.js';
import { ProfileService }   from './services/ProfileService.js';
import { ExtensionService } from './services/ExtensionService.js';

/**
 * Resolves the best available `fetch` implementation for the current runtime.
 * In Node.js ≥ 18 the global `fetch` is available; older versions must install
 * the `node-fetch` package.
 *
 * @returns {Function} A fetch-compatible function.
 */
async function resolveFetch() {
  if (typeof globalThis.fetch === 'function') {
    return globalThis.fetch.bind(globalThis);
  }
  // Conditional import keeps bundlers from including node-fetch unnecessarily.
  const { default: nodeFetch } = await import('node-fetch');
  return nodeFetch;
}

/**
 * Low-level HTTP helper shared by all services.
 *
 * @param {string} baseUrl - Base URL including the `/api/v1` path segment.
 * @param {Function} fetchFn - The fetch implementation to use.
 * @returns {{ get: Function, post: Function }} Thin HTTP helpers.
 */
function createHttp(baseUrl, fetchFn) {
  /**
   * Performs a GET request and parses the JSON response.
   *
   * @param {string} path - Relative path appended to `baseUrl`.
   * @returns {Promise<object>} Parsed JSON response body.
   */
  async function get(path) {
    const res = await fetchFn(`${baseUrl}/${path}`);
    return res.json();
  }

  /**
   * Performs a POST request with a JSON body and parses the JSON response.
   *
   * @param {string} path - Relative path appended to `baseUrl`.
   * @param {object} body - Request payload, serialised to JSON.
   * @returns {Promise<object>} Parsed JSON response body.
   */
  async function post(path, body) {
    const res = await fetchFn(`${baseUrl}/${path}`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(body),
    });
    return res.json();
  }

  return { get, post };
}

/**
 * Main client for the GPMLogin Global Local API.
 *
 * @example
 * const client = new GPMLoginGlobalClient('http://127.0.0.1:9495');
 * await client.ready; // wait for fetch to be resolved
 * const groups = await client.groups.getAll();
 */
export class GPMLoginGlobalClient {
  /**
   * @param {string} [baseUrl='http://127.0.0.1:9495'] - Base URL of the
   *   GPMLogin Global application. Do **not** include a trailing slash or the
   *   `/api/v1` path segment.
   */
  constructor(baseUrl = 'http://127.0.0.1:9495') {
    const apiBase = `${baseUrl.replace(/\/$/, '')}/api/v1`;

    /**
     * Promise that resolves once the internal fetch implementation is ready.
     * Await this before making API calls in environments that may need the
     * dynamic `node-fetch` import (Node.js < 18).
     *
     * @type {Promise<void>}
     */
    this.ready = resolveFetch().then((fetchFn) => {
      const http = createHttp(apiBase, fetchFn);

      /** @type {GroupService} Service for managing profile groups. */
      this.groups = new GroupService(http);

      /** @type {ProxyService} Service for managing proxies. */
      this.proxies = new ProxyService(http);

      /** @type {ProfileService} Service for managing browser profiles. */
      this.profiles = new ProfileService(http);

      /** @type {ExtensionService} Service for managing extensions. */
      this.extensions = new ExtensionService(http);
    });
  }
}
