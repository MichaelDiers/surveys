const Validator = require('../validator');

/**
 * Creates a person object.
 * @param {object} json The object is initialized from that object.
 * @param {string} json.email The email address of the person.
 * @param {string} json.id The id of the person as a v4 guid.
 * @param {string} json.name The name of the person.
 * @param {Validator} validator An input validator.
 */
const create = (json, validator = new Validator(json)) => {
  validator.validate({ json });
  validator.validateIsObject({ json });
  validator.validateEmail({ email: json.email });
  validator.validateUuid({ id: json.id });
  validator.validateString({ name: json.name });

  return {
    email: json.email,
    id: json.id,
    name: json.name,
  };
};

module.exports = create;
