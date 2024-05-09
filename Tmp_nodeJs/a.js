const http = require('http');
const fs = require('fs');
const path = require('path');

const server = http.createServer((req, res) => {
    const filePath = path.join(__dirname, 'public', req.url);

    if (req.url === '/') {
        // read index.html file and send it to the client
            fs.readFile('./public/index.html', (err, data) => {
                if (err) {
                res.writeHead(500, { 'Content-Type': 'text/plain' });
                res.end('Internal Server Error');
                } else {
                res.writeHead(200, { 'Content-Type': 'text/html' });
                res.end(data);
                }
            });
    } else if (req.url === '/about') {
        // handle about request
      } else {
        res.writeHead(404, { 'Content-Type': 'text/plain' });
        res.end('Page not found');
    }

  let data = '';
  req.on('data', chunk => {
    data += chunk;
  });
  req.on('end', () => {
    res.statusCode = 200;
    res.statusMessage = 'OK';
    res.writeHead(200, {'Content-Type': 'text/plain'});
    res.end(data);
  });

//   switch (req.method) {
//     case 'GET':
//       // Handle GET request
//       break;
//     case 'POST':
//       // Handle POST request
//       break;
//     case 'PUT':
//       // Handle PUT request
//       break;
//     case 'DELETE':
//       // Handle DELETE request
//       break;
//     default:
//       res.statusCode = 405;
//       res.end();
//       break;
//   }
});

server.listen(3000, () => {
  console.log('Echo server listening on port 3000');
});