/**
 * Tests for creating participants.
 */
const { expect } = require('chai');
const create = require('../../models/participant');
const {
  data: {
    participant,
  },
  createFromEmptyParameterThrowsError,
  createFromEmptyThrowsError,
  createFromNonObjectThrowsError,
  emailCheck,
  usingInvalidUuidsThrowsAnError,
} = require('../test-helper');

describe('participant.js', () => {
  createFromEmptyThrowsError(create);
  createFromEmptyParameterThrowsError(create, participant, 'email', 'id', 'name');
  createFromNonObjectThrowsError(create);

  describe('using invalid types for paramters throws an error', () => {
    it('using boolean for email throws an error', () => {
      const testData = JSON.parse(JSON.stringify(participant));
      testData.email = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for email = ');
    });

    it('using boolean for id throws an error', () => {
      const testData = JSON.parse(JSON.stringify(participant));
      testData.id = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for id = ');
    });

    it('using boolean for name throws an error', () => {
      const testData = JSON.parse(JSON.stringify(participant));
      testData.name = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for name = ');
    });
  });

  usingInvalidUuidsThrowsAnError(create, participant, 'id');
  emailCheck(create, participant, 'email');

  describe('using invalid questions throws an error', () => {
    it('choiceId is invalid', () => {
      const testData = JSON.parse(JSON.stringify(participant));
      testData.questions[1].choiceId = 'asdf';
      expect(() => create(testData)).to.throw(Error, 'Invalid value for choiceId');
    });

    it('questionId is invalid', () => {
      const testData = JSON.parse(JSON.stringify(participant));
      testData.questions[1].questionId = 'asdf';
      expect(() => create(testData)).to.throw(Error, 'Invalid value for questionId');
    });
  });

  describe('serialize check', () => {
    it('original and created object should be equal', () => {
      const testData = JSON.parse(JSON.stringify(participant));
      const created = create(testData);
      expect(Object.keys(participant).length).to.equal(Object.keys(created).length);
      ['email', 'id', 'name'].forEach((parameter) => {
        expect(participant[parameter]).to.equal(created[parameter]);
      });

      expect(participant.questions.length).to.equal(created.questions.length);
      participant.questions.forEach(({ choiceId, questionId }) => {
        expect(created.questions.some(
          (qr) => qr.choiceId === choiceId && qr.questionId === questionId,
        ))
          .to.equal(true);
      });
    });
  });

  describe('cross check data', () => {
    it('using duplication questionIds in refrences throws an error', () => {
      const testData = JSON.parse(JSON.stringify(participant));
      testData.questions.push(testData.questions[0]);
      expect(() => create(testData)).to.throw(Error, 'Invalid value for questions');
    });
  });
});
