const { expect } = require('chai');

const testHelper = require('../test-helper');
const Question = require('../../models/question');

describe('question.js', () => {
  testHelper.raiseErrorEmptyJson(Question);

  describe('id', () => {
    testHelper.raiseErrorIfNotExists(Question, testHelper.surveyTestData.questions[0], 'id');
    testHelper.passTest(Question, testHelper.surveyTestData.questions[0], 'id');
  });

  describe('order', () => {
    testHelper.raiseErrorIfNotExists(Question, testHelper.surveyTestData.questions[0], 'order');
    testHelper.passTest(Question, testHelper.surveyTestData.questions[0], 'order');

    describe('check invalid order values', () => {
      [-1, '1'].forEach((order) => {
        it(`order = ${order}`, () => {
          const json = JSON.parse(JSON.stringify(testHelper.surveyTestData.questions[0]));
          json.order = order;
          expect(() => new Question(json)).to.throw(Error, 'Invalid order:');
        });
      });
    });
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
