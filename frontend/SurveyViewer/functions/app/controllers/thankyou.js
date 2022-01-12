const DB_FIELD_PARTICIPATE_IDS = 'participantIds';

const initialize = (config = {}) => {
  const {
    database,
    collectionName,
  } = config;

  const controller = {
    thankyou: async (participantId, values) => {
      const snapshot = await database.collection(collectionName).where(DB_FIELD_PARTICIPATE_IDS, 'array-contains', participantId).limit(1).get();
      if (!snapshot.empty) {
        const survey = snapshot.docs[0].data();
        const participant = survey.participants.find((p) => p.guid === participantId);

        const selectedValues = values.split('/').map((x) => parseInt(x, 10));
        const preLink = new Array(selectedValues.length + 2).join('../');

        const options = {
          answers: [],
          participantName: participant.name,
          surveyName: survey.name,
          link: `${preLink}/../../participate/${participantId}`,
          staticPreLink: preLink,
        };

        // extract survey result
        survey.questions.forEach((question) => {
          question.choices.forEach((choice) => {
            if (selectedValues.includes(choice.value)) {
              options.answers.push({ question: question.question, answer: choice.answer });
            }
          });
        });

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
