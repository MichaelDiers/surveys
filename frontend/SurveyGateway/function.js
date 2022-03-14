/**
 * Google gloud http function.
 */
const app = require('./application/app');

const {
  ENV_GATEWAY_FRAME_TARGET: frameTarget,
  ENV_GATEWAY_VOTE_TARGET: voteTarget,
} = process.env;

exports.gateway = app({ frameTarget, voteTarget });
