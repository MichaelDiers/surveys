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
  } = config;

  const participantName = participants.find(({ id }) => id === participantId).name;
  surveyQuestions.sort((a, b) => a.order - b.order);
  surveyQuestions.forEach(({ choices }) => {
    choices.sort((a, b) => a.order - b.order);
  });

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
      const isClosed = await database.isSurveyClosed({ surveyId });
      if (isClosed) {
        res.redirect('../../closed');
      } else {
        const survey = await database.read({ surveyId, participantId });
        if (survey) {
          res.render('vote/index', buildViewDataForSurvey({ survey, participantId, internalSurveyId: surveyId }));
        } else {
          res.render('vote/unknown');
        }
      }
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
