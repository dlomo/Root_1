const { createServer } = require('node:http');
const Toolz = require('./JSTool.js');
// Toolz is a custom module that contains utility functions/packages
const Spliturl = require('url'); // Spliturl is a built-in module that helps parse URLs
// Spliturl is a built-in module that helps parse URLs
var fiLsystm = require('fs'); // File system module to work with the file system
//functionality: Read files / Create files / Update files / Delete files / Rename files

const hostname = '127.0.0.1';
const port = 3000;

const Oneserver = createServer((req, res) => {
  res.statusCode = 200;
  //res.setHeader('Content-Type', 'text/html'); // This is the default content type for HTML
  res.setHeader('Content-Type', 'text/plain'); // This is the default content type for plain text


  console.log('Hello Worldz');
  //res.end(txt);
  //res.end()


  fiLsystm.readFile('index_back.html', function(err, data) {
    res.writeHead(200, {'Content-Type': 'text/html'});
    res.write(data);
    return res.end();
  });

});

Oneserver.listen(port, hostname, () => {
  console.log(`Server running at http://${hostname}:${port}/`);
  console.log('Starting the server...');
  console.log('Server started successfully!');
});

