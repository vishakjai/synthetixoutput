namespace Authentication and Identity.Services;

public sealed record UiEventDefinition(string Name, IReadOnlyList<string> Targets, string NavigationTarget, string SuccessMessage);

public sealed class UiEventRegistry
{
    public static readonly Dictionary<string, Dictionary<string, UiEventDefinition>> Registry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Authentication_and_Identity"] = new(StringComparer.OrdinalIgnoreCase)
            {

            },
        };
}
