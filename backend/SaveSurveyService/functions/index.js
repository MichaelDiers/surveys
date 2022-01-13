const Firestore = require('@google-cloud/firestore');
const { PubSub } = require('@google-cloud/pubsub');
const { v4: uuidv4 } = require('uuid');

const {
  ENV_COLLECTION_NAME: collectionName,
  ENV_TOPIC_NAME_PUB: topicNamePub,
} = process.env;

const database = new Firestore();
const pubsub = new PubSub();

/**
 * Pubsub trigger for topic with name topicName.
 * - enriches the data of the pubsub message
 * - saves the messages to firestore
 * - publishes the message to topic topicNameCreated if the data is saved
 */
exports.SaveSurveyService = async (message) => {
  const json = JSON.parse(Buffer.from(message.data, 'base64').toString());

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
  json.timestamp = Firestore.FieldValue.serverTimestamp();

  const docRef = database.collection(collectionName).doc(uuidv4());
  await docRef.set(json);
  json.id = docRef.id;

  const data = Buffer.from(JSON.stringify(json));
  await pubsub.topic(topicNamePub).publishMessage({ data });
};
