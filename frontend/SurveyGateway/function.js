/**
 * Google gloud http function.
 */
const app = require('./application/app');

const {
  ENV_GATEWAY_FRAME_TARGET: frameTarget,
  ENV_GATEWAY_VOTE_TARGET: voteTarget,
  ENV_REQUEST_LOGGING: requestLogging = false,
  ENV_GATEWAY_TERMINATE_TARGET: terminateTarget,
} = process.env;

exports.gateway = app({
  frameTarget,
  voteTarget,
  requestLogging,
  terminateTarget,
});
