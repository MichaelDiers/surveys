const createPerson = require('./person');
const createQuestionReference = require('./question-reference');
const Validator = require('../validator');

/**
 * Creates a participant object.
 * @param {object} json The object is initialized from that object.
 * @param {string} json.email The email address of the participant.
 * @param {string} json.id The id of the participant as a v4 guid.
 * @param {string} json.name The name of the participant.
 * @param {object[]} json.questions The answer suggestions for the participant.
 * @param {Validator} validator An input validator.
*/
const create = (json, validator = new Validator(json)) => {
  const participant = createPerson(json);

  validator.validateArray({ questions: json.questions });

  participant.questions = json.questions.map((q) => createQuestionReference(q));

  validator.validateUnique({ questions: participant.questions.map((q) => q.questionId) });

  return participant;
};

module.exports = create;
