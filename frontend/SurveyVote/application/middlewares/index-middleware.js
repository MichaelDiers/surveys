const { Router } = require('express');

/**
 * Initializes the middleware for the index route.
 * @param {object} config A configuration object.
 * @param {Router} config.router An express router.
 * @returns The given router if set in config, a new express router otherwise.
 */
const initialize = (config = {}) => {
  const {
    router = Router(),
  } = config;

  router.use('/', (req, res, next) => {
    next();
  });

  return router;
};

module.exports = initialize;
