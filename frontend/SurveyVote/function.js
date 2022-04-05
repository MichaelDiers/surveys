const app = require('./application/app');
const config = require('./config');

const {
  ENV_SURVEYS_COLLECTION_NAME: surveysCollectionName,
  ENV_SURVEY_STATUS_COLLECTION_NAME: surveyStatusCollectionName,
  ENV_SURVEY_RESULTS_COLLECTION_NAME: surveyResultsCollectionName,
  ENV_SAVE_SURVEY_RESULT_TOPIC: saveSurveyResultTopic,
} = process.env;

exports.vote = app(
  config({
    baseName: 'vote',
    surveysCollectionName,
    surveyStatusCollectionName,
    surveyResultsCollectionName,
    saveSurveyResultTopic,
    requestLogging: false,
  }),
);
