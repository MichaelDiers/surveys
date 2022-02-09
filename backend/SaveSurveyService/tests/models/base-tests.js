/**
 * Tests for class Base.
 */

const { expect } = require('chai');

const Base = require('../../models/base');

describe('base.js', () => {
  describe('constructor', () => {
    describe('constructor throws error if json is invalid', () => {
      [undefined, null, ''].forEach((json) => {
        it(`new Base(${json})`, () => {
          expect(() => new Base(json)).to.throw(Error, 'Base: Empty json.');
        });
      });
    });
  });

  describe('validate', () => {
    describe('validate throws error if value is invalid', () => {
      [undefined, null, ''].forEach((value) => {
        it(`Base.validate('foo', ${value}))`, () => {
          expect(() => Base.validate('foo', value))
            .to.throw(Error, 'Invalid foo:');
        });
      });
    });

    describe('validate throws error if validateFunc does not succeed.', () => {
      it('Base.validate(\'foo\', 1, (value) => value > 1))', () => {
        expect(() => Base.validate('foo', 1, (value) => value > 1))
          .to.throw(Error, 'Invalid foo:');
      });
    });

    describe('create valid instance', () => {
      it('new Base({ foo: 1 })', () => {
        expect(new Base({ foo: 1 })).to.an.instanceOf(Base);
      });
    });

    describe('validateFunc succeeds', () => {
      it('new Base({ foo: 1 }) validateFunc succeeds', () => {
        expect(Base.validate('foo', 1)).to.equal(1);
      });

      it('new Base({ foo: 1 }) validateFunc succeeds', () => {
        expect(Base.validate('foo', 1, (value) => value === 1)).to.equal(1);
      });
    });
  });
});
