const express = require('express');
const router = express.Router();
const Order = require('../models/order');

// Create Order
router.post('/orders', async (req, res) => {
  try {
    const { customerId, status, total } = req.body;
    if (!customerId || !status || total <= 0) {
      return res.status(400).json({ error: 'Invalid order data' });
    }
    const order = new Order({ customerId, status, total });
    await order.save();
    res.status(201).json(order);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

// Update Order
router.put('/orders/:id', async (req, res) => {
  try {
    const { id } = req.params;
    const { status } = req.body;
    const order = await Order.findByIdAndUpdate(id, { status }, { new: true });
    if (!order) {
      return res.status(404).json({ error: 'Order not found' });
    }
    res.json(order);
  } catch (err) {
    res.status(500).json({ error: err.message });
  }
});

module.exports = router;
