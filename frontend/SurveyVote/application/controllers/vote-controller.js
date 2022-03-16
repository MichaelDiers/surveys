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
  surveyQuestions.sort((a, b) => a.order - b.order);
  surveyQuestions.forEach(({ choices }) => {
    choices.sort((a, b) => a.order - b.order);
  });

  if (surveyResults && surveyResults.length > 0) {
    surveyResults.sort((a, b) => b.created - a.created);
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

/**
 * Initializes the vote controller.
 * @param {object} config A configuration object.
 * @param {object} config.database Access the surveys database.
 * @returns The initialized controller object.
 */
const initialize = (config = {}) => {
  const {
    database,
  } = config;

  const controller = {
    /**
     * Renders the survey is closed view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    closed: async (req, res) => {
      res.render('vote/closed');
    },

    /**
     * Loads the requested survey for a participant and renders the view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    index: async (req, res) => {
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
    },

    /**
     * Submit a survey result.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    submit: (req, res) => {
      res.redirect(303, './thankyou');
    },

    /**
     * Render the thank you for participating view.
     * @param {express.Request} req The express request object.
     * @param {express.Response} res The express response object.
     */
    thankyou: (req, res) => {
      res.render('vote/thankyou');
    },
  };

  return controller;
};

module.exports = initialize;
