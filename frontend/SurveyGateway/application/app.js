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

  console.log(framePath); // eslint-disable-line
  console.log(frameTarget); // eslint-disable-line
  console.log(votePath); // eslint-disable-line
  console.log(voteTarget); // eslint-disable-line

  const app = express();
  app.use((req, res, next) => {
    console.log(`${req.method} ${req.originalUrl} ${req.url} ${req.hostname}`); // eslint-disable-line
    next();
  });

  app.use(framePath, createProxyMiddleware({
    changeOrigin: true,
    target: frameTarget,
    pathRewrite: {
      '^/gateway': '',
    },
  }));

  app.use(votePath, createProxyMiddleware({
    changeOrigin: true,
    target: voteTarget,
    pathRewrite: {
      '^/gateway': '',
    },
  }));

  return app;
};

module.exports = initialize;
