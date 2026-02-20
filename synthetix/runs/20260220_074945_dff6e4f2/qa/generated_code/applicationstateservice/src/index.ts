import express from 'express';
import { ApplicationState } from './state';

const app = express();
const port = process.env.PORT || 8080;

const state = new ApplicationState();

app.get('/health', (req, res) => {
  res.status(200).json({ status: 'healthy' });
});

app.get('/ready', (req, res) => {
  res.status(200).json({ status: 'ready' });
});

app.get('/state', (req, res) => {
  res.status(200).json({
    appName: state.getAppName(),
    totalSessions: state.getTotalSessions()
  });
});

app.listen(port, () => {
  console.log(`ApplicationStateService is running on port ${port}`);
});
