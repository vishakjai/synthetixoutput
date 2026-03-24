namespace TransactionService.Services;

public sealed record UiEventDefinition(string Name, IReadOnlyList<string> Targets, string NavigationTarget, string SuccessMessage);

public sealed class UiEventRegistry
{
    public static readonly Dictionary<string, Dictionary<string, UiEventDefinition>> Registry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["frmdeposit"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_save"] = new UiEventDefinition("Save Screen Data", new[] { "POST /transactions/deposit", "list" }, "/", "Changes saved successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmwithdraw"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmcheckbalance"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_save"] = new UiEventDefinition("Save Screen Data", new[] { "POST /transactions/deposit", "list" }, "/", "Changes saved successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
        };
}
