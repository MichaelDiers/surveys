const uuid = require('uuid');

/**
 * Creates the view data for a participant of a survey.
 * @param {object} config An options object.
 * @param {object} config.survey The survey object from firestore database.
 * @param {object} config.participantId The id of the survey participant.
 * @returns An options object that is used for rendering.
 */
const buildViewDataForSurvey = (config) => {
  const {
    internalSurveyId,
    survey: {
      name: surveyName,
      info: surveyInfo,
      questions: surveyQuestions,
      participants,
    },
    participantId,
    surveyResults,
  } = config;

  const participantName = participants.find(({ id }) => id === participantId).name;

  // process and set previous choices
  if (surveyResults && surveyResults.length > 0) {
    // sort in order to find the last result
    surveyQuestions.forEach((question, index) => {
      const result = surveyResults[0].results.find(({ questionId }) => questionId === question.id);
      if (result) {
        const surveyChoice = question.choices.find((choice) => choice.id === result.choiceId);
        surveyChoice.isSelected = true;
        if (surveyChoice.selectable) {
          surveyQuestions[index].choices = question.choices.filter((choice) => choice.selectable);
        }
      }
    });
  }

  const options = {
    participantName,
    surveyName,
    surveyInfo,
    surveyQuestions,
    surveyId: internalSurveyId,
    participantId,
    formId: uuid.v4(),
  };

  return options;
};

const buildDataForThankYouView = (options) => {
  const {
    participantId,
    results,
    survey: {
      name: surveyName,
      info: surveyInfo,
      participants,
      questions,
    },
    internalSurveyId,
    addPushState = true,
  } = options;

  const participantName = participants.find(({ id }) => id === participantId).name;

  const surveyResults = results.map(({ questionId, choiceId }) => {
    const question = questions.find(({ id }) => id === questionId);
    return {
      question: question.question,
      answer: question.choices.find(({ id }) => id === choiceId).answer,
    };
  });

  return {
    participantName,
    surveyName,
    surveyInfo,
    results: surveyResults,
    addPushState,
    pushStateUrl: `../../../../frame/vote/thankyou/${internalSurveyId}/${participantId}`,
  };
};

/**
 * Initializes the vote controller.
 * @param {object} config A configuration object.
 * @param {object} config.database Access the surveys database.
 * @param {object} config.pubSubClient Access the google cloud pub/sub.
 * @returns The initialized controller object.
 */
const initialize = (config = {}) => {
  const {
    database,
    pubSubClient,
  } = config;

  const controller = {
    /**
     * Renders the survey is closed view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    closed: async (req, res, next) => {
      try {
        res.render('vote/closed');
      } catch (err) {
        next(err);
      }
    },

    /**
     * Loads the requested survey for a participant and renders the view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    index: async (req, res, next) => {
      try {
        const { surveyId, participantId } = req.params;

        // database operations
        const surveyPromise = database.read({ surveyId, participantId });
        const surveyResultsPromise = database.readSurveyResults({ surveyId, participantId });
        const surveyIsClosedPromise = database.isSurveyClosed({ surveyId });

        // check if survey exists
        const survey = await surveyPromise;
        if (!survey) {
          res.render('vote/unknown');
          return;
        }

        // survey is closed
        if (await surveyIsClosedPromise) {
          res.redirect('../../closed');
          return;
        }

        // render the survey
        const surveyResults = await surveyResultsPromise;
        res.render('vote/index', buildViewDataForSurvey({
          survey,
          participantId,
          internalSurveyId: surveyId,
          surveyResults,
        }));
      } catch (err) {
        next(err);
      }
    },

    /**
     * Submit a survey result.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    submit: async (req, res, next) => {
      try {
        const {
          surveyId: internalSurveyId,
          participantId,
        } = req.body;

        const survey = await database.read({ surveyId: internalSurveyId, participantId });
        if (!survey) {
          console.error(`voteController.submit: Cannot read survey with id ${internalSurveyId} for participant ${participantId}`); // eslint-disable-line
          res.render('vote/unknown');
          return;
        }

        const message = {
          internalSurveyId,
          participantId,
          results: [],
        };

        survey.questions.forEach(({ id, choices }) => {
          const submitChoiceId = req.body[id];
          if (!submitChoiceId || choices.every((choice) => choice.id !== submitChoiceId)) {
            console.error(`voteController.submit: Missing question ${id} for survey ${internalSurveyId} for participant ${participantId}`); // eslint-disable-line
            res.render('vote/unknown');
          } else {
            message.results.push({ questionId: id, choiceId: submitChoiceId });
          }
        });

        await pubSubClient.publish(message);

        res.render(
          'vote/thankyou',
          buildDataForThankYouView({
            survey,
            participantId,
            results: message.results,
            internalSurveyId,
          }),
        );
      } catch (err) {
        next(err);
      }
    },

    /**
     * Render the thank you for participating view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    thankyou: async (req, res, next) => {
      try {
        const {
          surveyId: internalSurveyId,
          participantId,
        } = req.params;

        const surveyPromise = await database.read({ surveyId: internalSurveyId, participantId });
        const surveyResultsPromise = database.readSurveyResults({
          surveyId: internalSurveyId,
          participantId,
        });

        const survey = await surveyPromise;
        if (!survey) {
          console.error(`voteController.thankyou: Cannot read survey with id ${internalSurveyId} for participant ${participantId}`); // eslint-disable-line
          res.render('vote/unknown');
          return;
        }

        const surveyResults = await surveyResultsPromise;
        if (!surveyResults) {
          res.render('vote/unknown');
          return;
        }

        res.render(
          'vote/thankyou',
          buildDataForThankYouView({
            survey,
            participantId,
            results: surveyResults[0].results,
            internalSurveyId,
            addPushState: false,
          }),
        );
      } catch (err) {
        next(err);
      }
    },
  };

  return controller;
};

module.exports = initialize;
