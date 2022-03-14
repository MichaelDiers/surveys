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
      console.log(req.originalUrl); // eslint-disable-line
      console.log(req.baseUrl); // eslint-disable-line
      console.log(req.path); // eslint-disable-line
      console.log(req.originalUrl.split('/frame')); // eslint-disable-line
      console.log(gatewayAddress); // eslint-disable-line

      const urlSplit = req.originalUrl.split('/frame');
      const destination = urlSplit.length === 1 ? req.originalUrl : urlSplit[1];

      const placeholder = `${gatewayAddress}${destination}`;
      res.render('index/index', { placeholder });
    },
  };

  return controller;
};

module.exports = initialize;
