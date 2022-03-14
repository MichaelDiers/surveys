const { Router } = require('express');

/**
 * Initialize the index route.
 * @param {object} config A configuration object.
 * @param {object} config.controller The index controller.
 * @param {Router} config.router An express router.
 * @returns The given router if set in config, a new express router otherwise.
 */
const initialize = (config = {}) => {
  const {
    controller,
    router = Router(),
  } = config;

  router.get('/*', controller.index);

  return router;
};

module.exports = initialize;
