const http = require('http');

const initialize = (config = {}) => {
  const {
    surveyViewerServiceUrl = 'http://127.0.0.1:8080/',
  } = config;

  const controller = {
    viewSurveyAjax: async function viewSurveyAjax(participantId) {
      return new Promise((resolve, reject) => {
        const url = `${surveyViewerServiceUrl}${participantId}`;
        const req = http.request(url, {}, (response) => {
          let result = '';
          response.on('data', (chunk) => { result += chunk; });
          response.on('end', () => {
            if (response.statusCode === 200) {
              resolve(JSON.parse(result));
            } else {
              reject(new Error(`Error: url: ${url}, status: ${response.statusCode}`));
            }
          });
        });

        req.on('error', (e) => {
          console.error(e);
          reject(new Error(e.message));
        });

        req.end();
      });
    },
    viewSurvey: async function viewSurvey() {
      return {
        view: 'participate/index',
      };
    },
  };

  return controller;
};

module.exports = initialize;
