const Base = require('./base');
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
   * @param {Person[]} json.participants The participants of the survey.
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
    this.participants = json.participants?.map((p) => new Person(p));
    this.questions = json.questions?.map((q) => new Question(q));
    this.link = Base.validate('link', json.link);
    this.info = Base.validate('info', json.info);
  }
}

module.exports = Survey;
