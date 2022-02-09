/**
 * Tests for class Choice.
 */

const Choice = require('../../models/choice');
const testHelper = require('../test-helper');

describe('choice.js', () => {
  testHelper.raiseErrorEmptyJson(Choice);

  describe('id', () => {
    testHelper.raiseErrorIfNotExists(Choice, testHelper.surveyTestData.questions[0].choices[0], 'id');
    testHelper.testIds(Choice, testHelper.surveyTestData.questions[0].choices[0]);
  });

  describe('answer', () => {
    testHelper.raiseErrorIfNotExists(Choice, testHelper.surveyTestData.questions[0].choices[0], 'answer');
    testHelper.passTest(Choice, testHelper.surveyTestData.questions[0].choices[0], 'answer');
  });
});
