const request = require('supertest');
const express = require('express');
const session = require('express-session');
const customerRouter = require('../routes/customers');
const orderRouter = require('../routes/orders');

const app = express();
app.use(express.json());
app.use(session({
  secret: 'secret-key',
  resave: false,
  saveUninitialized: true
}));
app.use('/customers', customerRouter);
app.use('/orders', orderRouter);

app.get('/health', (req, res) => {
  res.status(200).json({ status: 'healthy' });
});

app.get('/ready', (req, res) => {
  res.status(200).json({ status: 'ready' });
});

// Tests

describe('GET /health', () => {
  it('should return healthy status', async () => {
    const res = await request(app).get('/health');
    expect(res.statusCode).toEqual(200);
    expect(res.body).toHaveProperty('status', 'healthy');
  });
});

describe('GET /ready', () => {
  it('should return ready status', async () => {
    const res = await request(app).get('/ready');
    expect(res.statusCode).toEqual(200);
    expect(res.body).toHaveProperty('status', 'ready');
  });
});
