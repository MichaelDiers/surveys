const initialize = () => {
  const controller = {
    index: async (req, res) => {
      res.render('vote/index');
    },
  };

  return controller;
};

module.exports = initialize;
