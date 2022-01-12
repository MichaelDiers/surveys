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

  router.post('/', async (req, res) => {
    const values = await controller.updateSurvey(req.body);
    res.redirect(303, `../thankyou/pid/${req.body.participantId}/${values}`);
  });

  return router;
};

module.exports = initialize;
