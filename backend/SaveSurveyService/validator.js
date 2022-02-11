const uuid = require('uuid');

// regex for validating emails
const emailRegex = /^([\w-|}~{`^?=/+*'&%$#!]+\.)*[\w-|}~{`^?=/+*'&%$#!]+@[\w-]+\.([\w]+){2,}$/;

/**
 * Input validator.
 */
class Validator {
  /**
   * Creates a new validator object.
   * @param {object} obj The object that is included in error messages.
   */
  constructor(obj) {
    this.obj = JSON.stringify(obj);
  }

  /**
   * Gets value and valueName for the object.
   * @param {object} obj The object to be validated.
   * @returns An object { value, valueName }.
   * @example
   *  const { value, valueName } = new Validator().getKeyValue({ foo: 'bar' });
   *  if (valueName !== 'foo' || value !== 'bar') {
   *    throw new Error();
   *  }
   */
  getKeyValue(obj) {
    if (!obj) {
      this.throwError(obj);
    }

    const key = Object.keys(obj)[0];
    const value = obj[key];
    return { value, valueName: key };
  }

  /**
   * Throws an error.
   * @param {any} value The value that caused the error.
   * @param {string} valueName The name of the parameter.
   * @param {string} message An optional error message.
   */
  throwError(value, valueName, message) {
    throw new Error(`Invalid value for ${valueName} = ${value} - ${message || ''} (${this.obj})`);
  }

  /**
   * Throws an Error if the given object is falsy.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validate(obj) {
    const { value, valueName } = this.getKeyValue(obj);
    if (!value) {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the value is falsy or not an array.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateArray(obj) {
    const { value, valueName } = this.validate(obj);
    if (!value.forEach || !value.map || value.length === 0) {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the value is not true or false.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateBoolean(obj) {
    const { value, valueName } = this.getKeyValue(obj);
    if (value !== true && value !== false) {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the value is an invalid email.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateEmail(obj) {
    const { value, valueName } = this.validateString(obj);
    if (!emailRegex.test(value)) {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the value is not an integer.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateInt(obj) {
    const { value, valueName } = this.getKeyValue(obj);
    if (!Number.isInteger(value)) {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the value is not an integer greater than zero.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateIntGreaterThanZero(obj) {
    const { value, valueName } = this.validateInt(obj);
    if (value <= 0) {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the value is not an object and has no Object.keys.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateIsObject(obj) {
    const { value, valueName } = this.validate(obj);
    if (typeof value !== 'object' || Object.keys(value).length === 0) {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the value is not a string.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateString(obj) {
    const { value, valueName } = this.validate(obj);
    if (typeof value !== 'string') {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the values of the array are not unique.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateUnique(obj) {
    const { value, valueName } = this.validateArray(obj);
    if (new Set(value).size !== value.length) {
      this.throwError(value, valueName, 'Duplicate values');
    }

    return { value, valueName };
  }

  /**
   * Throws an error if the value is not a v4 uuid.
   * @param {object} obj A { key: value } object.
   * @returns An object as { value, valueName: 'key' }.
   */
  validateUuid(obj) {
    const { value, valueName } = this.validateString(obj);
    if (!uuid.validate(value) || uuid.version(value) !== 4) {
      this.throwError(value, valueName);
    }

    return { value, valueName };
  }
}

module.exports = Validator;
