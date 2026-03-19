namespace AuthenticationService.Services;

public sealed class ScreenValidationService
{
    public void ValidateScreen(string screenId, IDictionary<string, string> state, IDictionary<string, string> errors)
    {
        foreach (var entry in state)
        {
            if (string.IsNullOrWhiteSpace(entry.Value))
            {
                continue;
            }
            if (entry.Value.Length > 256)
            {
                errors[entry.Key] = "Value exceeds allowed bounds.";
            }
        }
    }
}
