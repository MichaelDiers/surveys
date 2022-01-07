const functions = require('firebase-functions');
const admin = require('firebase-admin');

admin.initializeApp();
const database = admin.firestore();

const {
  collectionname: collectionName,
  topicname: topicName,
} = functions.config().savesurveyservice;

exports.surveyStore = functions.pubsub.topic(topicName).onPublish(async (message) => {
  const docRef = database.collection(collectionName).doc();
  await docRef.set(message.json);
});
