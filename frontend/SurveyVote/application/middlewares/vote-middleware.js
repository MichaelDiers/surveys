const { Router } = require('express');
const uuid = require('uuid');

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

  router.use('/for/:surveyId/:participantId', (req, res, next) => {
    const { surveyId, participantId } = req.params;
    if (surveyId
      && uuid.validate(surveyId)
      && uuid.version(surveyId) === 4
      && participantId
      && uuid.validate(participantId)
      && uuid.version(participantId) === 4) {
      next();
    } else {
      res.render('vote/unknown');
      next('invalid request');
    }
  });

  return router;
};

module.exports = initialize;
