const Base = require('./base');
const Participant = require('./participant');
const Person = require('./person');
const Question = require('./question');

const uuidValidator = require('../validators/uuid-validator');

/**
 * Describes the data of a survey.
 */
class Survey extends Base {
  /**
   * Creates a new instance of Survey.
   * @param {object} json The survey is initialized from the given json.
   * @param {string} json.id The id of the survey as a v4 guid.
   * @param {string} json.name The name of the survey.
   * @param {Person} json.organizer The organizer of the survey.
   * @param {Participant[]} json.participants The participants of the survey.
   * @param {Question[]} json.questions The questions of the survey.
   */
  constructor(json) {
    super(json);

    if (!json.participants || !json.participants.map) {
      throw new Error(`Invalid participants: '${json.participants}'`);
    }

    if (!json.questions || !json.questions.map) {
      throw new Error(`Invalid questions: '${json.questions}'`);
    }

    this.id = Base.validate('id', json.id, uuidValidator);
    this.name = Base.validate('name', json.name);
    this.organizer = new Person(json.organizer);
    this.participants = json.participants?.map((p) => new Participant(p));
    this.questions = json.questions?.map((q) => new Question(q));
    this.link = Base.validate('link', json.link);
    this.info = Base.validate('info', json.info);

    this.participants.forEach((participant, i) => {
      for (let j = i + 1; j < this.participants.length; j += 1) {
        if (participant.id === this.participants[j].id) {
          throw new Error(`Invalid participants (duplicate id): ${JSON.stringify(this.participants)}`);
        }
      }
    });

    this.participants.forEach((participant) => {
      participant.questions.forEach(({ questionId, choiceId }) => {
        const question = this.questions.find((q) => q.id === questionId);
        if (!question) {
          throw new Error(`Invalid participants (questionId does not match): ${JSON.stringify(this.participants)}`);
        }

        if (!question.choices.some((choice) => choice.id === choiceId)) {
          throw new Error(`Invalid participants (choiceId does not match): ${JSON.stringify(this.participants)}`);
        }
      });
    });

    this.questions.forEach((question) => {
      question.choices.forEach(({ id }, i) => {
        for (let j = i + 1; j < question.choices.length; j += 1) {
          if (id === question.choices[j].id) {
            throw new Error('Invalid choices (duplicate choiceId):');
          }
        }
      });
    });
  }
}

module.exports = Survey;
