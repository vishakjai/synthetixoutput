const request = require('supertest');
const express = require('express');
const customerRoutes = require('../routes/customers');

const app = express();
app.use(express.json());
app.use('/customers', customerRoutes);

// Mock Mongoose model
jest.mock('../models/customer', () => {
  return {
    find: jest.fn().mockResolvedValue([]),
    save: jest.fn().mockResolvedValue({ _id: '123', name: 'Test Customer' })
  };
});

// Test GET /customers
it('should return an empty array of customers', async () => {
  const response = await request(app).get('/customers');
  expect(response.statusCode).toBe(200);
  expect(response.body).toEqual([]);
});

// Test POST /customers
it('should create a new customer', async () => {
  const response = await request(app)
    .post('/customers')
    .send({ name: 'Test Customer' });
  expect(response.statusCode).toBe(201);
  expect(response.body).toHaveProperty('_id');
  expect(response.body.name).toBe('Test Customer');
});
