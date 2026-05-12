namespace Reporting and Analytics.Services;

public sealed record UiEventDefinition(string Name, IReadOnlyList<string> Targets, string NavigationTarget, string SuccessMessage);

public sealed class UiEventRegistry
{
    public static readonly Dictionary<string, Dictionary<string, UiEventDefinition>> Registry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Reporting_and_Analytics"] = new(StringComparer.OrdinalIgnoreCase)
            {

            },
        };
}
