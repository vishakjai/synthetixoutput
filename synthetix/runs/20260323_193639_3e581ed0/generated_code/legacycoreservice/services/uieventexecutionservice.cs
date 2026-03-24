namespace LegacyCoreService.Services;

public sealed record UiEventExecutionResult(string EventId, string Message, string NavigationTarget, IReadOnlyList<string> Targets);

public sealed class UiEventExecutionService
{
    private readonly IHttpClientFactory HttpClientFactory;

    public UiEventExecutionService(IHttpClientFactory httpClientFactory)
    {
        HttpClientFactory = httpClientFactory;
    }

    public UiEventExecutionResult Execute(string screenId, string eventId, IDictionary<string, string> state)
    {
        if (!UiEventRegistry.Registry.TryGetValue(screenId, out var screenEvents) || !screenEvents.TryGetValue(eventId, out var definition))
        {
            return new UiEventExecutionResult(eventId, "Unable to resolve the requested screen action.", string.Empty, Array.Empty<string>());
        }

        state["lastExecutedScreen"] = screenId;
        state["lastExecutedEvent"] = eventId;
        state["lastExecutedAt"] = DateTimeOffset.UtcNow.ToString("O");
        foreach (var target in definition.Targets)
        {
            if (!target.Contains(" ", StringComparison.Ordinal))
            {
                continue;
            }
            state["lastApiTarget"] = target;
            _ = HttpClientFactory.CreateClient();
            // TODO: Build the request payload from Screen Contract bindings and invoke the scoped API target.
        }
        return new UiEventExecutionResult(eventId, definition.SuccessMessage, definition.NavigationTarget, definition.Targets);
    }
}
