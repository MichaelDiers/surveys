const Validator = require('../validator');

/**
 * Creates a reference to a question.
 * @param {object} json The question reference is initialized from the given json.
 * @param {string} json.questionId The id of the referenced question.
 * @param {string} json.choiceId The id of the choice of the references question.
 * @param {Validator} validator An input validator.
 */
const create = (json, validator = new Validator(json)) => {
  validator.validate({ json });
  validator.validateIsObject({ json });
  validator.validateUuid({ questionId: json.questionId });
  validator.validateUuid({ choiceId: json.choiceId });

  return {
    questionId: json.questionId,
    choiceId: json.choiceId,
  };
};

module.exports = create;
