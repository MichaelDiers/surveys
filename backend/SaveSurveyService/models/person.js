const Base = require('./base');
const emailValidator = require('../validators/email-validator');
const uuidValidator = require('../validators/uuid-validator');

/**
 * Describes the organizer of a survey and is base class for Participant.
 */
class Person extends Base {
  /**
   * Creates a new instance of Person.
   * @param {object} json The object is initialized from that object.
   * @param {string} json.email The email address of the person.
   * @param {string} json.id The id of the person as a v4 guid.
   * @param {string} json.name The name of the person.
   */
  constructor(json) {
    super(json);

    this.email = Base.validate('email', json?.email, emailValidator);
    this.id = Base.validate('id', json?.id, uuidValidator);
    this.name = Base.validate('name', json?.name);
  }
}

module.exports = Person;
