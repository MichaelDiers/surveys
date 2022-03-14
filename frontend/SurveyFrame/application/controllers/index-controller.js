/**
 * Initializes the index controller.
 * @param {object} config A configuration object.
 * @param {gatewayAddress} config.gatewayAddress The base address of the gateway.
 * @returns The index controller object.
 */
const initialize = (config = {}) => {
  const {
    gatewayAddress,
  } = config;

  const controller = {
    index: async (req, res) => {
      const destination = req.originalUrl.split('/frame')[1];
      const placeholder = `${gatewayAddress}${destination}`;
      res.render('index/index', { placeholder });
    },
  };

  return controller;
};

module.exports = initialize;
