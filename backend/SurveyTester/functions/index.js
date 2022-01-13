const Firestore = require('@google-cloud/firestore');
const { PubSub } = require('@google-cloud/pubsub');

const {
  ENV_COLLECTION_NAME: collectionName,
  ENV_DOCUMENT_ID: documentId,
  ENV_TOPIC_NAME: topicName,
} = process.env;

const database = new Firestore();
const pubsub = new PubSub();

exports.SurveyTester = async (req, res) => {
  const docRef = database.collection(collectionName).doc(documentId);
  const doc = await docRef.get();

  if (doc.exists) {
    const data = doc.data();

    const buffer = Buffer.from(JSON.stringify(data));
    await pubsub.topic(topicName).publishMessage({ data: buffer });
    res.send('OK').end();
  } else {
    res.send('no data').end();
  }
};
