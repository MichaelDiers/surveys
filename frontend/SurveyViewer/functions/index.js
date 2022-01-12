const admin = require('firebase-admin');
const functions = require('firebase-functions');
const { PubSub } = require('@google-cloud/pubsub');

const app = require('./app/index');

admin.initializeApp();

const {
  collectionname: collectionName,
  projectid: projectId,
  topicname: topicNameEvaluateSurvey,
} = functions.config().surveyviewer;

const database = admin.firestore();
const pubsub = new PubSub({ projectId });

exports.SurveyViewer = functions.https.onRequest(
  app({
    database,
    pubsub,
    topicNameEvaluateSurvey,
    collectionName,
  }),
);
