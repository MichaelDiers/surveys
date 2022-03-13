const { Router } = require('express');
const data = require('../data/pug-view');

/**
 * Initializes the middleware for setting pug values.
 * @param {object} config A configuration object.
 * @param {Router} config.router An express router.
 * @returns The given router if set in config, a new express router otherwise.
 */
const initialize = (config = {}) => {
  const {
    router = Router(),
    gatewayAddress,
  } = config;

  router.use((req, res, next) => {
    res.locals.pubLocals = data;
    res.locals.pubLocals.gatewayAddress = gatewayAddress;
    next();
  });

  return router;
};

module.exports = initialize;
