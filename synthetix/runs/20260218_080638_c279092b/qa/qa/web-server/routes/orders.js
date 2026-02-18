const express = require('express');
const router = express.Router();

// Mock data for orders
let orders = {};
let nextOrderId = 1;

router.post('/create', (req, res) => {
  const { customerId, status, total } = req.body;
  if (!customerId || !status || total <= 0) {
    return res.status(400).send('All fields are required, total must be > 0.');
  }
  const orderId = nextOrderId++;
  orders[orderId] = { customerId, status, total, date: new Date() };
  res.send(`Order ${orderId} created.`);
});

router.post('/update', (req, res) => {
  const { orderId, newStatus } = req.body;
  if (!orderId || !newStatus || !orders[orderId]) {
    return res.status(400).send('Order ID and new status are required.');
  }
  orders[orderId].status = newStatus;
  res.send(`Order ${orderId} updated.`);
});

module.exports = router;
