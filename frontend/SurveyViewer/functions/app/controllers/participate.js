const http = require('http');

const initialize = (config = {}) => {
  const {
    surveyViewerServiceUrl = 'http://127.0.0.1:8080/',
  } = config;

  const controller = {
    submit: async function submit(vote) {
      return new Promise((resolve, reject) => {
        const postData = JSON.stringify(vote);
        const url = `${surveyViewerServiceUrl}submit`;
        const req = http.request(
          url,
          {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
              'Content-Length': Buffer.byteLength(postData),
            },
          },
          (res) => {
            if (res.statusCode >= 400) {
              reject(new Error(`Error: url: ${url}, status: ${res.statusCode}`));
            } else if (res.statusCode === 200) {
              resolve();
            } else {
              res.on('end', () => {
                if (res.statusCode === 200) {
                  resolve();
                } else {
                  reject(new Error(`Error: url: ${url}, status: ${res.statusCode}`));
                }
              });
            }
          },
        );

        req.on('error', (e) => {
          reject(new Error(e.message));
        });

        req.write(postData);
        req.end();
      });
    },
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
