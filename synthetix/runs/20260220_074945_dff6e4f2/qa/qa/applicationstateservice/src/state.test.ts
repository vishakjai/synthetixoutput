import { ApplicationState } from './state';

describe('ApplicationState', () => {
  let state: ApplicationState;

  beforeEach(() => {
    state = new ApplicationState();
  });

  test('should initialize with default values', () => {
    expect(state.getAppName()).toBe('Legacy Customer Orders');
    expect(state.getTotalSessions()).toBe(0);
  });

  test('should increment total sessions', () => {
    state.incrementSessions();
    expect(state.getTotalSessions()).toBe(1);
  });

  test('should decrement total sessions', () => {
    state.incrementSessions();
    state.decrementSessions();
    expect(state.getTotalSessions()).toBe(0);
  });
});
