const { expect } = require('chai');
const uuid = require('uuid');

const surveyTestData = require('../schema/example.json');

const guids = {
  invalid: [
    undefined,
    null,
    '',
    uuid.v4().substring(0, uuid.v4().length - 2),
    'dfghjklö',
    '456789',
  ],
  valid: [
    uuid.v4(),
  ],
};

const passTest = (CtorFunc, data, propertyName) => {
  describe(`initialize ${CtorFunc.name} with valid value`, () => {
    it(`check ${propertyName}`, () => {
      const json = JSON.parse(JSON.stringify(data));
      expect(new CtorFunc(json)[propertyName]).to.equal(data[propertyName]);
    });
  });
};

const raiseErrorEmptyJson = (CtorFunc) => {
  describe('raise error if input is invalid', () => {
    [undefined, null, ''].forEach((json) => {
      it(`new ${CtorFunc.name}(${json})`, () => {
        expect(() => new CtorFunc(json)).to.throw(Error, `${CtorFunc.name}: Empty json`);
      });
    });
  });
};

const raiseErrorIfNotExists = (CtorFunc, data, propertyName, error) => {
  describe(`raise error if ${propertyName} does not exists`, () => {
    it(`delete ${propertyName}`, () => {
      const json = JSON.parse(JSON.stringify(data));
      delete json[propertyName];
      expect(() => new CtorFunc(json)).to.throw(Error, error || `Invalid ${propertyName}: 'undefined'`);
    });
  });

  describe(`raise error if ${propertyName} is invalid`, () => {
    [undefined, null, ''].forEach((value) => {
      it(`${propertyName} = ${value}`, () => {
        const json = JSON.parse(JSON.stringify(data));
        json[propertyName] = value;
        expect(() => new CtorFunc(json)).to.throw(Error, error || `Invalid ${propertyName}: '${value}'`);
      });
    });
  });
};

const testIds = (CtorFunc, data) => {
  describe('expect initialization to throw error for invalid id', () => {
    guids.invalid.forEach((id) => {
      it(`id = ${id}`, () => {
        const json = JSON.parse(JSON.stringify(data));
        json.id = id;
        expect(() => new CtorFunc(json)).to.throw(`Invalid id: '${id}'`);
      });
    });
  });

  describe('expect initialization to pass for valid id', () => {
    guids.valid.forEach((id) => {
      it(`id = ${id}`, () => {
        const json = JSON.parse(JSON.stringify(data));
        json.id = id;
        expect(new CtorFunc(json).id).to.equal(id);
      });
    });
  });
};

module.exports = {
  surveyTestData,
  raiseErrorIfNotExists,
  raiseErrorEmptyJson,
  passTest,
  testIds,
  guids,
};
