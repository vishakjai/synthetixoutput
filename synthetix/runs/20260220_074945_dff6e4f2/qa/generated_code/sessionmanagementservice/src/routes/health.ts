import { Router } from 'express';

export const healthRouter = Router();

healthRouter.get('/', (req, res) => {
  res.status(200).json({ status: 'healthy' });
});

healthRouter.get('/ready', (req, res) => {
  res.status(200).json({ status: 'ready' });
});
