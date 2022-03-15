const csurf = require('csurf');
const express = require('express');
const cookieParser = require('cookie-parser');
/**
 * Initialize the CSRF middleware.
 * @param {object} config A configuration object.
 * @param {express.Router} config.router An express router.
 * @returns {express.Router} The express router that is initialized.
 */
const initialize = (options = {}) => {
  const {
    router = express.Router(),
  } = options;

  router.use(cookieParser());
  const csurfProtection = csurf({ cookie: true });
  router.use(csurfProtection);
  router.use((req, res, next) => {
    res.locals.csurfToken = req.csrfToken();
    next();
  });

  return router;
};

module.exports = initialize;
