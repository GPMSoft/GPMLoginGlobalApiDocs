/**
 * @file GroupService.js
 * Service for managing profile groups via the GPMLogin Global API.
 */

/**
 * Asserts that `response.success` is truthy, throwing otherwise.
 *
 * @param {object} response - Parsed API response envelope.
 * @throws {Error} When `response.success` is falsy.
 */
function ensureSuccess(response) {
  if (!response.success) {
    throw new Error(`GPMLogin API error: ${response.message}`);
  }
}

/**
 * Provides CRUD operations for profile groups.
 */
export class GroupService {
  /**
   * @param {{ get: Function, post: Function }} http - Shared HTTP helper.
   */
  constructor(http) {
    this._http = http;
  }

  /**
   * Retrieves all profile groups.
   *
   * @returns {Promise<Array<object>>} Array of group objects.
   * @throws {Error} On API failure.
   *
   * @example
   * const groups = await client.groups.getAll();
   */
  async getAll() {
    const response = await this._http.get('groups');
    ensureSuccess(response);
    return response.data;
  }

  /**
   * Retrieves a single group by its identifier.
   *
   * @param {string} id - The group identifier.
   * @returns {Promise<object>} The group object.
   * @throws {Error} On API failure.
   */
  async getById(id) {
    const response = await this._http.get(`groups/${id}`);
    ensureSuccess(response);
    return response.data;
  }

  /**
   * Creates a new profile group.
   *
   * @param {object} params - Group parameters.
   * @param {string} params.name - Display name of the group.
   * @param {number} [params.order] - Optional sort order.
   * @returns {Promise<object>} The newly created group.
   * @throws {Error} On API failure.
   *
   * @example
   * const group = await client.groups.create({ name: 'My Group', order: 1 });
   */
  async create({ name, order }) {
    const body = { name };
    if (order !== undefined) {
      body.order = order;
    }
    const response = await this._http.post('groups/create', body);
    ensureSuccess(response);
    return response.data;
  }

  /**
   * Updates an existing profile group.
   *
   * @param {string} id - The group identifier.
   * @param {object} params - Updated group parameters.
   * @param {string} params.name - New display name.
   * @param {number} [params.order] - New sort order.
   * @returns {Promise<object>} The updated group.
   * @throws {Error} On API failure.
   */
  async update(id, { name, order }) {
    const body = { name };
    if (order !== undefined) {
      body.order = order;
    }
    const response = await this._http.post(`groups/update/${id}`, body);
    ensureSuccess(response);
    return response.data;
  }

  /**
   * Deletes a profile group by its identifier.
   *
   * @param {string} id - The group identifier.
   * @returns {Promise<void>}
   * @throws {Error} On API failure.
   */
  async delete(id) {
    const response = await this._http.get(`groups/delete/${id}`);
    ensureSuccess(response);
  }
}
