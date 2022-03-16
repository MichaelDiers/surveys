const initialize = (options = {}) => {
  const {
    appRoute = '/',
    baseName,
    gatewayAddress = '/gateway',
    requestLogging = false,
    publicLocalFolder = './application/public',
    publicRoute = '/public',
    viewEngine = 'pug',
    viewLocalFolder = './application/views',
    css = [],
    js = [],
    lang = 'de',
    surveysCollectionName,
    surveyStatusCollectionName,
  } = options;

  const cssFiles = [`${gatewayAddress}/${baseName}${publicRoute}/${baseName}.min.css`, ...css];
  const jsFiles = [`${gatewayAddress}/${baseName}${publicRoute}/${baseName}.min.js`, ...js];
  const favicon = `${gatewayAddress}/${baseName}${publicRoute}/favicon.ico`;
  const csurfCookieName = `_csrf_${baseName}`;

  const config = {
    appRoute,
    gatewayAddress,
    publicLocalFolder,
    publicRoute,
    requestLogging,
    viewEngine,
    viewLocalFolder,
    files: {
      css: cssFiles,
      js: jsFiles,
      favicon,
    },
    lang,
    surveysCollectionName,
    surveyStatusCollectionName,
    csurfCookieName,
  };

  return config;
};

module.exports = initialize;
