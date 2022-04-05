const app = require('./application/app');
const config = require('./config');

const application = app(
  config({
    appRoute: '/vote',
    baseName: 'vote',
    gatewayAddress: '',
    requestLogging: true,
    surveysCollectionName: 'surveys-test',
    surveyStatusCollectionName: 'survey-status-test',
    surveyResultsCollectionName: 'survey-results-test',
    saveSurveyResultTopic: 'SAVE_SURVEY_RESULT_TEST',
  }),
);

const port = 3002;
application.listen(port, () => {
  // eslint-disable-next-line
  console.log(`Example app listening on port ${port}`);
});
