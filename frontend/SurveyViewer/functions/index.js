const functions = require('firebase-functions');
const app = require('./app/index');

exports.SurveyViewer = functions.https.onRequest(app());
