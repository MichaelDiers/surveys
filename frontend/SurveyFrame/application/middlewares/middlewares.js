const base = require('./base-middleware');
const csurf = require('./csurf-middleware');
const index = require('./index-middleware');
const pug = require('./pug-middleware');

module.exports = {
  base,
  csurf,
  index,
  pug,
};
