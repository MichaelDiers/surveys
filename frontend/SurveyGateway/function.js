/**
 * Google gloud http function.
 */
const app = require('./application/app');

const {
  ENV_GATEWAY_FRAME_TARGET: frameTarget,
} = process.env;

exports.gateway = app({ frameTarget });
