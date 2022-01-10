const initialize = (config = {}) => {
  const {
    database,
  } = config;

  const controller = {
    viewSurvey: async (participantId) => {
      const snapshot = await database.collection('surveys').where('participantIds', 'array-contains', participantId).limit(1).get();
      if (!snapshot.empty) {
        const survey = snapshot.docs[0].data();
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

      return {
        view: 'participate/nodata',
      };
    },
  };

  return controller;
};

module.exports = initialize;
