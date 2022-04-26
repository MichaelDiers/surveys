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
      // handle local and server environment
      const destination = urlSplit.length === 1 ? req.originalUrl : urlSplit[1];
      const container = urlSplit.length === 1 ? req.originalUrl.split('/')[1] : req.originalUrl.split('/frame')[1].split('/')[1];

      const placeholderAjax = `${gatewayAddress}${destination}`;
      const containerId = `${container}-container`;

      const subViewBase = destination.split('/')[1];
      const placeholderAjaxHeader = `${gatewayAddress}/${subViewBase}/header`;
      const placeholderAjaxFooter = `${gatewayAddress}/${subViewBase}/footer`;

      res.locals.pubLocals.files.css.push(`${gatewayAddress}/${subViewBase}/public/${subViewBase}.min.css`);
      res.locals.pubLocals.files.js.push(`${gatewayAddress}/${subViewBase}/public/${subViewBase}.min.js`);

      res.render('index/index', {
        containerId, placeholderAjax, placeholderAjaxFooter, placeholderAjaxHeader,
      });
    },
  };

  return controller;
};

module.exports = initialize;
