const uuid = require('uuid');
const http = require('https');

const initialize = (config = {}) => {
  const {
    surveyViewerServiceUrl,
  } = config;

  const controller = {
    submit: async function submit(vote) {
      return new Promise((resolve, reject) => {
        const data = { questions: [] };
        Object.entries(vote).forEach(([key, value]) => {
          if (key !== '_csrf') {
            if (key === 'participantId') {
              data[key] = value;
            } else if (uuid.validate(key)) {
              data.questions.push({ questionId: key, value });
            }
          }
        });

        const postData = JSON.stringify(data);
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
