namespace ReportingService.Services;

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
        ["frmWithinDate"] = new UiScreenDescriptor(
            "frmWithinDate",
            "Business workflow executed through event-driven UI controls",
            "Business workflow executed through event-driven UI controls.",
            "/ui/frmwithindate",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Within Date", 1, new[] { "dtfrom", "dtto", "frawithindate" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["dtfrom"] = new UiFieldDescriptor("dtfrom", "Dtfrom", "input", "text", false, true, 1, "Optional dtfrom", "", "text", null, "", "dtfrom", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["dtto"] = new UiFieldDescriptor("dtto", "Dtto", "input", "text", false, true, 2, "Optional dtto", "", "text", null, "", "dtto", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["frawithindate"] = new UiFieldDescriptor("frawithindate", "Wit Hin Date", "fieldset", "text", false, false, 7, "", "", "text", null, "", "frawithindate", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmdaily"] = new UiScreenDescriptor(
            "frmdaily",
            "Business workflow executed through event-driven UI controls",
            "Business workflow executed through event-driven UI controls.",
            "/ui/frmdaily",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Da Ily", 1, new[] { "txtdaily", "fradaily" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["txtdaily"] = new UiFieldDescriptor("txtdaily", "Da Ily", "input", "text", false, true, 1, "Optional da ily", "", "text", null, "", "txtdaily", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["fradaily"] = new UiFieldDescriptor("fradaily", "Da Ily", "fieldset", "text", false, false, 4, "", "", "text", null, "", "fradaily", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmmonthlyreport"] = new UiScreenDescriptor(
            "frmmonthlyreport",
            "Operational reporting and statement generation workflow",
            "Operational reporting and statement generation workflow.",
            "/ui/frmmonthlyreport",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Monthly Rep Ort", 1, new[] { "dtpfrom", "dtpto", "cmbcustomerid", "lblfrom", "lblto" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["dtpfrom"] = new UiFieldDescriptor("dtpfrom", "From", "input", "text", false, true, 1, "Optional from", "", "text", null, "", "dtpfrom", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["dtpto"] = new UiFieldDescriptor("dtpto", "To", "input", "text", false, true, 2, "Optional to", "", "text", null, "", "dtpto", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["cmbcustomerid"] = new UiFieldDescriptor("cmbcustomerid", "Customer ID", "select", "text", false, true, 3, "Optional customer id", "", "text", null, "", "cmbcustomerid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblfrom"] = new UiFieldDescriptor("lblfrom", "From", "display", "text", false, false, 8, "", "", "text", null, "", "lblfrom", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblto"] = new UiFieldDescriptor("lblto", "To", "display", "text", false, false, 9, "", "", "text", null, "", "lblto", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmstatement"] = new UiScreenDescriptor(
            "frmstatement",
            "Operational reporting and statement generation workflow",
            "Operational reporting and statement generation workflow.",
            "/ui/frmstatement",
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
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["Form1"] = new UiScreenDescriptor(
            "Form1",
            "Business workflow executed through event-driven UI controls",
            "Business workflow executed through event-driven UI controls.",
            "/ui/form1",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Form1", 1, new[] { "dtpicker1", "dtpicker2", "shape5" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["dtpicker1"] = new UiFieldDescriptor("dtpicker1", "Ick Er1", "input", "text", false, true, 1, "Optional ick er1", "", "text", null, "", "dtpicker1", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["dtpicker2"] = new UiFieldDescriptor("dtpicker2", "Ick Er2", "input", "text", false, true, 2, "Optional ick er2", "", "text", null, "", "dtpicker2", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["shape5"] = new UiFieldDescriptor("shape5", "Shape5", "input", "text", false, true, 8, "Optional shape5", "", "text", null, "", "shape5", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /reporting/expireitemswithindate, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmExpireItemsWithinDate"] = new UiScreenDescriptor(
            "frmExpireItemsWithinDate",
            "Business workflow executed through event-driven UI controls",
            "Business workflow executed through event-driven UI controls.",
            "/ui/frmexpireitemswithindate",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Expire Items Within Date", 1, new[] { "dtfrom", "dtto" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["dtfrom"] = new UiFieldDescriptor("dtfrom", "Dtfrom", "input", "text", false, true, 1, "Optional dtfrom", "", "text", null, "", "dtfrom", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["dtto"] = new UiFieldDescriptor("dtto", "Dtto", "input", "text", false, true, 2, "Optional dtto", "", "text", null, "", "dtto", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /reporting/expireitemswithindate, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmMonthly"] = new UiScreenDescriptor(
            "frmMonthly",
            "Operational reporting and statement generation workflow",
            "Operational reporting and statement generation workflow.",
            "/ui/frmmonthly",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Monthly", 1, new[] { "cmbreport" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["cmbreport"] = new UiFieldDescriptor("cmbreport", "Report", "select", "text", false, true, 1, "Optional report", "", "text", null, "", "cmbreport", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /reporting/expireitemswithindate, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmreport"] = new UiScreenDescriptor(
            "frmreport",
            "Operational reporting and statement generation workflow",
            "Operational reporting and statement generation workflow.",
            "/ui/frmreport",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Rep Ort", 1, new[] { "dtpfromdate", "dtptodate", "frareport", "frasearch", "lblcustomerid", "txtbalance", "txtfirstname", "txtlastname", "txtaccount", "txtaccountno", "txtcustomerid", "txttypeofaccount" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["dtpfromdate"] = new UiFieldDescriptor("dtpfromdate", "From Date", "input", "date", false, true, 1, "Optional from date", "", "text", null, "", "dtpfromdate", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["dtptodate"] = new UiFieldDescriptor("dtptodate", "To Date", "input", "date", false, true, 2, "Optional to date", "", "text", null, "", "dtptodate", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["frareport"] = new UiFieldDescriptor("frareport", "Rep Ort", "fieldset", "text", false, false, 10, "", "", "text", null, "", "frareport", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["frasearch"] = new UiFieldDescriptor("frasearch", "Search", "fieldset", "text", false, false, 11, "", "", "text", null, "", "frasearch", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustomerid"] = new UiFieldDescriptor("lblcustomerid", "Customer ID", "display", "text", false, false, 18, "", "", "numeric", null, "", "lblcustomerid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtbalance"] = new UiFieldDescriptor("txtbalance", "Balance", "input", "number", false, true, 19, "Optional balance", "Enter digits only.", "numeric", null, "", "txtbalance", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtbalance-NUMERIC", "numeric", "Balance must be numeric.", null) }),
                ["txtfirstname"] = new UiFieldDescriptor("txtfirstname", "First Name", "input", "text", true, true, 20, "Enter first name", "", "text", null, "", "txtfirstname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtfirstname-REQUIRED", "required", "First Name is required.", null) }),
                ["txtlastname"] = new UiFieldDescriptor("txtlastname", "Last Name", "input", "text", true, true, 21, "Enter last name", "", "text", null, "", "txtlastname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtlastname-REQUIRED", "required", "Last Name is required.", null) }),
                ["txtaccount"] = new UiFieldDescriptor("txtaccount", "Account", "input", "number", true, true, 22, "Enter account", "Enter digits only.", "numeric", null, "", "txtaccount", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, new[] { new UiValidationDescriptor("VR-txtaccount-REQUIRED", "required", "Account is required.", null), new UiValidationDescriptor("VR-txtaccount-NUMERIC", "numeric", "Account must be numeric.", null) }),
                ["txtaccountno"] = new UiFieldDescriptor("txtaccountno", "Account No", "input", "number", true, true, 23, "Enter account no", "Enter digits only.", "numeric", null, "", "txtaccountno", "", "", "", new[] { "Yes", "No" }, new[] { new UiValidationDescriptor("VR-txtaccountno-REQUIRED", "required", "Account No is required.", null), new UiValidationDescriptor("VR-txtaccountno-NUMERIC", "numeric", "Account No must be numeric.", null) }),
                ["txtcustomerid"] = new UiFieldDescriptor("txtcustomerid", "Customer ID", "input", "number", true, true, 24, "Enter customer id", "Enter digits only.", "numeric", null, "", "txtcustomerid", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtcustomerid-REQUIRED", "required", "Customer ID is required.", null), new UiValidationDescriptor("VR-txtcustomerid-NUMERIC", "numeric", "Customer ID must be numeric.", null) }),
                ["txttypeofaccount"] = new UiFieldDescriptor("txttypeofaccount", "Type Of Account", "input", "number", true, true, 25, "Enter type of account", "Enter digits only.", "numeric", null, "", "txttypeofaccount", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, new[] { new UiValidationDescriptor("VR-txttypeofaccount-REQUIRED", "required", "Type Of Account is required.", null), new UiValidationDescriptor("VR-txttypeofaccount-NUMERIC", "numeric", "Type Of Account must be numeric.", null) }),
            },
            new[]
            {
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmtransaction"] = new UiScreenDescriptor(
            "frmtransaction",
            "Transaction ledger management and adjustment workflow",
            "Transaction ledger management and adjustment workflow.",
            "/ui/frmtransaction",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Transaction", 1, new[] { "lvwtransactions", "cboaccno", "fraaccountno", "option1", "option2" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["lvwtransactions"] = new UiFieldDescriptor("lvwtransactions", "Lvw Transactions", "input", "text", false, true, 1, "Optional lvw transactions", "", "text", null, "", "lvwtransactions", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["cboaccno"] = new UiFieldDescriptor("cboaccno", "Acc No", "select", "text", false, true, 2, "Optional acc no", "", "text", null, "", "cboaccno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["fraaccountno"] = new UiFieldDescriptor("fraaccountno", "Account No", "fieldset", "text", false, false, 7, "", "", "text", null, "", "fraaccountno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["option1"] = new UiFieldDescriptor("option1", "I On1", "radio", "text", false, true, 10, "Optional i on1", "", "text", null, "", "option1", "option1", "I On1", "I On1", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["option2"] = new UiFieldDescriptor("option2", "I On2", "radio", "text", false, true, 11, "Optional i on2", "", "text", null, "", "option2", "option2", "I On2", "I On2", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /reporting/expireitemswithindate, list"),
            }
        ),
        ["frmwith"] = new UiScreenDescriptor(
            "frmwith",
            "Transaction ledger management and adjustment workflow",
            "Transaction ledger management and adjustment workflow.",
            "/ui/frmwith",
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
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /reporting/expireitemswithindate, list"),
            }
        ),
        };
}
