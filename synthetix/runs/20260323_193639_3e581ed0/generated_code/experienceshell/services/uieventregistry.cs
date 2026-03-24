namespace ExperienceShell.Services;

public sealed record UiEventDefinition(string Name, IReadOnlyList<string> Targets, string NavigationTarget, string SuccessMessage);

public sealed class UiEventRegistry
{
    public static readonly Dictionary<string, Dictionary<string, UiEventDefinition>> Registry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["frmSplash"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_search"] = new UiEventDefinition("Load Screen Data", new[] { "GET /experienceshell/splash" }, "", "Load Screen Data completed successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["menu"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_search"] = new UiEventDefinition("Load Screen Data", new[] { "GET /experienceshell/splash" }, "", "Load Screen Data completed successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["Mdi"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_search"] = new UiEventDefinition("Load Screen Data", new[] { "GET /experienceshell/splash" }, "", "Load Screen Data completed successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
        };
}
