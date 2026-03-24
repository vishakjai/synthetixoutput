namespace LegacyCoreService.Services;

public sealed record UiValidationDescriptor(string Id, string Kind, string Message, int? MaxLength);
public sealed record UiFieldDescriptor(
    string FieldId,
    string Label,
    string RenderKind,
    string InputType,
    bool Required,
    bool Submittable,
    int TabOrder,
    string Placeholder,
    string HelpText,
    string InputMode,
    int? MaxLength,
    string NavigationTarget,
    string BindingName,
    string RadioGroupName,
    string RadioGroupLabel,
    string OptionValue,
    IReadOnlyList<string> Options,
    IReadOnlyList<UiValidationDescriptor> Rules);
public sealed record UiSectionDescriptor(string Id, string Label, int Order, IReadOnlyList<string> FieldIds);
public sealed record UiActionDescriptor(string EventId, string Label, string Kind, string Placement, string TargetHint);
public sealed record UiScreenDescriptor(
    string ScreenId,
    string ScreenName,
    string Description,
    string DefaultRoute,
    string FidelityLevel,
    double FidelityScore,
    IReadOnlyList<UiSectionDescriptor> Sections,
    IReadOnlyDictionary<string, UiFieldDescriptor> Fields,
    IReadOnlyList<UiActionDescriptor> Actions);

public sealed class UiScreenRegistry
{
    public static readonly Dictionary<string, UiScreenDescriptor> Screens =
        new(StringComparer.OrdinalIgnoreCase)
        {
        ["frmaddinterest"] = new UiScreenDescriptor(
            "frmaddinterest",
            "Interest calculation and posting workflow",
            "Interest calculation and posting workflow.",
            "/ui/frmaddinterest",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Add Interest", 1, new[] { "txtdate", "cbomonth", "cboyear", "fra", "lblamount", "lblbal", "lblbalance", "lblcurrentbalance", "lblcustomerid", "lblfirstname", "lblid", "lblinterest", "lbllastname", "lbltransaction", "lbltransactionid", "lbltype", "txtaccountno", "txtcurrentdate" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["txtdate"] = new UiFieldDescriptor("txtdate", "Date", "input", "date", false, true, 1, "Optional date", "", "text", null, "", "txtdate", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["cbomonth"] = new UiFieldDescriptor("cbomonth", "Mo Nth", "select", "text", false, true, 2, "Optional mo nth", "", "text", null, "", "cbomonth", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["cboyear"] = new UiFieldDescriptor("cboyear", "Y Ear", "select", "text", false, true, 3, "Optional y ear", "", "text", null, "", "cboyear", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["fra"] = new UiFieldDescriptor("fra", "Screen", "fieldset", "text", false, false, 10, "", "", "text", null, "", "fra", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblamount"] = new UiFieldDescriptor("lblamount", "Amo Unt", "display", "text", false, false, 13, "", "", "numeric", null, "", "lblamount", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbal"] = new UiFieldDescriptor("lblbal", "Bal", "display", "text", false, false, 14, "", "", "text", null, "", "lblbal", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbalance"] = new UiFieldDescriptor("lblbalance", "Balance", "display", "text", false, false, 15, "", "", "numeric", null, "", "lblbalance", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcurrentbalance"] = new UiFieldDescriptor("lblcurrentbalance", "Cur Ren T Balance", "display", "text", false, false, 16, "", "", "numeric", null, "", "lblcurrentbalance", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustomerid"] = new UiFieldDescriptor("lblcustomerid", "Customer ID", "display", "text", false, false, 17, "", "", "numeric", null, "", "lblcustomerid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblfirstname"] = new UiFieldDescriptor("lblfirstname", "First Name", "display", "text", false, false, 19, "", "", "text", null, "", "lblfirstname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblid"] = new UiFieldDescriptor("lblid", "ID", "display", "text", false, false, 20, "", "", "numeric", null, "", "lblid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblinterest"] = new UiFieldDescriptor("lblinterest", "Interest", "display", "text", false, false, 21, "", "", "text", null, "", "lblinterest", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbllastname"] = new UiFieldDescriptor("lbllastname", "Last Name", "display", "text", false, false, 23, "", "", "text", null, "", "lbllastname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltransaction"] = new UiFieldDescriptor("lbltransaction", "Transaction", "display", "text", false, false, 24, "", "", "text", null, "", "lbltransaction", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltransactionid"] = new UiFieldDescriptor("lbltransactionid", "Transaction ID", "display", "text", false, false, 25, "", "", "numeric", null, "", "lbltransactionid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltype"] = new UiFieldDescriptor("lbltype", "Type", "display", "text", false, false, 26, "", "", "text", null, "", "lbltype", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["txtaccountno"] = new UiFieldDescriptor("txtaccountno", "Account No", "input", "number", true, true, 27, "Enter account no", "Enter digits only.", "numeric", null, "", "txtaccountno", "", "", "", new[] { "Yes", "No" }, new[] { new UiValidationDescriptor("VR-txtaccountno-REQUIRED", "required", "Account No is required.", null), new UiValidationDescriptor("VR-txtaccountno-NUMERIC", "numeric", "Account No must be numeric.", null) }),
                ["txtcurrentdate"] = new UiFieldDescriptor("txtcurrentdate", "Cur Ren T Date", "input", "date", false, true, 28, "Optional cur ren t date", "", "text", null, "", "txtcurrentdate", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmdep"] = new UiScreenDescriptor(
            "frmdep",
            "Business workflow executed through event-driven UI controls",
            "Business workflow executed through event-driven UI controls.",
            "/ui/frmdep",
            "medium",
            0.81,
            new[]
            {

            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {

            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /legacycore/addinterest, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frminterest"] = new UiScreenDescriptor(
            "frminterest",
            "Interest calculation and posting workflow",
            "Interest calculation and posting workflow.",
            "/ui/frminterest",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Interest", 1, new[] { "listview1" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["listview1"] = new UiFieldDescriptor("listview1", "List View1", "input", "text", false, true, 1, "Optional list view1", "", "text", null, "", "listview1", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /legacycore/addinterest, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        };
}
