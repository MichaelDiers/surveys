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
    it('more suggested ansers than questions throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      testData.participants[0].questions.push({ questionId: uuid.v4(), choiceId: uuid.v4() });
      expect(() => create(testData)).to.throw(Error, 'Missing or too many suggested answers');
    });

    it('less suggested ansers than questions throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      testData.participants[0].questions.pop();
      expect(() => create(testData)).to.throw(Error, 'Missing or too many suggested answers');
    });

    it('question id referenced that does not exists throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      testData.participants[0].questions[0].questionId = uuid.v4();
      expect(() => create(testData)).to.throw(Error, 'referenced questionId does not exists');
    });

    it('choice id referenced that does not exists throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      testData.participants[0].questions[0].choiceId = uuid.v4();
      expect(() => create(testData)).to.throw(Error, 'referenced choiceId does not exists');
    });

    it('duplicate suggested answer for a question throws an error', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      const reference = JSON.parse(JSON.stringify(testData.participants[0].questions[0]));
      testData.participants[0].questions.push(reference);
      expect(() => create(testData)).to.throw(Error, 'Duplicate values');
    });

    it('run example', () => {
      const testData = JSON.parse(JSON.stringify(survey));
      create(testData);
    });
  });
});
