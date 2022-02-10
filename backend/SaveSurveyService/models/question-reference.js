const Base = require('./base');
const uuidValidator = require('../validators/uuid-validator');

/**
 * Describes a suggested answer for a survey question.
 */
class QuestionReference extends Base {
  /**
   * Creates a new instance of QuestionReference.
   * @param {object} json The question reference is initialized from that object.
   * @param {string} json.questionId The id of the referenced question.
   * @param {string} json.choiceId The id of the choice of the references question.
   */
  constructor(json) {
    super(json);

    this.questionId = Base.validate('questionId', json.questionId, uuidValidator);
    this.choiceId = Base.validate('choiceId', json.choiceId, uuidValidator);
  }
}

module.exports = QuestionReference;
