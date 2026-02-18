const express = require('express');
const mongoose = require('mongoose');
const customerRoutes = require('./routes/customers');

const app = express();
const PORT = process.env.PORT || 8080;

// Middleware
app.use(express.json());

// Routes
app.use('/customers', customerRoutes);

// Health Check Endpoints
app.get('/health', (req, res) => {
  res.status(200).json({ status: 'healthy' });
});

app.get('/ready', (req, res) => {
  res.status(200).json({ status: 'ready' });
});

// Connect to MongoDB
mongoose.connect('mongodb://localhost:27017/customers', {
  useNewUrlParser: true,
  useUnifiedTopology: true
}).then(() => {
  console.log('Connected to MongoDB');
  app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
  });
}).catch(err => {
  console.error('Failed to connect to MongoDB', err);
});
