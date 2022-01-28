const {GoogleAuth} = require('google-auth-library');
const uuid = require('uuid');
const http = require('https');

const auth = new GoogleAuth();

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
              const error = `Error: url: ${url}, status: ${res.statusCode}`;
              console.error(error);
              reject(new Error(error));
            } else if (res.statusCode === 200) {
              resolve();
            } else {
              res.on('end', () => {
                if (res.statusCode === 200) {
                  resolve();
                } else {
                  const error = `Error: url: ${url}, status: ${res.statusCode}`;
                  reject(new Error(error));
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
        const url2 = `${surveyViewerServiceUrl}${participantId}`;
        const client = await auth.getIdTokenClient(surveyViewerServiceUrl);
        const res = await client.request({url2});
        console.info(res.data);

        // ----
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
