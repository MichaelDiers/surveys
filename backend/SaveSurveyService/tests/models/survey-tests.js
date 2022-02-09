const { expect } = require('chai');

const testHelper = require('../test-helper');
const Survey = require('../../models/survey');

describe('survey.js', () => {
  testHelper.raiseErrorEmptyJson(Survey);

  describe('id', () => {
    testHelper.raiseErrorIfNotExists(Survey, testHelper.surveyTestData, 'id');
    testHelper.testIds(Survey, testHelper.surveyTestData);
  });

  describe('name', () => {
    testHelper.raiseErrorIfNotExists(Survey, testHelper.surveyTestData, 'name');
    testHelper.passTest(Survey, testHelper.surveyTestData, 'name');
  });

  describe('info', () => {
    testHelper.raiseErrorIfNotExists(Survey, testHelper.surveyTestData, 'info');
    testHelper.passTest(Survey, testHelper.surveyTestData, 'info');
  });

  describe('link', () => {
    testHelper.raiseErrorIfNotExists(Survey, testHelper.surveyTestData, 'link');
    testHelper.passTest(Survey, testHelper.surveyTestData, 'link');
  });

  describe('organizer', () => {
    testHelper.raiseErrorIfNotExists(Survey, testHelper.surveyTestData, 'organizer', 'Person: Empty json.');

    it('set valid organizer', () => {
      const json = JSON.parse(JSON.stringify(testHelper.surveyTestData));
      const survey = new Survey(json);
      expect(survey.organizer.email).to.equal(json.organizer.email);
      expect(survey.organizer.id).to.equal(json.organizer.id);
      expect(survey.organizer.name).to.equal(json.organizer.name);
    });
  });

  describe('participants', () => {
    testHelper.raiseErrorIfNotExists(
      Survey,
      testHelper.surveyTestData,
      'participants',
      'Invalid participants:',
    );

    it('set valid participants', () => {
      const json = JSON.parse(JSON.stringify(testHelper.surveyTestData));
      const survey = new Survey(json);
      json.participants.forEach((p) => {
        expect(survey.participants.some(
          (sp) => sp.email === p.email && sp.id === p.id && sp.name === p.name,
        ))
          .to.equal(true);
      });

      expect(json.participants.length).to.equal(survey.participants.length);
    });
  });

  describe('questions', () => {
    testHelper.raiseErrorIfNotExists(
      Survey,
      testHelper.surveyTestData,
      'questions',
      'Invalid questions:',
    );

    it('set valid questions', () => {
      const json = JSON.parse(JSON.stringify(testHelper.surveyTestData));
      const survey = new Survey(json);
      json.questions.forEach((q) => {
        expect(survey.questions.some(
          (sq) => sq.question === q.question && sq.id === q.id,
        ))
          .to.equal(true);
      });

      expect(json.questions.length).to.equal(survey.questions.length);
    });
  });

  describe('timestamp', () => {
    it('timestamp is set', () => {
      // eslint-disable-next-line no-unused-expressions
      expect(new Survey(testHelper.surveyTestData).timestamp).to.be.not.undefined;
    });
  });
});
