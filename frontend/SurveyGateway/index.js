/**
 * Used for local testing.
 */
const app = require('./application/app');

const application = app({
  framePath: '/gateway/frame',
  frameTarget: 'http://localhost:3001',
  votePath: '/gateway/vote',
  voteTarget: 'http://localhost:3002',
});

const port = 3000;

application.listen(port, () => {
  // eslint-disable-next-line
  console.log(`Example app listening on port ${port}`);
});
