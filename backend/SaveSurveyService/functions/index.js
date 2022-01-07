const functions = require('firebase-functions');
const admin = require('firebase-admin');
const { PubSub } = require('@google-cloud/pubsub');

admin.initializeApp();
const database = admin.firestore();

const {
  collectionname: collectionName,
  topicname: topicName,
  projectid: projectId,
  topicnamecreated: topicNameCreated,
} = functions.config().savesurveyservice;

const pubsub = new PubSub({ projectId });

exports.SaveSurvey = functions.pubsub.topic(topicName).onPublish(async (message) => {
  const docRef = database.collection(collectionName).doc();
  await docRef.set(message.json);

  const data = Buffer.from(JSON.stringify(message.json));
  await pubsub.topic(topicNameCreated).publishMessage({ data });
});
