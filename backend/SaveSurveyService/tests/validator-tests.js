const { expect } = require('chai');
const uuid = require('uuid');

const Validator = require('../validator');

describe('validator.js', () => {
  describe('getKeyValue', () => {
    it('getKeyValue return value and valueName', () => {
      const validator = new Validator();
      const { value, valueName } = validator.getKeyValue({ key: 'value' });
      expect(value).to.equal('value');
      expect(valueName).to.equal('key');
    });
  });

  describe('throwError', () => {
    it('throwError without message and without object', () => {
      const validator = new Validator();
      expect(() => validator.throwError(true, 'foo')).to.throw(Error, 'Invalid value for foo = true -  (undefined)');
    });

    it('throwError without message', () => {
      const validator = new Validator({ bar: 'foo' });
      expect(() => validator.throwError(true, 'foo')).to.throw(Error, 'Invalid value for foo = true -  ({"bar":"foo"})');
    });

    it('throwError', () => {
      const validator = new Validator({ bar: 'foo' });
      expect(() => validator.throwError(true, 'foo', 'my message')).to.throw(Error, 'Invalid value for foo = true - my message ({"bar":"foo"})');
    });
  });

  describe('validate', () => {
    [undefined, null, 0, ''].forEach((value) => {
      it('throw an Error for falsy values', () => {
        expect(() => new Validator().validate({ value })).to.throw(Error);
      });
    });

    it('expect to return { valueName, value }', () => {
      const { value, valueName } = new Validator().validate({ key: 'value' });
      expect(value).to.equal('value');
      expect(valueName).to.equal('key');
    });
  });

  describe('validateArray', () => {
    [undefined, null, 0, '', true, 'foobar', []].forEach((value) => {
      it(`throw an Error if value is falsy or not an array: ${JSON.stringify(value)}`, () => {
        expect(() => new Validator().validateArray({ key: value })).to.throw(Error);
      });
    });

    it('expect to return { valueName, value }', () => {
      const { value, valueName } = new Validator().validateArray({ key: ['value', 'foo'] });
      expect(value[0]).to.equal('value');
      expect(value[1]).to.equal('foo');
      expect(valueName).to.equal('key');
    });
  });

  describe('validateBoolean', () => {
    [undefined, null, 0, '', 1234, 'foobar', []].forEach((value) => {
      it(`throw an Error if value is falsy or not true or false: ${value}`, () => {
        expect(() => new Validator().validateBoolean({ value })).to.throw(Error);
      });
    });

    [true, false].forEach((trueFalse) => {
      it(`expect to return { valueName, value } for ${trueFalse}`, () => {
        const { value, valueName } = new Validator().validateBoolean({ value: trueFalse });
        expect(value).to.equal(trueFalse);
        expect(valueName).to.equal('value');
      });
    });
  });

  describe('validateEmail', () => {
    [
      null,
      undefined,
      '',
      '.@bar.de',
      '.foo@bar.de',
      'foo..@bar.de',
      'foo.@bar.de',
      'foo@bar.',
      'foo@bar.d',
      'foo@.',
      'foo.bar',
      'foo@.bar.de',
      'foo(@bar.de',
      'foo)@bar.de',
    ].forEach((email) => {
      it(`throw an error for email: ${email}`, () => {
        expect(() => new Validator().validateEmail({ email })).to.throw(Error);
      });
    });

    [
      'foo@bar.de',
      'foo.bar@bar.de',
      'foo.bar.foo@bar.de',
      'foo123@bar.de',
      '123@bar.de',
      '123-321@bar.de',
      '123-321-123@bar.de',
      '-@bar.de',
      '--@bar.de',
      'foo!@bar.de',
      'foo#@bar.de',
      'foo$@bar.de',
      'foo%@bar.de',
      'foo&@bar.de',
      'foo\'@bar.de',
      'foo*@bar.de',
      'foo+@bar.de',
      'foo-@bar.de',
      'foo/@bar.de',
      'foo=@bar.de',
      'foo?@bar.de',
      'foo^@bar.de',
      'foo_@bar.de',
      'foo`@bar.de',
      'foo{@bar.de',
      'foo|@bar.de',
      'foo}@bar.de',
      'foo~@bar.de',
      'foo@bar-foo.de',
      'foo@bar_77foo.de',
    ].forEach((email) => {
      it('expect email to be valid', () => {
        const { value, valueName } = new Validator().validateEmail({ email });
        expect(value).to.equal(email);
        expect(valueName).to.equal('email');
      });
    });
  });

  describe('validateInt', () => {
    [undefined, null, 1.1, '', { foo: 'bar' }, 'foobar', []].forEach((value) => {
      it(`throw an Error if value is not an integer: ${value}`, () => {
        expect(() => new Validator().validateInt({ value })).to.throw(Error);
      });
    });

    [-1, 0, 1].forEach((value) => {
      it(`expect to return { valueName, value } for ${value}`, () => {
        const { value: result, valueName } = new Validator().validateInt({ value });
        expect(result).to.equal(value);
        expect(valueName).to.equal('value');
      });
    });
  });

  describe('validateIntGreaterThanZero', () => {
    [undefined, null, 1.1, '', { foo: 'bar' }, 'foobar', [], -1, 0].forEach((value) => {
      it(`throw an Error if value is not an integer greater than zero: ${value}`, () => {
        expect(() => new Validator().validateIntGreaterThanZero({ value })).to.throw(Error);
      });
    });

    [1, 12].forEach((value) => {
      it(`expect to return { valueName, value } for ${value}`, () => {
        const { value: result, valueName } = new Validator().validateIntGreaterThanZero({ value });
        expect(result).to.equal(value);
        expect(valueName).to.equal('value');
      });
    });
  });

  describe('validateIsObject', () => {
    [undefined, null, 1.1, '', 'foobar', [], -1, 0, {}].forEach((value) => {
      it(`throw an Error if value is not an object: ${JSON.stringify(value)}`, () => {
        expect(() => new Validator().validateIsObject({ value })).to.throw(Error);
      });
    });

    [{ foo: 1 }].forEach((value) => {
      it(`expect to return { valueName, value } for ${value}`, () => {
        const { value: result, valueName } = new Validator().validateIsObject({ value });
        expect(result).to.equal(value);
        expect(valueName).to.equal('value');
      });
    });
  });

  describe('validateString', () => {
    [undefined, null, 1.1, '', [], -1, 0, {}].forEach((value) => {
      it(`throw an Error for value: ${JSON.stringify(value)}`, () => {
        expect(() => new Validator().validateString({ value })).to.throw(Error);
      });
    });

    ['foobar'].forEach((value) => {
      it(`expect to return { valueName, value } for ${value}`, () => {
        const { value: result, valueName } = new Validator().validateString({ value });
        expect(result).to.equal(value);
        expect(valueName).to.equal('value');
      });
    });
  });

  describe('validateUnique', () => {
    [undefined, null, 1.1, '', [], -1, 0, {}, [1, 1, 2, 3], ['1', '2', '1', '3']].forEach((value) => {
      it(`throw an Error for value: ${JSON.stringify(value)}`, () => {
        expect(() => new Validator().validateUnique({ value })).to.throw(Error);
      });
    });

    [[1, 2, 3], ['1', '2', '3']].forEach((value) => {
      it(`expect to return { valueName, value } for ${value}`, () => {
        const { value: result, valueName } = new Validator().validateUnique({ value });
        expect(result).to.equal(value);
        expect(valueName).to.equal('value');
      });
    });
  });

  describe('validateUuid', () => {
    [undefined, null, 1.1, '', { foo: 'bar' }, 'foobar', [], -1, 0].forEach((value) => {
      it(`throw an Error for value: ${value}`, () => {
        expect(() => new Validator().validateUuid({ value })).to.throw(Error);
      });
    });

    [uuid.v4()].forEach((value) => {
      it(`expect to return { valueName, value } for ${value}`, () => {
        const { value: result, valueName } = new Validator().validateUuid({ value });
        expect(result).to.equal(value);
        expect(valueName).to.equal('value');
      });
    });
  });
});
