const { expect } = require('chai');
const uuid = require('uuid');

const survey = require('../schema/example.json');

const choice = {
  answer: 'text',
  id: uuid.v4(),
  selectable: true,
};

const person = {
  email: 'example@domain.example',
  id: uuid.v4(),
  name: 'name of the game',
};

const participant = JSON.parse(JSON.stringify(person));
participant.questions = [
  {
    choiceId: uuid.v4(),
    questionId: uuid.v4(),
  },
  {
    choiceId: uuid.v4(),
    questionId: uuid.v4(),
  },
];

const question = {
  question: 'question text',
  id: uuid.v4(),
  order: 1,
  choices: [1, 2, 3].map((i) => {
    const created = {
      answer: `text-${i}`,
      id: uuid.v4(),
      selectable: true,
    };

    return created;
  }),
};

const questionReference = {
  questionId: uuid.v4(),
  choiceId: uuid.v4(),
};

const empty = [undefined, null, ''];

const invalidGuid = [
  'abc',
  uuid.v4().substring(0, uuid.v4().length - 2),
  '5678',
  56789,
];

const createFromNonObjectThrowsError = (create) => {
  describe('create throws an Error if input is not an object', () => {
    it('json=true', () => {
      expect(() => create(true)).to.throw(Error, 'Invalid value for json = true');
    });
  });
};

const createFromEmptyThrowsError = (create) => {
  describe('create throws an Error if input is empty', () => {
    empty.forEach((json) => {
      it(`json=${json}`, () => {
        expect(() => create(json)).to.throw(Error, 'Invalid value for json =');
      });
    });
  });
};

const createFromEmptyParameterThrowsError = (create, data, ...parameters) => {
  describe('throw error if parameter is not set', () => {
    parameters.forEach((parameter) => {
      describe(`parameter: ${parameter}`, () => {
        it(`delete ${parameter}`, () => {
          const testData = JSON.parse(JSON.stringify(data));
          delete testData[parameter];
          expect(() => create(testData)).to.throw(Error, `Invalid value for ${parameter} =`);
        });

        empty.forEach((parameterValue) => {
          it(`${parameter}=${parameterValue}`, () => {
            const testData = JSON.parse(JSON.stringify(data));
            testData[parameter] = parameterValue;
            expect(() => create(testData)).to.throw(Error, `Invalid value for ${parameter} =`);
          });
        });
      });
    });
  });
};

const emailCheck = (create, data, parameter) => {
  describe('using invalid emails throws an error', () => {
    ['foobar', '@bar.de', 'foo@'].forEach((email) => {
      it(`${parameter} = ${email}`, () => {
        const testData = JSON.parse(JSON.stringify(data));
        testData[parameter] = email;
        expect(() => create(testData)).to.throw(`Invalid value for ${parameter} = ${email}`);
      });
    });
  });
};

const serializeTest = (create, data) => {
  describe('serialize check', () => {
    it('original and created object should be equal', () => {
      const testData = JSON.parse(JSON.stringify(data));
      const created = create(testData);
      expect(Object.keys(data).length).to.equal(Object.keys(created).length);
      Object.keys(data).forEach((key) => {
        expect(data[key]).to.equal(created[key]);
      });
    });
  });
};

const usingInvalidUuidsThrowsAnError = (create, data, parameter) => {
  describe('using a non uuid v4 throws an error', () => {
    invalidGuid.forEach((guid) => {
      it(`${parameter} = ${guid}`, () => {
        const testData = JSON.parse(JSON.stringify(data));
        testData[parameter] = guid;
        expect(() => create(testData)).to.throw(Error, `Invalid value for ${parameter} = ${guid}`);
      });
    });
  });
};

module.exports = {
  data: {
    choice,
    empty,
    invalidGuid,
    participant,
    person,
    question,
    questionReference,
    survey,
  },
  createFromEmptyParameterThrowsError,
  createFromEmptyThrowsError,
  createFromNonObjectThrowsError,
  emailCheck,
  serializeTest,
  usingInvalidUuidsThrowsAnError,
};
