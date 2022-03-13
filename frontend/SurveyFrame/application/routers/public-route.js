const express = require('express');

/**
 * Initialize the public route for images, css and js files.
 * @param {object} config The configuration for initializing the route.
 * @param {string} config.publicLocalFolder The local folder for public files.
 * @param {string} config.publicRoute The url for public files.
 * @param {express.Router} config.router The router for handling the route.
 */
const initialize = (config = {}) => {
  const {
    publicLocalFolder = './application/public',
    publicRoute = '/public',
    router = express.Router(),
  } = config;

  const statics = express.static(publicLocalFolder, { index: false });
  router.use(publicRoute, statics);

  return router;
};

module.exports = initialize;
