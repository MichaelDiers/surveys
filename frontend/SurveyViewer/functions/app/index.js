const express = require('express');

const controllers = require('./controllers/index');
const middleware = require('./middleware/index');
const routers = require('./routers/index');

const initialize = (config = {}) => {
  const router = express.Router();

  const statics = express.static('./app/static', { index: false });
  router.use('/static', statics);

  router.use(middleware.base());

  router.use('/participate', routers.participateRouter({
    controller: controllers.participate(),
  }));

  router.use('/thankyou', routers.thankyou({
    controller: controllers.thankyou(),
  }));

  const app = express();
  app.set('views', './app/views');
  app.set('view engine', 'pug');
  app.use('/', router);
  return app;
};

module.exports = initialize;
