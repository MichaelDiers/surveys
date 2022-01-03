const express = require('express');

/**
 * Initialiaze a new express app.
 */
const init = () => {
  const app = express();
  app.get('/', (req, res) => {
    res.send('Hello World').end();
  });

  return app;
};

module.exports = init;
