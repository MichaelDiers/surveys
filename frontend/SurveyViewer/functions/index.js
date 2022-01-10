const admin = require('firebase-admin');
const functions = require('firebase-functions');

const app = require('./app/index');

admin.initializeApp();
const database = admin.firestore();

exports.SurveyViewer = functions.https.onRequest(app({ database }));
