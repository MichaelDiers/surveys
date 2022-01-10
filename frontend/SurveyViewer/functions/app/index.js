const express = require('express');

const initialize = () => {
  const router = express.Router();
  router.get('/', (req, res) => {
    res.send('hello world!').end();
  });

  const app = express();
  app.use('/', router);
  return router;
};

module.exports = initialize;
