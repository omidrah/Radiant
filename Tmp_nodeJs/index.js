

const fs = require('fs');


console.log('hi ...start')


console.log(Buffer.from('CMD').toString('hex'))
console.log(Buffer.from('CMD').toString('utf-8'))
console.log( Array.from(Buffer.from('CMD')))


const ssid = 'my-ap'; // Replace with your desired string
const ssidByteArray = Array.from(Buffer.from(ssid));
// Assuming you already have the ssidByteArray
fs.writeFileSync('a', Buffer.from(ssidByteArray), 'binary');