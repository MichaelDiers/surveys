const { expect } = require('chai');
const uuid = require('uuid');

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

  describe('dependencies', () => {
    describe('check participant ids', () => {
      it('participant ids are unique', () => {
        const data = JSON.parse(JSON.stringify(testHelper.surveyTestData));
        expect(() => new Survey(data)).not.to.throw(Error);
      });

      it('participant ids are not unique', () => {
        const data = JSON.parse(JSON.stringify(testHelper.surveyTestData));
        data.participants.push(data.participants[0]);
        expect(() => new Survey(data)).to.throw(Error, 'Invalid participants (duplicate id):');
      });
    });

    describe('check participant default answers', () => {
      it('participant answer matches questions', () => {
        const data = JSON.parse(JSON.stringify(testHelper.surveyTestData));
        expect(() => new Survey(data)).not.to.throw(Error);
      });

      it('questionId does not match questions', () => {
        const data = JSON.parse(JSON.stringify(testHelper.surveyTestData));
        data.participants[0].questions[1].questionId = uuid.v4();

        expect(() => new Survey(data)).to.throw(Error, 'Invalid participants (questionId does not match):');
      });

      it('choiceId does not match questions', () => {
        const data = JSON.parse(JSON.stringify(testHelper.surveyTestData));
        data.participants[0].questions[1].choiceId = uuid.v4();

        expect(() => new Survey(data)).to.throw(Error, 'Invalid participants (choiceId does not match):');
      });
    });

    describe('check question choices', () => {
      it('duplicate choice id', () => {
        const data = JSON.parse(JSON.stringify(testHelper.surveyTestData));
        data.questions[1].choices[1].id = data.questions[1].choices[0].id;

        expect(() => new Survey(data)).to.throw(Error, 'Invalid choices (duplicate choiceId):');
      });
    });
  });
});
