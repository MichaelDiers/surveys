const initialize = () => {
  const controller = {
    index: async (req, res) => {
      res.render('header/index');
    },
  };

  return controller;
};

module.exports = initialize;
