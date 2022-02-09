const { expect } = require('chai');

const testHelper = require('../test-helper');
const uuidValidator = require('../../validators/uuid-validator');

describe('uuid-validator.js', () => {
  testHelper.guids.invalid.forEach((id) => {
    it(`exptect '${id}' to be invalid`, () => {
      expect(uuidValidator(id)).to.equal(false);
    });
  });

  testHelper.guids.valid.forEach((id) => {
    it(`exptect '${id}' to be valid`, () => {
      expect(uuidValidator(id)).to.equal(true);
    });
  });
});
