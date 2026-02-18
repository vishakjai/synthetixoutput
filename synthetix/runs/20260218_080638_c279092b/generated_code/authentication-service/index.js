const express = require('express');
const session = require('express-session');
const jwt = require('jsonwebtoken');

const app = express();
const PORT = process.env.PORT || 8080;

app.use(express.json());
app.use(session({
  secret: 'secret-key',
  resave: false,
  saveUninitialized: true
}));

// Health endpoints
app.get('/health', (req, res) => {
  res.status(200).json({ status: 'healthy' });
});

app.get('/ready', (req, res) => {
  res.status(200).json({ status: 'ready' });
});

// Authentication endpoint
app.post('/login', (req, res) => {
  const { username } = req.body;
  if (!username) {
    return res.status(400).json({ message: 'Username is required' });
  }
  req.session.isAuthenticated = true;
  req.session.username = username;
  const token = jwt.sign({ username }, 'jwt-secret-key', { expiresIn: '1h' });
  res.json({ message: `Welcome, ${username}.`, token });
});

app.post('/logout', (req, res) => {
  req.session.destroy(err => {
    if (err) {
      return res.status(500).json({ message: 'Logout failed' });
    }
    res.json({ message: 'You have been logged out.' });
  });
});

app.listen(PORT, () => {
  console.log(`Authentication Service running on port ${PORT}`);
});
