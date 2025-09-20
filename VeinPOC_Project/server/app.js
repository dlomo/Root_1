const express = require('express');
const path = require('path');
const app_sys = express();

app_sys.use(express.json());
app_sys.use(express.static(path.join(__dirname, '../public/frontLocal')));

// API route example
app_sys.get('/api/hello', (req,res) => res.json({ msg: "Hello API" }));

app_sys.listen(3000, ()=> console.log("1st Server running on http://localhost:3000"));


/* app_sys.listen(port, hostname, () => {
  console.log('2nd...');	
  console.log(`Server running at http://${hostname}:${port}/`);
  console.log('Starting the server...');
  console.log('Server started successfully!');
});
 */
