const baseMiddleware = require('./base-middleware');
const csurfMiddleware = require('./csurf-middleware');
const indexMiddleware = require('./index-middleware');
const pugMiddleware = require('./pug-middleware');

module.exports = {
  baseMiddleware,
  csurfMiddleware,
  indexMiddleware,
  pugMiddleware,
};
