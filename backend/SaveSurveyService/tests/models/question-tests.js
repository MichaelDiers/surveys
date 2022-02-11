/**
 * Tests for creating questions.
 */
const { expect } = require('chai');
const create = require('../../models/question');
const {
  data: {
    question,
  },
  createFromEmptyParameterThrowsError,
  createFromEmptyThrowsError,
  createFromNonObjectThrowsError,
  usingInvalidUuidsThrowsAnError,
} = require('../test-helper');

describe('question.js', () => {
  createFromEmptyThrowsError(create);
  createFromEmptyParameterThrowsError(create, question, 'question', 'id', 'order', 'choices');
  createFromNonObjectThrowsError(create);

  describe('using invalid types for paramters throws an error', () => {
    ['question', 'id', 'order', 'choices'].forEach((parameter) => {
      it(`using boolean for ${parameter} throws an error`, () => {
        const testData = JSON.parse(JSON.stringify(question));
        testData[parameter] = true;
        expect(() => create(testData)).to.throw(Error, `Invalid value for ${parameter} = true`);
      });
    });
  });

  usingInvalidUuidsThrowsAnError(create, question, 'id');

  describe('using invalid data throws an error', () => {
    it('choice.id is invalid', () => {
      const testData = JSON.parse(JSON.stringify(question));
      testData.choices[1].id = 'asdf';
      expect(() => create(testData)).to.throw(Error, 'Invalid value for id = asdf');
    });

    it('choice.answer is invalid', () => {
      const testData = JSON.parse(JSON.stringify(question));
      testData.choices[1].answer = true;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for answer = true');
    });

    it('choice.selectable is invalid', () => {
      const testData = JSON.parse(JSON.stringify(question));
      testData.choices[1].selectable = 'foobar';
      expect(() => create(testData)).to.throw(Error, 'Invalid value for selectable = foobar');
    });

    it('order is invalid', () => {
      const testData = JSON.parse(JSON.stringify(question));
      testData.order = -1;
      expect(() => create(testData)).to.throw(Error, 'Invalid value for order = -1');
    });
  });

  describe('serialize check', () => {
    it('original and created object should be equal', () => {
      const testData = JSON.parse(JSON.stringify(question));
      const created = create(testData);
      expect(Object.keys(question).length).to.equal(Object.keys(question).length);
      ['question', 'id', 'order'].forEach((parameter) => {
        expect(question[parameter]).to.equal(created[parameter]);
      });

      expect(question.choices.length).to.equal(created.choices.length);
      expect(created.choices.every(
        (choice) => Object.keys(choice).length === Object.keys(question.choices[0]).length,
      ))
        .to.equal(true);
      question.choices.forEach((choice) => {
        expect(created.choices.some(
          (cc) => cc.answer === choice.answer
            && cc.id === choice.id
            && cc.selectable === choice.selectable,
        ))
          .to.equal(true);
      });
    });
  });

  describe('cross check data', () => {
    it('using duplication ids for choices throws an error', () => {
      const testData = JSON.parse(JSON.stringify(question));
      testData.choices.push(testData.choices[0]);
      expect(() => create(testData)).to.throw(Error, 'Invalid value for choices');
    });
  });
});
