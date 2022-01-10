const express = require('express');

const controllers = require('./controllers/index');
const middleware = require('./middleware/index');
const routers = require('./routers/index');

const initialize = (config = {}) => {
  const {
    database,
  } = config;

  const router = express.Router();
  router.use(middleware.base());

  router.use('/participate', routers.participateRouter({ controller: controllers.participateController({ database }) }));
  router.use('/thankyou', routers.thankyou());

  const app = express();
  app.set('views', './app/views');
  app.set('view engine', 'pug');
  app.use('/', router);
  return app;
};

module.exports = initialize;
