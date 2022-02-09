const Base = require('./base');
const Choice = require('./choice');
const uuidValidator = require('../validators/uuid-validator');

/**
 * Describes a question of a survey.
 */
class Question extends Base {
  /**
   * Creates a new instance of Question.
   * @param {object} json The object is initialized from the given json.
   * @param {Choice[]} json.choices The choices of the question.
   * @param {string} json.id The id of the question as a v4 guid.
   * @param {string} json.question The text of the question.
   */
  constructor(json) {
    super(json);

    if (!json.choices || !json.choices.map) {
      throw new Error(`Invalid choices: '${json.choices}'`);
    }

    this.choices = json.choices.map((choice) => new Choice(choice));
    this.id = Base.validate('id', json?.id, uuidValidator);
    this.question = Base.validate('question', json?.question);
  }
}

module.exports = Question;
