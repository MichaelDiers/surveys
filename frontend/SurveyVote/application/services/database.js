const { Firestore } = require('@google-cloud/firestore');

const firestore = new Firestore();

/**
 * Intialize a database object.
 * @param {config} config A configuration object.
 * @param {string} config.surveysCollectionName The name of the collection to be accessed.
 * @returns The intialized database.
 */
const initialize = (config) => {
  const {
    surveysCollectionName,
    surveyStatusCollectionName,
  } = config;

  const database = {
    /**
     * Read a survey with document id and a given id of a participant.
     * @param {object} options An options object.
     * @param {string} options.surveyId The id of the survey document.
     * @param {string} options.participantId The id of the participant.
     * @returns A survey object.
     */
    read: async (options) => {
      const { surveyId, participantId } = options;
      const snapshot = await firestore.collection(surveysCollectionName).doc(surveyId).get();
      if (snapshot.exists) {
        const document = snapshot.data();
        if (document.participants.some(({ id }) => id === participantId)) {
          return document;
        }
      }

      return null;
    },

    /**
     * Check if the survey has a status closed document.
     * @param {object} options An options object.
     * @param {string} options.surveyId The id of the survey document.
     * @returns True if the survey is closed and false otherwise.
     */
    isSurveyClosed: async (options) => {
      const { surveyId } = options;
      const snapshot = await firestore
        .collection(surveyStatusCollectionName)
        .where('internalSurveyId', '==', surveyId)
        .where('status', '==', 'Closed')
        .limit(1)
        .get();
      return snapshot.size === 1;
    },
  };

  return database;
};

module.exports = initialize;
