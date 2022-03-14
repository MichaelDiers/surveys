const app = require('./application/app');
const config = require('./config');

exports.vote = app(
  config({
    baseName: 'vote',
  }),
);
