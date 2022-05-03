const initialize = () => {
  const controller = {
    index: async (req, res, next) => {
      try {
        res.render('footer/index');
      } catch (err) {
        next(err);
      }
    },
  };

  return controller;
};

module.exports = initialize;
