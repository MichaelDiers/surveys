const express = require('express');

const controllers = require('./controllers/controllers');
const middlewares = require('./middlewares/middlewares');
const routers = require('./routers/routers');

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
    gatewayAddress,
    viewEngine,
    viewLocalFolder,
    appRoute,
    requestLogging,
    publicLocalFolder,
    publicRoute,
    // pug
    lang,
    files,
  } = config;

  middlewares.baseMiddleware({ router, requestLogging });
  routers.publicRoute({
    router,
    publicLocalFolder,
    publicRoute,
  });
  middlewares.pugMiddleware({ router, lang, files });
  // middlewares.csurfMiddleware({ router });
  routers.indexRoute({
    router: middlewares.indexMiddleware({ router }),
    controller: controllers.indexController({ gatewayAddress }),
  });

  app.set('views', viewLocalFolder);
  app.set('view engine', viewEngine);

  app.use(appRoute, router);

  app.use((err, req, res, next) => {
    if (res.headersSent) {
      next(err);
    } else {
      if (err) {
        // eslint-disable-next-line no-console
        console.error(err.stack);
        // eslint-disable-next-line no-console
        console.error(err.message);
      }

      res.status(500).send('error');
    }
  });

  return app;
};

module.exports = initialize;
