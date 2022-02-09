const { expect } = require('chai');

const testHelper = require('../test-helper');
const Question = require('../../models/question');

describe('question.js', () => {
  testHelper.raiseErrorEmptyJson(Question);

  describe('id', () => {
    testHelper.raiseErrorIfNotExists(Question, testHelper.surveyTestData.questions[0], 'id');
    testHelper.passTest(Question, testHelper.surveyTestData.questions[0], 'id');
  });

  describe('question', () => {
    testHelper.raiseErrorIfNotExists(Question, testHelper.surveyTestData.questions[0], 'question');
    testHelper.passTest(Question, testHelper.surveyTestData.questions[0], 'question');
  });

  describe('choices', () => {
    testHelper.raiseErrorIfNotExists(Question, testHelper.surveyTestData.questions[0], 'choices');

    it('valid choices', () => {
      const input = JSON.parse(JSON.stringify(testHelper.surveyTestData.questions[0]));
      const question = new Question(input);
      const expected = JSON.parse(JSON.stringify(testHelper.surveyTestData.questions[0]));
      expect(question.choices.length).to.equal(expected.choices.length);
      expected.choices.forEach((choice) => {
        expect(question.choices.some(
          (actual) => actual.id === choice.id && actual.answer === choice.answer,
        )).to.equal(true);
      });
    });
  });
});
