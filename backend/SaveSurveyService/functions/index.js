const Firestore = require('@google-cloud/firestore');
const { v4: uuidv4 } = require('uuid');

// read enironment variables
const {
  ENV_COLLECTION_NAME: collectionName,
} = process.env;

// intialize firestore and pub/sub
const database = new Firestore();

/**
 * Converts a Pub/Sub message to a survey object.
 * @param {object} json The Pub/Sub message as json object.
 * @returns An object that defines a new survey.
 */
const convertMessageToSurvey = (json) => {
  const survey = {
    name: json.name,
    status: 'CREATED',
    timestamp: Firestore.FieldValue.serverTimestamp(),
    participantIds: [],
    questions: [
      {
        question: 'bitte auswählen',
        value: undefined,
      },
    ],
    organizer: {
      name: json.replyToEmail.name,
      email: json.replyToEmail.email,
    },
  };

  // reorganize participants and prepare for easier firebase updates
  json.participants.forEach((participant) => {
    const id = uuidv4();
    survey[id] = {
      name: participant.name,
      email: participant.email,
    };

    survey.participantIds.push(id);
  });

  // enrich questions
  let questionValue = 1;
  json.questions.forEach((question) => {
    const surveyQuestion = {
      question: question.question,
      guid: uuidv4(),
      choices: [],
    };

    question.choices.forEach(({ answer }) => {
      surveyQuestion.choices.push({
        answer,
        value: questionValue,
      });

      questionValue += 1;
    });

    survey.questions.push(surveyQuestion);
  });

  return survey;
};

/**
 * Background Cloud Function to be triggered by Pub/Sub.
 * Creates a mew survey from the message data and
 * sends the survey to firestore.
 * @param {object} message The Pub/Sub message.
 */
const onMessagePublished = async (message) => {
  // parse message and covert to survey
  const json = JSON.parse(Buffer.from(message.data, 'base64').toString());
  const survey = convertMessageToSurvey(json);

  // send to firestore
  const docRef = database.collection(collectionName).doc(uuidv4());
  await docRef.set(survey);
};

/**
 * Background Cloud Function to be triggered by Pub/Sub.
 */
exports.SaveSurveyService = onMessagePublished;
