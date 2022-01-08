const functions = require('firebase-functions');
const admin = require('firebase-admin');
const { PubSub } = require('@google-cloud/pubsub');

admin.initializeApp();

const {
  collectionname: collectionName,
  projectid: projectId,
  documentid: documentId,
  topicname: topicName,
} = functions.config().surveytester;

const database = admin.firestore();
const pubsub = new PubSub({ projectId });

exports.TestSurvey = functions.https.onRequest(async (req, res) => {
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
});
