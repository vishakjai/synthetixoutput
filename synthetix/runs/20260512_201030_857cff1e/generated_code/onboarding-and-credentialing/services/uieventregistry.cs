namespace Onboarding and Credentialing.Services;

public sealed record UiEventDefinition(string Name, IReadOnlyList<string> Targets, string NavigationTarget, string SuccessMessage);

public sealed class UiEventRegistry
{
    public static readonly Dictionary<string, Dictionary<string, UiEventDefinition>> Registry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["Onboarding_and_Credentialing"] = new(StringComparer.OrdinalIgnoreCase)
            {

            },
        };
}
