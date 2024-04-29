const express = require('express')
const url     = require('url');

const app = express()

app.get('/', function (req, res) {
  res.send('Hello World');
  console.log(req.url);
})

app.listen(3000)