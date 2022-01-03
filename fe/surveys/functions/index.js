const functions = require('firebase-functions');
const app = require('./app/index');

exports.surveys = functions.https.onRequest(app());
