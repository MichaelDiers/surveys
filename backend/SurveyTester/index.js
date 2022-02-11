const Firestore = require('@google-cloud/firestore');
const { PubSub } = require('@google-cloud/pubsub');

const {
  ENV_COLLECTION_NAME: collectionName = 'test-surveys',
  ENV_DOCUMENT_ID: documentId = 'survey-tester-input',
  ENV_TOPIC_NAME_PUB: topicNamePub = 'SAVE_SURVEY',
} = process.env;

const database = new Firestore();
const pubsub = new PubSub();

exports.SurveyTester = async (req, res) => {
  const docRef = database.collection(collectionName).doc(documentId);
  const doc = await docRef.get();

  if (doc.exists) {
    const { json } = doc.data();
    const data = Buffer.from(json);
    await pubsub.topic(topicNamePub).publishMessage({ data });
    res.send('OK').end();
  } else {
    res.send('no data').end();
  }
};
