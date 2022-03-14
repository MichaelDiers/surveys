const initialize = () => {
  const controller = {
    index: async (req, res) => {
      res.render('index/index');
    },
  };

  return controller;
};

module.exports = initialize;
