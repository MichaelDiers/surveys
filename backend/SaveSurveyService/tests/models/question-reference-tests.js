/**
 * Tests for creating question references.
 */
const { expect } = require('chai');
const create = require('../../models/question-reference');
const {
  data: {
    questionReference,
  },
  createFromEmptyParameterThrowsError,
  createFromEmptyThrowsError,
  createFromNonObjectThrowsError,
  serializeTest,
  usingInvalidUuidsThrowsAnError,
} = require('../test-helper');

describe('question-refrence.js', () => {
  createFromEmptyThrowsError(create);
  createFromEmptyParameterThrowsError(create, questionReference, 'questionId', 'choiceId');
  createFromNonObjectThrowsError(create);

  describe('using invalid types for paramters throws an error', () => {
    it('using boolean for questionId throws an error', () => {
      const testData = JSON.parse(JSON.stringify(questionReference));
      testData.questionId = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for questionId = true');
    });

    it('using boolean for choiceId throws an error', () => {
      const testData = JSON.parse(JSON.stringify(questionReference));
      testData.choiceId = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for choiceId = true');
    });
  });

  usingInvalidUuidsThrowsAnError(create, questionReference, 'questionId');
  usingInvalidUuidsThrowsAnError(create, questionReference, 'choiceId');
  serializeTest(create, questionReference);
});
