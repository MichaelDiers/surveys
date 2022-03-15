const express = require('express');
const { createProxyMiddleware } = require('http-proxy-middleware');

/**
 * Initializes an express application.
 * @param {object} config A configuration object.
 * @param {string} config.frame The target url for the frame.
 * @returns {express.Express} An express application.
 */
const initialize = (config = {}) => {
  const {
    framePath = '/frame',
    frameTarget,
    votePath = '/vote',
    voteTarget,
  } = config;

  const app = express();
  // csurfMiddleware({ router: app });

  app.use(framePath, createProxyMiddleware({
    changeOrigin: true,
    target: frameTarget,
    pathRewrite: {
      '^/gateway': '',
    },
    logLevel: 'debug',
  }));

  app.use(votePath, createProxyMiddleware({
    changeOrigin: true,
    target: voteTarget,
    pathRewrite: {
      '^/gateway': '',
    },
    logLevel: 'debug',
  }));

  // app.use('/', router);
  return app;
};

module.exports = initialize;
