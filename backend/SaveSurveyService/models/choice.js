const Base = require('./base');
const uuidValidator = require('../validators/uuid-validator');

/**
 * Describes a choice of a survey question.
 */
class Choice extends Base {
  /**
   * Creates a new instance of Choice.
   * @param {object} json The choice is initialized from that object.
   * @param {string} json.answer The text of the answer.
   * @param {string} json.id The id of the choice. A string that represents a v4 guid.
   */
  constructor(json) {
    super(json);

    this.answer = Base.validate('answer', json?.answer);
    this.id = Base.validate('id', json?.id, uuidValidator);
  }
}

module.exports = Choice;
