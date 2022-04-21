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
      const urlSplit = req.originalUrl.split('/frame');
      const destination = urlSplit.length === 1 ? req.originalUrl : urlSplit[1];
      const container = req.originalUrl.split('/frame')[1].split('/')[1];

      const placeholder = `${gatewayAddress}${destination}`;
      const containerId = `${container}-container`;

      res.render('index/index', { containerId, placeholder });
    },
  };

  return controller;
};

module.exports = initialize;
