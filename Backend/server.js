// Example using Express.js
const express = require('express');
const cors = require('cors');
const bodyParser = require('body-parser');
const fs = require('fs');
const date = require('date-and-time');


const server = express();

// Middleware to parse JSON data
server.use(express.json());
// Enable CORS for all routes
server.use(cors());
server.use(bodyParser.json());
server.use(bodyParser.urlencoded({ extended: true }));
// Example defining a route in Express
server.get('/', (req, res) => {
    res.send('<h1>Hello, Express.js Server!</h1>');
});

// Handle POST requests to save data
server.post('/api/save-data', (req, res) => {
    //const { name, message } = req.body;
    //const content = `Name: ${name}\nMessage: ${message}\n`;
    let content = JSON.stringify(req.body, null, 2); // The third argument (2) adds indentation for readability
    // console.log(content)
    const now = new Date();
    const formattedDateTime = date.format(now, 'YYYY-MM-DD-HH-mm-ss');
    const filePath = `./datalogs/${formattedDateTime}.txt`;
      // Save data to a file (e.g., data.txt)
    // fs.appendFile(filePath, content, (err) => {
    //   if (err) {
    //     console.error('Error saving data:', err);
    //     res.status(500).send('Error saving data');
    //   } else {
    //     console.log('Data saved successfully!');
    //     res.status(200).send('Data saved successfully');
    //   }
    // });
     //save data  hex base in file
    fs.appendFile(filePath,Buffer.from(content).toString('hex') , (err) => {
      if (err) {
        console.error('Error saving data:', err);
        res.status(500).send('Error saving data');
      } else {
        console.log('Data saved successfully!');
        res.status(200).send('Data saved successfully');
      }
    });
  });

// Include route files
//const usersRoute = require('./routes/users');
//const productsRoute = require('./routes/products');


// Use routes
//app.use('/users', usersRoute);
//app.use('products', productsRoute);



// Example specifying the port and starting the server
const port = process.env.PORT || 3000; // You can use environment variables for port configuration
server.listen(port, () => {
    console.log(`Server is running on port ${port}`);
});