import { Router } from 'express';

export const sessionRouter = Router();

sessionRouter.post('/login', (req, res) => {
  const { userName } = req.body;
  if (!userName) {
    req.session.isAuthenticated = false;
    req.session.userName = '';
    req.session.flashMessage = 'User name is required.';
    return res.status(400).json({ message: req.session.flashMessage });
  }
  req.session.isAuthenticated = true;
  req.session.userName = userName;
  req.session.flashMessage = `Welcome, ${userName}.`;
  res.status(200).json({ message: req.session.flashMessage });
});

sessionRouter.post('/logout', (req, res) => {
  req.session.destroy((err) => {
    if (err) {
      return res.status(500).json({ message: 'Logout failed.' });
    }
    res.status(200).json({ message: 'You have been logged out.' });
  });
});
