const { Router } = require('express');

const initialize = (config = {}) => {
  const {
    controller,
    router = Router(),
  } = config;

  router.get('/:participantId', async (req, res) => {
    const { view, options } = await controller.thankyou(req.params.participantId);
    res.render(view, options);
  });

  return router;
};

module.exports = initialize;
