const app = require('./application/app');

const application = app();

const port = 3001;
application.listen(port, () => {
  console.log(`Example app listening on port ${port}`)
});
