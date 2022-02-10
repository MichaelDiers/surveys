const { expect } = require('chai');

const testHelper = require('../test-helper');
const Participant = require('../../models/participant');

describe('participant.js', () => {
  describe('empty json', () => {
    [undefined, null, ''].forEach((json) => {
      it(`empty json: ${json}`, () => {
        expect(() => new Participant(json)).to.throw(Error, 'Participant: Empty json.');
      });
    });
  });

  describe('email', () => {
    testHelper.raiseErrorIfNotExists(Participant, testHelper.surveyTestData.participants[0], 'email');

    it('raise error for invalid email', () => {
      const participant = JSON.parse(JSON.stringify(testHelper.surveyTestData.participants[0]));
      participant.email = 'email@email';
      expect(() => new Participant(participant)).throws(Error, 'Invalid email: \'email@email\'');
    });

    it('create participant with valid email', () => {
      const participant = JSON.parse(JSON.stringify(testHelper.surveyTestData.participants[0]));
      participant.email = 'email@email.de';
      expect(new Participant(participant).email).to.equal('email@email.de');
    });
  });

  describe('id', () => {
    testHelper.raiseErrorIfNotExists(Participant, testHelper.surveyTestData.participants[0], 'id');
    testHelper.testIds(Participant, testHelper.surveyTestData.participants[0]);
  });

  describe('name', () => {
    testHelper.raiseErrorIfNotExists(Participant, testHelper.surveyTestData.participants[0], 'name');

    it('set valid name', () => {
      const participant = JSON.parse(JSON.stringify(testHelper.surveyTestData.participants[0]));
      const name = 'new name';
      participant.name = name;
      expect(new Participant(participant).name).to.equal(name);
    });
  });

  describe('questions', () => {
    testHelper.raiseErrorIfNotExists(Participant, testHelper.surveyTestData.participants[0], 'questions');

    it('set valid questions', () => {
      const data = JSON.parse(JSON.stringify(testHelper.surveyTestData.participants[0]));

      const actual = new Participant(data);
      const expected = JSON.parse(JSON.stringify(testHelper.surveyTestData.participants[0]));

      expect(actual.questions.length).to.equal(expected.questions.length);
      actual.questions.forEach((aq) => {
        expect(expected.questions.some(
          (eq) => eq.questionId === aq.questionId && eq.choiceId === aq.choiceId,
        ));
      });
    });
  });

  it('serialize participant', () => {
    const json = JSON.parse(JSON.stringify(testHelper.surveyTestData.participants[0]));
    const participant = new Participant(json);
    const participantJson = JSON.parse(JSON.stringify(participant));
    expect(participant.email).to.equal(participantJson.email);
    expect(participant.id).to.equal(participantJson.id);
    expect(participant.name).to.equal(participantJson.name);
    expect(Object.entries(participant).length).to.equal(Object.entries(participantJson).length);
  });
});
