const express = require('express')
const url   = require('url');
const fs = require('fs');

const app = express()


app.get('/', function (req, res) {
 
  console.log(req.url);
  const data = 'Radiant!';
  fs.writeFile('example.txt', data, (err) => {
    if (err) {
      console.error(err);
      return;
    }
    console.log('Data written to file');
  });
})
/**route like /users/:userId , where userId is a dynamic parameter
 * :userId  a dynamic parameter that can be accessed using the req.params object
 */
app.get('/users/:userId', function (req, res) {
  res.send('User ID: ' + req.params.userId);
});


/**Middleware for authenticate */
app.use('/dashboard', function (req, res, next) {
  // Perform authentication
  next();
});
/**middleware for log request */
app.use(function (req, res, next) {
  console.log('Incoming request: ' + req.url);
  next();
});

/**Express.js static middleware
 * Here's an example of how to use the static middleware to serve static files from a directory named public
 */
app.use(express.static('public'));

app.listen(3000)