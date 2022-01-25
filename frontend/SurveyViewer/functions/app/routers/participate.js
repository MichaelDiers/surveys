const { Router } = require('express');

const initialize = (config = {}) => {
  const {
    controller,
    router = Router(),
  } = config;

  router.get('/id/:participantId', async (req, res) => {
    const result = await controller.viewSurvey(req.params.participantId);
    const { view, options } = result;
    res.render(view, options);
  });

  router.post('/id/:participantId', async (req, res) => {
    try {
      const result = await controller.viewSurveyAjax(req.params.participantId);
      if (result) {
        res.json(result).end();
      } else {
        res.status(404).end();
      }
    } catch {
      res.status(404).end();
    }
  });

  router.post('/submit', async (req, res) => {
    console.log(req.body);
    res.status(200).end();
  });

  return router;
};

module.exports = initialize;
