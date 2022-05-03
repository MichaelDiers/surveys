const express = require('express');
const supertest = require('supertest');

const footerController = require('../../../application/controllers/footer-controller');

describe('footer-controller', () => {
  describe('index', () => {
    it('responds with html', async () => {
      const app = express();
      app.set('views', './application/views');
      app.set('view engine', 'pug');
      app.get('/', footerController().index);
      await supertest(app)
        .get('/')
        .expect(200)
        .expect('Content-Type', /text\/html/);
    });
  });
});
