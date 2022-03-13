const compression = require('compression');
const cookieParser = require('cookie-parser');
const express = require('express');
const helmet = require('helmet');

/**
 * Initialize the basic middleware.
 * @param {object} config A configuration object.
 * @param {express.Router} config.router An express router.
 * @returns {express.Router} The given router if set in config, a new express router otherwise.
 */
const initialize = (config = {}) => {
  const {
    router = express.Router(),
  } = config;

  router.use(helmet());
  router.use(compression());
  router.use(express.urlencoded({ extended: false }));
  router.use(express.json());
  router.use(cookieParser());

  router.use((req, res, next) => {
    console.log(`${req.method} ${req.baseUrl}`);
    next();
  });

  return router;
};

module.exports = initialize;
