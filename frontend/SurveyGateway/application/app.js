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
    votePath = '/vote',
    voteTarget,
  } = config;

  const app = express();
  // csurfMiddleware({ router: app });

  app.use(express.urlencoded({ extended: false }));
  app.use(express.json());
  app.use((req, res, next) => {
    // eslint-disable-next-line
    console.log(`${req.method} ${req.originalUrl} ${JSON.stringify(req.body)}`);
    next();
  });
  app.use(framePath, createProxyMiddleware({
    changeOrigin: true,
    target: frameTarget,
    pathRewrite: {
      '^/gateway': '',
    },
    logLevel: 'debug',
    onProxyReq: fixRequestBody,
  }));

  app.use(votePath, createProxyMiddleware({
    changeOrigin: true,
    target: voteTarget,
    pathRewrite: {
      '^/gateway': '',
    },
    logLevel: 'debug',
    onProxyReq: fixRequestBody,
  }));

  // app.use('/', router);
  return app;
};

module.exports = initialize;
