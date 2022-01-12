const DB_FIELD_PARTICIPATE_IDS = 'participantIds';

const initialize = (config = {}) => {
  const {
    database,
    collectionName,
  } = config;

  const controller = {
    thankyou: async (participantId) => {
      const snapshot = await database.collection(collectionName).where(DB_FIELD_PARTICIPATE_IDS, 'array-contains', participantId).limit(1).get();
      if (!snapshot.empty) {
        const survey = snapshot.docs[0].data();
        const participant = survey.participants.find((p) => p.guid === participantId);

        const options = {
          answers: [],
          participantName: participant.name,
          surveyName: survey.name,
          link: `../participate/${participantId}`,
        };

        // extract survey result
        if (participant.hasVoted === true) {
          survey.questions.forEach((question) => {
            question.choices.forEach((choice) => {
              if (choice.value === participant[question.guid]) {
                options.answers.push({ question: question.question, answer: choice.answer });
              }
            });
          });
        }

        return {
          view: 'thankyou/index',
          options,
        };
      }

      // handle unknown survey
      return {
        view: 'thankyou/index',
      };
    },
  };

  return controller;
};

module.exports = initialize;
