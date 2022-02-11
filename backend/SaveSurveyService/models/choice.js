const Validator = require('../validator');

/**
 * Creates a choice object.
 * @param {object} json The choice is initialized from the given json.
 * @param {string} json.answer The text of the answer.
 * @param {string} json.id The id of the choice. A string that represents a v4 guid.
 * @param {boolean} json.selectable Indicates if the answer is a valid answer or an info text.
 * @param {Validator} validator An input validator.
 */
const create = (json, validator = new Validator(json)) => {
  validator.validate({ json });
  validator.validateIsObject({ json });
  validator.validateString({ answer: json.answer });
  validator.validateUuid({ id: json.id });
  validator.validateBoolean({ selectable: json.selectable });

  return {
    answer: json.answer,
    id: json.id,
    selectable: json.selectable,
  };
};

module.exports = create;
