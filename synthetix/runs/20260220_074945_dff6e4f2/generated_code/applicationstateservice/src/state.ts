export class ApplicationState {
  private appName: string;
  private totalSessions: number;

  constructor() {
    this.appName = 'Legacy Customer Orders';
    this.totalSessions = 0;
  }

  public getAppName(): string {
    return this.appName;
  }

  public getTotalSessions(): number {
    return this.totalSessions;
  }

  public incrementSessions(): void {
    this.totalSessions += 1;
  }

  public decrementSessions(): void {
    this.totalSessions -= 1;
  }
}
