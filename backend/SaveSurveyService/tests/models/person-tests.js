/**
 * Tests for creating persons.
 */
const { expect } = require('chai');
const create = require('../../models/person');
const {
  data: {
    person,
  },
  createFromEmptyParameterThrowsError,
  createFromEmptyThrowsError,
  createFromNonObjectThrowsError,
  emailCheck,
  serializeTest,
  usingInvalidUuidsThrowsAnError,
} = require('../test-helper');

describe('person.js', () => {
  createFromEmptyThrowsError(create);
  createFromEmptyParameterThrowsError(create, person, 'email', 'id', 'name');
  createFromNonObjectThrowsError(create);

  describe('using invalid types for paramters throws an error', () => {
    it('using boolean for email throws an error', () => {
      const testData = JSON.parse(JSON.stringify(person));
      testData.email = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for email = ');
    });

    it('using boolean for id throws an error', () => {
      const testData = JSON.parse(JSON.stringify(person));
      testData.id = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for id = ');
    });

    it('using boolean for name throws an error', () => {
      const testData = JSON.parse(JSON.stringify(person));
      testData.name = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for name = ');
    });
  });

  usingInvalidUuidsThrowsAnError(create, person, 'id');
  emailCheck(create, person, 'email');
  serializeTest(create, person);
});
