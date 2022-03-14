const app = require('./application/app');
const config = require('./config');

exports.frame = app(
  config({
    baseName: 'frame',
  }),
);
