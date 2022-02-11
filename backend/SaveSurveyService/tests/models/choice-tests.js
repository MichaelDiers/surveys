/**
 * Tests for creating choices.
 */
const { expect } = require('chai');
const create = require('../../models/choice');
const {
  data: {
    choice,
  },
  createFromEmptyParameterThrowsError,
  createFromEmptyThrowsError,
  createFromNonObjectThrowsError,
  serializeTest,
  usingInvalidUuidsThrowsAnError,
} = require('../test-helper');

describe('choice.js', () => {
  createFromEmptyThrowsError(create);
  createFromEmptyParameterThrowsError(create, choice, 'answer', 'id', 'selectable');
  createFromNonObjectThrowsError(create);

  describe('using invalid types for paramters throws an error', () => {
    it('using boolean for answer throws an error', () => {
      const testData = JSON.parse(JSON.stringify(choice));
      testData.answer = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for answer = ');
    });

    it('using boolean for id throws an error', () => {
      const testData = JSON.parse(JSON.stringify(choice));
      testData.id = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for id = ');
    });

    it('using string for selectable throws an error', () => {
      const testData = JSON.parse(JSON.stringify(choice));
      testData.selectable = 'true';
      expect(() => create(testData)).to.throw(Error, 'Invalid value for selectable = ');
    });
  });

  usingInvalidUuidsThrowsAnError(create, choice, 'id');
  serializeTest(create, choice);
});
