const request = require('supertest');
const express = require('express');
const session = require('express-session');
const jwt = require('jsonwebtoken');

const app = express();
app.use(express.json());
app.use(session({
  secret: 'secret-key',
  resave: false,
  saveUninitialized: true
}));

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

// Tests

describe('Authentication Service', () => {
  it('should login a user and return a token', async () => {
    const response = await request(app)
      .post('/login')
      .send({ username: 'testuser' });
    expect(response.statusCode).toBe(200);
    expect(response.body).toHaveProperty('token');
  });

  it('should require a username to login', async () => {
    const response = await request(app)
      .post('/login')
      .send({});
    expect(response.statusCode).toBe(400);
    expect(response.body.message).toBe('Username is required');
  });

  it('should logout a user', async () => {
    const response = await request(app)
      .post('/logout');
    expect(response.statusCode).toBe(200);
    expect(response.body.message).toBe('You have been logged out.');
  });
});
