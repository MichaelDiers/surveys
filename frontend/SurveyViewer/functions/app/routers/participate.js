const { Router } = require('express');

const initialize = (config = {}) => {
  const {
    controller,
    router = Router(),
  } = config;

  router.get('/:surveyId', async (req, res) => {
    const result = await controller.viewSurvey(req.params.surveyId);
    const { view, options } = result;
    res.render(view, options);
  });

  router.post('/', (req, res) => {
    res.redirect(303, `../thankyou/${req.body.participantId}`);
  });

  return router;
};

module.exports = initialize;
