const express = require('express');
const supertest = require('supertest');

const headerController = require('../../../application/controllers/header-controller');

describe('header-controller', () => {
  describe('index', () => {
    it('responds with html', async () => {
      const app = express();
      app.set('views', './application/views');
      app.set('view engine', 'pug');
      app.get('/', headerController().index);
      await supertest(app)
        .get('/')
        .expect(200)
        .expect('Content-Type', /text\/html/);
    });
  });
});
