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
    viewEngine = 'pug',
    viewLocalFolder = './application/views',
    baseAddress = '/',
    gatewayAddress = '/gateway',
  } = config;

  middlewares.base({ router });
  routers.public({ router });
  middlewares.pug({ router, gatewayAddress });
  middlewares.csurf({ router });
  routers.index({
    router: middlewares.index({ router }),
    controller: controllers.index(),
  });

  app.set('views', viewLocalFolder);
  app.set('view engine', viewEngine);

  app.use(baseAddress, router);

  return app;
};

module.exports = initialize;
