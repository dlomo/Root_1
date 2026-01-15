const express = require('express');
const path = require('path');
const axios = require('axios');
const https = require('https'); // add this

const app_sys = express();

app_sys.use(express.json());
app_sys.use(express.static(path.join(__dirname, '../public/frontLocal')));

// API route that calls your .NET API
app_sys.get('/api/hello', async (req, res) => {
  try {
    const response = await axios.get('https://localhost:7041/api/hello', {
      httpsAgent: new https.Agent({ rejectUnauthorized: false }) // ignore self-signed dev cert in dev
    });
    res.json({ dotnetResponse: response.data });
  } catch (err) {
    console.error("Error calling .NET API:", err.message);
    res.status(500).json({ error: "Failed to reach SurgeryApp API" });
  }
});

app_sys.listen(3000, () => console.log("Node.js server running on http://localhost:3000"));


/* app_sys.listen(port, hostname, () => {
  console.log('2nd...');	
  console.log(`Server running at http://${hostname}:${port}/`);
  console.log('Starting the server...');
  console.log('Server started successfully!');
});
 */
