const { Router } = require('express');

const initialize = (config = {}) => {
  const {
    controller,
    router = Router(),
  } = config;

  router.get('/pid/:participantId/*', async (req, res) => {
    const { view, options } = await controller.thankyou(req.params.participantId, req.params[0]);
    res.render(view, options);
  });

  return router;
};

module.exports = initialize;
