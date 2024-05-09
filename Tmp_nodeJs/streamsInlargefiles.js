const fs = require('fs');

try {
  // Define the file path and create a Readable stream
  const filePath = './tmp/large/ViolentHead-GioPika.mp3';
  const readableStream = fs.createReadStream(filePath, { highWaterMark: 16 * 1024 });

  // Define the destination file path and create a Writable stream
  const destinationPath = './tmp/destination/file.mp3';
  const writableStream = fs.createWriteStream(destinationPath);

  // Use the pipe method to read from the Readable stream and write to the Writable stream
  readableStream.pipe(writableStream);

  // Handle errors
  readableStream.on('error', (err) => {
    console.error(`Error reading from file: ${err}`);
  });

  writableStream.on('error', (err) => {
    console.error(`Error writing to file: ${err}`);
  });

  writableStream.on('finish', () => {
    console.log('File successfully written!');
  });
} catch (err) {
  console.error(`Error streaming file: ${err}`);
}