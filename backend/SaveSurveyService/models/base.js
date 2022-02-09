/**
 * Base class for all models.
 */
class Base {
  /**
   * Creates a new instance of Base.
   * @param {object} json The object is validated.
   * @throws Error if the json is invalid.
   */
  constructor(json) {
    if (!json) {
      throw new Error(`${this.constructor.name}: Empty json.`);
    }
  }

  /**
   * Validate a property.
   * @param {string} propertyName The name of the property to be validated.
   * @param {any} value The value of the property.
   * @param {function} validateFunc An additional validation function '(value) => true'.
   * @returns The value if the validation succeeds. If the validation fails
   *  an Error is thrown.
   */
  static validate(propertyName, value, validateFunc) {
    if (value && (!validateFunc || validateFunc(value))) {
      return value;
    }

    throw new Error(`Invalid ${propertyName}: '${value}'`);
  }
}

module.exports = Base;
