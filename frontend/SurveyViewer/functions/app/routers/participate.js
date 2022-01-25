const { Router } = require('express');

const initialize = (config = {}) => {
  const {
    controller,
    router = Router(),
  } = config;

  router.get('/:participantId', async (req, res) => {
    const result = await controller.viewSurvey(req.params.participantId);
    const { view, options } = result;
    res.render(view, options);
  });

  router.post('/:participantId', async (req, res) => {    
    try {
      var result = await controller.viewSurveyAjax(req.params.participantId);     
      console.log(result) ;
      if (result) {
        res.json(result).end();
      } else {
        res.status(404).end();
      }
    } catch {
      res.status(404).end();
    }
  });

  return router;
};

module.exports = initialize;
