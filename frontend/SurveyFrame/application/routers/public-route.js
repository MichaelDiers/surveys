const express = require('express');

/**
 * Initialize the public route for images, css and js files.
 * @param {object} config The configuration for initializing the route.
 * @param {string} config.publicLocalFolder The local folder for public files.
 * @param {string} config.publicRoute The url for public files.
 * @param {express.Router} config.router The router for handling the route.
 * @returns The given router if set in config, a new express router otherwise.
*/
const initialize = (config = {}) => {
  const {
    publicLocalFolder,
    publicRoute,
    router = express.Router(),
  } = config;

  const statics = express.static(publicLocalFolder, { index: false });
  router.use(publicRoute, statics);

  return router;
};

module.exports = initialize;
