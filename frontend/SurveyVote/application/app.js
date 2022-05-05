const express = require('express');

const controllers = require('./controllers/controllers');
const middlewares = require('./middlewares/middlewares');
const routers = require('./routers/routers');
const databaseInit = require('./services/database');
const pubSubInit = require('./services/pub-sub');

/**
 * Initializes the app.
 * @param {object} config A configuration object.
 * @param {express.Router} config.router An express router.
 * @param {Express} An express application.
 * @returns The given app if set in config, a new express app otherwise.
 */
const initialize = (config = {}) => {
  const {
    app = express(),
    router = express.Router(),
    viewEngine,
    viewLocalFolder,
    appRoute,
    requestLogging,
    // pug
    lang,
    files,
    surveysCollectionName,
    surveyStatusCollectionName,
    surveyResultsCollectionName,
    database = databaseInit({
      surveyStatusCollectionName,
      surveysCollectionName,
      surveyResultsCollectionName,
    }),
    // csurfCookieName,
    saveSurveyResultTopic,
    pubSubClient = pubSubInit({ topicName: saveSurveyResultTopic }),
  } = config;

  middlewares.baseMiddleware({ router, requestLogging });
  routers.publicRoute({ router });
  middlewares.pugMiddleware({ router, lang, files });
  // middlewares.csurfMiddleware({ router, csurfCookieName });

  routers.footerRoute({
    router,
    controller: controllers.footerController(),
  });

  routers.headerRoute({
    router,
    controller: controllers.headerController(),
  });

  routers.voteRoute({
    router: middlewares.voteMiddleware({ router }),
    controller: controllers.voteController({ database, pubSubClient }),
  });

  app.set('views', viewLocalFolder);
  app.set('view engine', viewEngine);

  app.use(appRoute, router);

  app.use((err, req, res, next) => {
    if (res.headersSent) {
      next(err);
    } else {
      if (err) {
        console.error(err.stack);
        console.error(err.message);
      }

      res.status(500).send('error');
    }
  });

  return app;
};

module.exports = initialize;
