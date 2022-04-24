const initialize = () => {
  const controller = {
    index: async (req, res) => {
      res.render('footer/index');
    },
  };

  return controller;
};

module.exports = initialize;
