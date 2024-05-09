const express = require('express')
const url = require('url');
const fs = require('fs');

const app = express()

app.get('/', function (req, res) {
    //  res.send('Hello World');
    console.log(req.url);

    const data = 'Hello, World!';
    //write async
    try {
        fs.writeFile('example.txt', data, (err) => {
            if (err) {
                console.error(err);
                return;
            }
            console.log('Data written to file');
        });
    } catch (err) {
        console.error('File not found: ' + err.path);
    }
    //read async
    try {
        fs.readFile('example.txt', (err, data) => {
            if (err) {
                console.error(err);
                return;
            }
            console.log(data.toString());
        });
    }
    catch (err) {
        if (err.code === 'EACCES') {
            console.error('Permission denied: ' + err.path);
          } else {
            console.error('Error occurred: ' + err);
          }
    }
    //add to exist file  ... async
    try {
        fs.appendFile('example.txt', 'This is new data!', function (err) {
            if (err) throw err;
            console.log('Data appended to file!');
        });
    } catch (err) {
        console.error('File not found: ' + err.path);
    }
    /**Delete file */
    try {
        fs.unlink('example.txt', function (err) {
            if (err) throw err;
            console.log('File deleted!');
        });
    } catch (err) {
        console.error('File not found: ' + err.path);
    }

})

app.listen(3000)