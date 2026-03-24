namespace AuthenticationService.Services;

public sealed record UiEventDefinition(string Name, IReadOnlyList<string> Targets, string NavigationTarget, string SuccessMessage);

public sealed class UiEventRegistry
{
    public static readonly Dictionary<string, Dictionary<string, UiEventDefinition>> Registry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["frmLogin1"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_save"] = new UiEventDefinition("Save Screen Data", new[] { "POST /auth/login", "list" }, "/", "Changes saved successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
        };
}
