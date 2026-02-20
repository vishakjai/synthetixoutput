import express from 'express';
import session from 'express-session';
import { healthRouter } from './routes/health';
import { sessionRouter } from './routes/session';

const app = express();
const PORT = process.env.PORT || 8080;

app.use(express.json());
app.use(session({
  secret: 'your-secret-key',
  resave: false,
  saveUninitialized: true,
}));

app.use('/health', healthRouter);
app.use('/session', sessionRouter);

app.listen(PORT, () => {
  console.log(`SessionManagementService is running on port ${PORT}`);
});
