const app = require('./application/app');

const application = app({ baseAddress: '/frame', gatewayAddress: 'http://localhost:3000/gateway' });

const port = 3001;
application.listen(port, () => {
  console.log(`Example app listening on port ${port}`);
});
