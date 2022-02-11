const createChoice = require('./choice');
const Validator = require('../validator');

/**
 * Creates a question object.
 * @param {object} json The object is initialized from the given json.
 * @param {object[]} json.choices The choices of the question.
 * @param {string} json.id The id of the question as a v4 guid.
 * @param {string} json.question The text of the question.
 * @param {int} json.order Used for sorting questions.
 */
const create = (json, validator = new Validator(json)) => {
  validator.validate({ json });
  validator.validateIsObject({ json });
  validator.validateArray({ choices: json.choices });
  validator.validateUuid({ id: json.id });
  validator.validateString({ question: json.question });
  validator.validateIntGreaterThanZero({ order: json.order });

  const question = {
    choices: json.choices.map((choice) => createChoice(choice)),
    id: json.id,
    question: json.question,
    order: json.order,
  };

  validator.validateUnique({ choices: question.choices.map((c) => c.id) });

  return question;
};

module.exports = create;
