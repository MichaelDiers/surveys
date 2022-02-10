const Person = require('./person');
const QuestionReference = require('./question-reference');

/**
 * Describes a participant of a survey.
 */
class Participant extends Person {
  /**
   * Creates a new instance of Participant.
   * @param {object} json The object is initialized from that object.
   * @param {string} json.email The email address of the participant.
   * @param {string} json.id The id of the participant as a v4 guid.
   * @param {string} json.name The name of the participant.
   * @param {QuestionReference[]} json.questions The answer suggestions for the participant.
   */
  constructor(json) {
    super(json);

    if (!json.questions || !json.questions.map) {
      throw new Error(`Invalid questions: '${json.questions}'`);
    }

    this.questions = json.questions.map((q) => new QuestionReference(q));
  }
}

module.exports = Participant;
