const Firestore = require('@google-cloud/firestore');
const { PubSub } = require('@google-cloud/pubsub');

const {
  ENV_COLLECTION_NAME: collectionName = 'test-surveys',
  ENV_DOCUMENT_ID: documentId = 'survey-tester-input',
  ENV_TOPIC_NAME_PUB: topicNamePub = 'SAVE_SURVEY',
} = process.env;

const database = new Firestore();
const pubsub = new PubSub();

// https://stackoverflow.com/questions/16167581/sort-object-properties-and-json-stringify
const orderedStringify = (obj) => {
  const allKeys = [];
  const seen = {};
  JSON.stringify(obj, (key, value) => {
    if (!(key in seen)) {
      allKeys.push(key);
      seen[key] = null;
    }

    return value;
  });

  allKeys.sort();
  return JSON.stringify(obj, allKeys);
};

exports.SurveyTester = async (req, res) => {
  const docRef = database.collection(collectionName).doc(documentId);
  const doc = await docRef.get();

  if (doc.exists) {
    const stringify = orderedStringify(doc.data());
    const data = Buffer.from(stringify);
    await pubsub.topic(topicNamePub).publishMessage({ data });
    res.send('OK').end();
  } else {
    res.send('no data').end();
  }
};
