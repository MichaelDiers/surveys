const createParticipant = require('./participant');
const createPerson = require('./person');
const createQuestion = require('./question');
const Validator = require('../validator');

/**
 * Creates a survey object.
 * @param {object} json The survey is initialized from the given json.
 * @param {string} json.id The id of the survey as a v4 guid.
 * @param {string} json.info A description of the survey.Base
 * @param {string} json.link A link to additional survey information.
 * @param {string} json.name The name of the survey.
 * @param {object} json.organizer The organizer of the survey.
 * @param {object[]} json.participants The participants of the survey.
 * @param {object[]} json.questions The questions of the survey.
 * @param {Validator} validator An input validator.
 */
const create = (json, validator = new Validator(json)) => {
  validator.validateIsObject({ json });
  validator.validateUuid({ id: json.id });
  validator.validateString({ info: json.info });
  validator.validateString({ link: json.link });
  validator.validateString({ name: json.name });
  validator.validateArray({ participants: json.participants });
  validator.validateArray({ questions: json.questions });
  validator.validateIsObject({ organizer: json.organizer });

  const survey = {
    id: json.id,
    info: json.info,
    link: json.link,
    name: json.name,
    organizer: createPerson(json.organizer),
    participants: json.participants.map((p) => createParticipant(p)),
    questions: json.questions.map((q) => createQuestion(q)),
  };

  survey.participants.forEach((participant) => {
    if (participant.questions.length !== survey.questions.length) {
      validator.throwError(participant, 'participant', 'questions references and questions do not match');
    }

    participant.questions.forEach((qr) => {
      const question = survey.questions.find((q) => q.id === qr.questionId);
      if (!question) {
        validator.throwError(participant, 'participant', 'unknown reference to question id');
      }

      if (!question.choices.some((q) => q.choiceId === qr.choiceId)) {
        validator.throwError(participant, 'participant', 'unknown reference to choice id');
      }
    });
  });

  return survey;
};

module.exports = create;
