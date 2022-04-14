const { PubSub } = require('@google-cloud/pubsub');
const uuid = require('uuid');

const pubSubClient = new PubSub();

/**
 * Initializes a pub/sub client.
 * @param {object} options An options object.
 * @param {string} options.topicName The name of the pub/sub topic.
 * @returns A new pub/sub client.
 */
const initialize = (options) => {
  const { topicName } = options;

  const client = {
    /**
     * Publish a message to pub/sub.
     * @param {object} message The message that is sent.
     * @param {string} message.internalSurveyId The internal survey id.
     * @param {string} message.participantId The id of the survey participant.
     * @param {Array} message.results The survey result of the participant.
     *  Entries are objects of { questionId, choiceId }.
     */
    publish: async (message) => {
      const {
        internalSurveyId,
        participantId,
        results,
      } = message;

      const resultsJson = Array.from(results.map(({ questionId, choiceId }) => `{"questionId":"${questionId}","choiceId":"${choiceId}"}`)).join(',');
      const json = `{"processId":"${uuid.v4()}","surveyResult":{"documentId":"","created":"","parentDocumentId":"${internalSurveyId}","participantId":"${participantId}","isSuggested":false,"results":[${resultsJson}]}}`;
      const data = Buffer.from(json);
      await pubSubClient
        .topic(topicName)
        .publishMessage({ data });
    },
  };

  return client;
};

module.exports = initialize;
