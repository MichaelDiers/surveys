const functions = require('firebase-functions');
const admin = require('firebase-admin');
const { PubSub } = require('@google-cloud/pubsub');
const { v4: uuidv4 } = require('uuid');
const { firestore } = require('firebase-admin');

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
  json.participants.forEach((_, index) => {
    json.participants[index].guid = uuidv4();
  });
  json.participantIds = json.participants.map((participant) => participant.guid);
  let value = 1;
  json.questions.forEach((question, index) => {
    json.questions[index].guid = uuidv4();
    question.choices.forEach((choice, qindex) => {
      if (choice.hasNoValue !== true) {
        json.questions[index].choices[qindex].value = value;
        value += 1;
      }
    });
  });
  json.timestamp = firestore.FieldValue.serverTimestamp();

  const docRef = database.collection(collectionName).doc(uuidv4());
  await docRef.set(json);
  json.id = docRef.id;

  const data = Buffer.from(JSON.stringify(json));
  await pubsub.topic(topicNameCreated).publishMessage({ data });
});
