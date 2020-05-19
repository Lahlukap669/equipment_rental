const express = require('express');
const serveStatic = require("serve-static")
const path = require('path');
app = express();
app.use(serveStatic(path.join(__dirname, 'dist')));

app.get(/.*/, (req, res) => res.sendFile(path.resolve(__dirname, 'index.html')))

const port = process.env.PORT || 4000;
app.listen(port);