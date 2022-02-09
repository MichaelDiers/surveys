const { expect } = require('chai');
const emailValidator = require('../../validators/email-validator');

describe('email-validator.js', () => {
  [
    null,
    undefined,
    '',
    '.@bar.de',
    '.foo@bar.de',
    'foo..@bar.de',
    'foo.@bar.de',
    'foo@bar.',
    'foo@bar.d',
    'foo@.',
    'foo.bar',
    'foo@.bar.de',
    'foo(@bar.de',
    'foo)@bar.de',
  ].forEach((email) => {
    it(`exptect '${email}' to be invalid`, () => {
      expect(emailValidator(email)).to.equal(false);
    });
  });

  [
    'foo@bar.de',
    'foo.bar@bar.de',
    'foo.bar.foo@bar.de',
    'foo123@bar.de',
    '123@bar.de',
    '123-321@bar.de',
    '123-321-123@bar.de',
    '-@bar.de',
    '--@bar.de',
    'foo!@bar.de',
    'foo#@bar.de',
    'foo$@bar.de',
    'foo%@bar.de',
    'foo&@bar.de',
    'foo\'@bar.de',
    'foo*@bar.de',
    'foo+@bar.de',
    'foo-@bar.de',
    'foo/@bar.de',
    'foo=@bar.de',
    'foo?@bar.de',
    'foo^@bar.de',
    'foo_@bar.de',
    'foo`@bar.de',
    'foo{@bar.de',
    'foo|@bar.de',
    'foo}@bar.de',
    'foo~@bar.de',
    'foo@bar-foo.de',
    'foo@bar_77foo.de',
  ].forEach((email) => {
    it(`exptect '${email}' to be valid`, () => {
      expect(emailValidator(email)).to.equal(true);
    });
  });
});
