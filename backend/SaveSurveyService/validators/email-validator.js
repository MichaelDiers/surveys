// regualar expression for validating emails
const regex = /^([\w-|}~{`^?=/+*'&%$#!]+\.)*[\w-|}~{`^?=/+*'&%$#!]+@[\w-]+\.([\w]+){2,}$/;

/**
 * Validate an email address.
 * @param {string} email The email to be validated.
 * @returns True if the email is valid and false otherwise.
 */
const validate = (email) => {
  if (email) {
    return regex.test(email);
  }

  return false;
};

module.exports = validate;
