

const sql = require('mssql');

const config = {
  user: 'yourUser',
  password: 'yourPassword',
  server: 'localhost',
  database: 'yourDB',
  options: { encrypt: true, trustServerCertificate: true }
};

module.exports = async function queryDB(query) {
  try {
    const pool = await sql.connect(config);
    return await pool.request().query(query);
  } catch (err) {
    console.error("DB Error:", err);
  }
};