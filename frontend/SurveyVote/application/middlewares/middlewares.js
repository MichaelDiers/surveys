const baseMiddleware = require('./base-middleware');
const csurfMiddleware = require('./csurf-middleware');
const pugMiddleware = require('./pug-middleware');
const voteMiddleware = require('./vote-middleware');

module.exports = {
  baseMiddleware,
  csurfMiddleware,
  pugMiddleware,
  voteMiddleware,
};
