const app = require('./application/app');
const config = require('./config');

const {
  ENV_SURVEYS_COLLECTION_NAME: surveysCollectionName,
  ENV_SURVEY_STATUS_COLLECTION_NAME: surveyStatusCollectionName,
} = process.env;

exports.vote = app(
  config({
    baseName: 'vote',
    surveysCollectionName,
    surveyStatusCollectionName,
  }),
);
