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

/**
 * Pubsub trigger for topic with name topicName.
 * - enriches the data of the pubsub message
 * - saves the messages to firestore
 * - publishes the message to topic topicNameCreated if the data is saved
 */
exports.SaveSurveyService = functions.pubsub.topic(topicName).onPublish(async (message) => {
  const { json } = message;
  json.status = 'CREATED';
  const docRef = database.collection(collectionName).doc();
  await docRef.set(json);
  json.id = docRef.id;

  const data = Buffer.from(JSON.stringify(json));
  await pubsub.topic(topicNameCreated).publishMessage({ data });
});
