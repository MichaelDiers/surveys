const csurf = require('csurf');
const express = require('express');

/**
 * Initialize the CSRF middleware.
 * @param {object} config A configuration object.
 * @param {express.Router} config.router An express router.
 * @param {string} config.csurfCookieName The name of the csrf cookie..
 * @returns {express.Router} The express router that is initialized.
 */
const initialize = (options = {}) => {
  const {
    router = express.Router(),
    csurfCookieName,
  } = options;

  const csurfProtection = csurf({
    cookie: {
      key: csurfCookieName,
      httpOnly: true,
      sameSite: true,
    },
  });
  router.use(csurfProtection);
  router.use((req, res, next) => {
    res.locals.csurfToken = req.csrfToken();
    next();
  });

  return router;
};

module.exports = initialize;
