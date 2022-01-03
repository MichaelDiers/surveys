const functions = require('firebase-functions');
const app = require('./app/index');

exports.adminfe = functions.https.onRequest(app());
