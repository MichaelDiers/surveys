const { Router } = require('express');

const initialize = (config = {}) => {
  const {
    router = Router(),
  } = config;

  router.get('/:participantId', (req, res) => {
    res.render('thankyou/index', { link: `../participate/${req.params.participantId}` });
  });

  return router;
};

module.exports = initialize;
