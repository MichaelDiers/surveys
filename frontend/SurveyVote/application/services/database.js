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
  };

  return database;
};

module.exports = initialize;
