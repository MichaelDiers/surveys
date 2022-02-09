const uuid = require('uuid');

/**
 * Vaidates a string that represents a v4 guid.
 * @param {string} id The id to be validated.
 * @returns True if the id is valid and false otherwise.
 */
const validate = (id) => {
  if (id) {
    return uuid.validate(id) && uuid.version(id) === 4;
  }

  return false;
};

module.exports = validate;
