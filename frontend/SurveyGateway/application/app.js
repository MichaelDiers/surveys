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
  } = config;

  const app = express();

  app.use(framePath, createProxyMiddleware({
    target: frameTarget,
    pathRewrite: {
      '^/gateway': '',
    },
  }));

  return app;
};

module.exports = initialize;
