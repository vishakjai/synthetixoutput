namespace ReportingService.Services;

public sealed record UiEventDefinition(string Name, IReadOnlyList<string> Targets, string NavigationTarget, string SuccessMessage);

public sealed class UiEventRegistry
{
    public static readonly Dictionary<string, Dictionary<string, UiEventDefinition>> Registry =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["frmWithinDate"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmdaily"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmmonthlyreport"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmstatement"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["Form1"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_save"] = new UiEventDefinition("Save Screen Data", new[] { "POST /reporting/expireitemswithindate", "list" }, "/", "Changes saved successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmExpireItemsWithinDate"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_save"] = new UiEventDefinition("Save Screen Data", new[] { "POST /reporting/expireitemswithindate", "list" }, "/", "Changes saved successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmMonthly"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_save"] = new UiEventDefinition("Save Screen Data", new[] { "POST /reporting/expireitemswithindate", "list" }, "/", "Changes saved successfully."),
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmreport"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_cancel"] = new UiEventDefinition("Cancel Screen", new[] { "previous" }, "/", "Cancel Screen completed successfully."),
            },
            ["frmtransaction"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_save"] = new UiEventDefinition("Save Screen Data", new[] { "POST /reporting/expireitemswithindate", "list" }, "/", "Changes saved successfully."),
            },
            ["frmwith"] = new(StringComparer.OrdinalIgnoreCase)
            {
            ["evt_save"] = new UiEventDefinition("Save Screen Data", new[] { "POST /reporting/expireitemswithindate", "list" }, "/", "Changes saved successfully."),
            },
        };
}
