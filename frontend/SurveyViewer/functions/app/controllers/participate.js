const DB_FIELD_PARTICIPATE_IDS = 'participantIds';
const DB_VALUE_STATUS_CLOSED = 'CLOSED';

const initialize = (config = {}) => {
  const {
    database,
    pubsub,
    topicNameEvaluateSurvey,
    collectionName,
  } = config;

  const controller = {
    updateSurvey: async (surveyResult) => {
      const result = JSON.parse(JSON.stringify(surveyResult));
      delete result._csrf; // eslint-disable-line no-underscore-dangle
      const data = Buffer.from(JSON.stringify(result));
      await pubsub.topic(topicNameEvaluateSurvey).publishMessage({ data });
      delete result.participantId;
      return Object.values(result).join('/');
    },
    viewSurvey: async (participantId) => {
      const snapshot = await database.collection(collectionName).where(DB_FIELD_PARTICIPATE_IDS, 'array-contains', participantId).limit(1).get();
      if (!snapshot.empty) {
        const survey = snapshot.docs[0].data();
        const participant = survey.participants.find((p) => p.guid === participantId);

        // pre-select answers if participant has already voted
        if (participant.hasVoted === true) {
          survey.questions.forEach((question, qi) => {
            question.choices.forEach((choice, ci) => {
              survey.questions[qi].choices[ci].isSelected = choice.value === participant[question.guid]; // eslint-disable-line max-len
            });
          });
        }

        if (survey.status !== DB_VALUE_STATUS_CLOSED) {
          const options = {
            participantName: survey.participants.find((p) => p.guid === participantId).name,
            participantId,
            surveyName: survey.name,
            questions: survey.questions,
          };

          return {
            view: 'participate/index',
            options,
          };
        }

        // handle closed state
        const options = {
          participantName: survey.participants.find((p) => p.guid === participantId).name,
          participantId,
          surveyName: survey.name,
          questions: survey.questions,
          closed: true,
        };

        return {
          view: 'participate/closed',
          options,
        };
      }

      return {
        view: 'participate/nodata',
      };
    },
  };

  return controller;
};

module.exports = initialize;
