const request = require('supertest');
const express = require('express');
const mongoose = require('mongoose');
const orderRoutes = require('../routes/orders');

const app = express();
app.use(express.json());
app.use(orderRoutes);

beforeAll(async () => {
  await mongoose.connect(process.env.MONGODB_URI, { useNewUrlParser: true, useUnifiedTopology: true });
});

afterAll(async () => {
  await mongoose.connection.close();
});

describe('Order API', () => {
  it('should create a new order', async () => {
    const response = await request(app)
      .post('/orders')
      .send({ customerId: '507f191e810c19729de860ea', status: 'NEW', total: 100 });
    expect(response.statusCode).toBe(201);
    expect(response.body).toHaveProperty('_id');
  });

  it('should update an order', async () => {
    const order = await request(app)
      .post('/orders')
      .send({ customerId: '507f191e810c19729de860ea', status: 'NEW', total: 100 });

    const response = await request(app)
      .put(`/orders/${order.body._id}`)
      .send({ status: 'PROCESSING' });
    expect(response.statusCode).toBe(200);
    expect(response.body.status).toBe('PROCESSING');
  });
});
