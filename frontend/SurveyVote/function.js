const app = require('./application/app');
const config = require('./config');

const {
  ENV_SURVEYS_COLLECTION_NAME: surveysCollectionName,
} = process.env;

exports.vote = app(
  config({
    baseName: 'vote',
    surveysCollectionName,
  }),
);
