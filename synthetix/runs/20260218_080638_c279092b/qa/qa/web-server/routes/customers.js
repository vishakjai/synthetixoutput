const express = require('express');
const router = express.Router();

// Mock data for customers
const customers = {
  1: 'John Doe',
  2: 'Jane Smith'
};

router.get('/', (req, res) => {
  if (!req.session.isAuthenticated) {
    return res.status(401).send('You must log in to view customers and orders.');
  }
  res.json(customers);
});

router.post('/login', (req, res) => {
  const { userName } = req.body;
  if (!userName) {
    req.session.isAuthenticated = false;
    return res.status(400).send('User name is required.');
  }
  req.session.isAuthenticated = true;
  req.session.userName = userName;
  res.send(`Welcome, ${userName}.`);
});

router.post('/logout', (req, res) => {
  req.session.destroy(err => {
    if (err) {
      return res.status(500).send('Error logging out.');
    }
    res.send('You have been logged out.');
  });
});

module.exports = router;
