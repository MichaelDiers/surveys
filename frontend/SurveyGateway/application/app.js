const express = require('express');
const { createProxyMiddleware, fixRequestBody } = require('http-proxy-middleware');

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
    terminatePath = '/terminate',
    terminateTarget,
    votePath = '/vote',
    voteTarget,
    requestLogging,
  } = config;

  const app = express();

  app.use(express.urlencoded({ extended: false }));
  app.use(express.json());

  if (requestLogging) {
    app.use((req, res, next) => {
      // eslint-disable-next-line
      console.log(`${req.method} ${req.originalUrl} ${JSON.stringify(req.body)}`);
      next();
    });
  }

  const logLevel = requestLogging ? 'info' : 'error';

  app.use(framePath, createProxyMiddleware({
    changeOrigin: true,
    target: frameTarget,
    pathRewrite: {
      '^/gateway': '',
    },
    onProxyReq: fixRequestBody,
    logLevel,
  }));

  app.use(terminatePath, createProxyMiddleware({
    changeOrigin: true,
    target: terminateTarget,
    pathRewrite: {
      '^/gateway': '',
    },
    onProxyReq: fixRequestBody,
    logLevel,
  }));

  app.use(votePath, createProxyMiddleware({
    changeOrigin: true,
    target: voteTarget,
    pathRewrite: {
      '^/gateway': '',
    },
    onProxyReq: fixRequestBody,
    logLevel,
  }));

  return app;
};

module.exports = initialize;
