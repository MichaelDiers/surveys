const { expect } = require('chai');

const testHelper = require('../test-helper');
const Person = require('../../models/person');

describe('person.js', () => {
  describe('empty json', () => {
    [undefined, null, ''].forEach((json) => {
      it(`empty json: ${json}`, () => {
        expect(() => new Person(json)).to.throw(Error, 'Person: Empty json.');
      });
    });
  });

  describe('email', () => {
    testHelper.raiseErrorIfNotExists(Person, testHelper.surveyTestData.organizer, 'email');

    it('raise error for invalid email', () => {
      const organizer = JSON.parse(JSON.stringify(testHelper.surveyTestData.organizer));
      organizer.email = 'email@email';
      expect(() => new Person(organizer)).throws(Error, 'Invalid email: \'email@email\'');
    });

    it('create person with valid email', () => {
      const organizer = JSON.parse(JSON.stringify(testHelper.surveyTestData.organizer));
      organizer.email = 'email@email.de';
      expect(new Person(organizer).email).to.equal('email@email.de');
    });
  });

  describe('id', () => {
    testHelper.raiseErrorIfNotExists(Person, testHelper.surveyTestData.organizer, 'id');
    testHelper.testIds(Person, testHelper.surveyTestData.organizer);
  });

  describe('name', () => {
    testHelper.raiseErrorIfNotExists(Person, testHelper.surveyTestData.organizer, 'name');

    it('set valid name', () => {
      const organizer = JSON.parse(JSON.stringify(testHelper.surveyTestData.organizer));
      const name = 'new name';
      organizer.name = name;
      expect(new Person(organizer).name).to.equal(name);
    });
  });

  it('serialize person', () => {
    const json = JSON.parse(JSON.stringify(testHelper.surveyTestData.organizer));
    const person = new Person(json);
    const personJson = JSON.parse(JSON.stringify(person));
    expect(personJson.email).to.equal(personJson.email);
    expect(personJson.id).to.equal(personJson.id);
    expect(personJson.name).to.equal(personJson.name);
    expect(Object.entries(personJson).length).to.equal(Object.entries(json).length);
  });
});
