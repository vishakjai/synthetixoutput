namespace AuthenticationService.Services;

public sealed class UiEventExecutionService
{
    public void Execute(string screenId, string eventId, IDictionary<string, string> state)
    {
        state["lastExecutedScreen"] = screenId;
        state["lastExecutedEvent"] = eventId;
    }
}
