const app = require('./application/app');
const config = require('./config');

const application = app(
  config({
    baseName: 'frame',
    gatewayAddress: 'http://localhost:3000/gateway',
    requestLogging: true,
  }),
);

const port = 3001;
application.listen(port, () => {
  // eslint-disable-next-line
  console.log(`Example app listening on port ${port}`);
});
