/**
 * Tests for creating surveys.
 */
const { expect } = require('chai');
const uuid = require('uuid');
const create = require('../../models/survey');
const {
  data: {
    survey,
  },
  createFromEmptyParameterThrowsError,
  createFromEmptyThrowsError,
  createFromNonObjectThrowsError,
  usingInvalidUuidsThrowsAnError,
} = require('../test-helper');

describe('survey.js', () => {
  createFromEmptyThrowsError(create);
  createFromEmptyParameterThrowsError(create, survey, ...Object.keys(survey));
  createFromNonObjectThrowsError(create);

  describe('using invalid types for paramters throws an error', () => {
    Object.keys(survey).forEach((parameter) => {
      it(`using boolean for ${parameter} throws an error`, () => {
        const testData = JSON.parse(JSON.stringify(survey));
        testData[parameter] = true;
        expect(() => create(testData)).to.throw(Error, 'Invalid value for ');
      });
    });
  });

  usingInvalidUuidsThrowsAnError(create, survey, 'id');

  describe('cross check data', () => {
    it('missing suggested answer for a question throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      testData.participants[0].questions.pop();
      expect(() => create(testData)).to.throw(Error, 'questions references and questions do not match');
    });

    it('additional suggested answer for an unknown question throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      const reference = JSON.parse(JSON.stringify(testData.participants[0].questions[0]));
      reference.questionId = uuid.v4();
      testData.participants[0].questions.push(reference);
      expect(() => create(testData)).to.throw(Error, 'questions references and questions do not match');
    });

    it('duplicate suggested answer for a question throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      const reference = JSON.parse(JSON.stringify(testData.participants[0].questions[0]));
      testData.participants[0].questions.push(reference);
      expect(() => create(testData)).to.throw(Error, 'Duplicate values');
    });

    it('unknown choiceId for a question throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      testData.participants[0].questions[0].choiceId = uuid.v4();
      expect(() => create(testData)).to.throw(Error, 'unknown reference to choice id');
    });
  });
});
