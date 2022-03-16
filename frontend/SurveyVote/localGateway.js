const app = require('./application/app');
const config = require('./config');

const application = app(
  config({
    appRoute: '/vote',
    baseName: 'vote',
    gatewayAddress: 'http://localhost:3000/gateway',
    requestLogging: true,
    surveysCollectionName: 'surveys',
    surveyStatusCollectionName: 'surveys-status',
    surveyResultsCollectionName: 'survey-results',
    saveSurveyResultTopic: 'SAVE_SURVEY_RESULT',
  }),
);

const port = 3002;
application.listen(port, () => {
  // eslint-disable-next-line
  console.log(`Example app listening on port ${port}`);
});
