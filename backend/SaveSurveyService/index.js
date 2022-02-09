const Firestore = require('@google-cloud/firestore');
const { PubSub } = require('@google-cloud/pubsub');

const Survey = require('./models/survey');

// read enironment variables
const {
  ENV_COLLECTION_NAME: collectionName,
  ENV_STATUS_CREATED: statusCreated,
  ENV_TOPIC_NAME: topicName,
} = process.env;

// intialize firestore and pub/sub
const database = new Firestore();
const pubsub = new PubSub();

/**
 * Background Cloud Function to be triggered by Pub/Sub.
 * Creates a mew survey from the message data and
 * sends the survey to firestore.
 * @param {object} message The Pub/Sub message.
 */
const onMessagePublished = async (message) => {
  try {
    // parse message and covert to survey
    const json = JSON.parse(Buffer.from(message.data, 'base64').toString());
    const survey = new Survey(json);

    // send to firestore
    const docRef = database.collection(collectionName).doc(survey.id);
    await docRef.set(survey);

    // send status update
    const statusUpdate = `{"surveyId":"${survey.id}","participantId": "", "status": "${statusCreated}"}`;
    const data = Buffer.from(statusUpdate);
    await pubsub.topic(topicName).publishMessage({ data });
  } catch (error) {
    // eslint-disable-next-line no-console
    console.error(`Unexpected error: ${error.message}`);
  }
};

/**
 * Background Cloud Function to be triggered by Pub/Sub.
 */
exports.SaveSurveyService = onMessagePublished;
