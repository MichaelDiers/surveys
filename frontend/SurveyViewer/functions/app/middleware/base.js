const compression = require('compression');
const cookieParser = require('cookie-parser');
const csurf = require('csurf');
const express = require('express');
const helmet = require('helmet');

const initialize = (config = {}) => {
  const {
    router = express.Router(),
  } = config;

  router.use(helmet());
  router.use(compression());
  router.use(express.urlencoded({ extended: false }));
  router.use(express.json());
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
