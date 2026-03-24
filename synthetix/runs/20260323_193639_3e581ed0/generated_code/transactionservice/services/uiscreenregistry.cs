namespace TransactionService.Services;

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
        ["frmdeposit"] = new UiScreenDescriptor(
            "frmdeposit",
            "Deposit capture and balance posting workflow",
            "Deposit capture and balance posting workflow.",
            "/ui/frmdeposit",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Dep O Sit", 1, new[] { "txtdateoftransaction", "fracheque", "fraext", "framode", "lblaccount", "lblbalance", "lblbankname", "lblchequeissued", "lblcustomer", "lblcustomerid", "lbldate", "lblfirstname", "lbllastname", "lbltypeofaccount", "optcash", "optcheque", "optno", "optyes", "txtbankname", "txtchequeno", "txtsearchaccountno" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["txtdateoftransaction"] = new UiFieldDescriptor("txtdateoftransaction", "Date Of Transaction", "input", "date", false, true, 1, "Optional date of transaction", "", "text", null, "", "txtdateoftransaction", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["fracheque"] = new UiFieldDescriptor("fracheque", "Cheque", "fieldset", "text", false, false, 6, "", "", "text", null, "", "fracheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["fraext"] = new UiFieldDescriptor("fraext", "Ext", "fieldset", "text", false, false, 7, "", "", "text", null, "", "fraext", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["framode"] = new UiFieldDescriptor("framode", "Mode", "fieldset", "text", false, false, 8, "", "", "text", null, "", "framode", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblaccount"] = new UiFieldDescriptor("lblaccount", "Account", "display", "text", false, false, 11, "", "", "numeric", null, "", "lblaccount", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbalance"] = new UiFieldDescriptor("lblbalance", "Balance", "display", "text", false, false, 12, "", "", "numeric", null, "", "lblbalance", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbankname"] = new UiFieldDescriptor("lblbankname", "Bank Name", "display", "text", false, false, 13, "", "", "text", null, "", "lblbankname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblchequeissued"] = new UiFieldDescriptor("lblchequeissued", "Cheque Issued", "display", "text", false, false, 14, "", "", "text", null, "", "lblchequeissued", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustomer"] = new UiFieldDescriptor("lblcustomer", "Customer", "display", "text", false, false, 15, "", "", "text", null, "", "lblcustomer", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustomerid"] = new UiFieldDescriptor("lblcustomerid", "Customer ID", "display", "text", false, false, 16, "", "", "numeric", null, "", "lblcustomerid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbldate"] = new UiFieldDescriptor("lbldate", "Date", "display", "text", false, false, 17, "", "", "text", null, "", "lbldate", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblfirstname"] = new UiFieldDescriptor("lblfirstname", "First Name", "display", "text", false, false, 19, "", "", "text", null, "", "lblfirstname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbllastname"] = new UiFieldDescriptor("lbllastname", "Last Name", "display", "text", false, false, 21, "", "", "text", null, "", "lbllastname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltypeofaccount"] = new UiFieldDescriptor("lbltypeofaccount", "Type Of Account", "display", "text", false, false, 22, "", "", "numeric", null, "", "lbltypeofaccount", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["optcash"] = new UiFieldDescriptor("optcash", "Cash", "radio", "text", false, true, 23, "Optional cash", "", "text", null, "", "paymentmode", "paymentmode", "Payment Mode", "Cash", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["optcheque"] = new UiFieldDescriptor("optcheque", "Cheque", "radio", "text", false, true, 24, "Optional cheque", "", "text", null, "", "paymentmode", "paymentmode", "Payment Mode", "Cheque", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["optno"] = new UiFieldDescriptor("optno", "No", "radio", "text", false, true, 25, "Optional no", "", "text", null, "", "yesno", "yesno", "Yes Or No", "No", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["optyes"] = new UiFieldDescriptor("optyes", "Yes", "radio", "text", false, true, 26, "Optional yes", "", "text", null, "", "yesno", "yesno", "Yes Or No", "Yes", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtbankname"] = new UiFieldDescriptor("txtbankname", "Bank Name", "input", "text", true, true, 27, "Enter bank name", "", "text", null, "", "txtbankname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtbankname-REQUIRED", "required", "Bank Name is required.", null) }),
                ["txtchequeno"] = new UiFieldDescriptor("txtchequeno", "Cheque No", "input", "text", false, true, 28, "Optional cheque no", "", "text", null, "", "txtchequeno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtsearchaccountno"] = new UiFieldDescriptor("txtsearchaccountno", "Search Account No", "input", "number", true, true, 29, "Enter search account no", "Enter digits only.", "numeric", null, "", "txtsearchaccountno", "", "", "", new[] { "Yes", "No" }, new[] { new UiValidationDescriptor("VR-txtsearchaccountno-REQUIRED", "required", "Search Account No is required.", null), new UiValidationDescriptor("VR-txtsearchaccountno-NUMERIC", "numeric", "Search Account No must be numeric.", null) }),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /transactions/deposit, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmwithdraw"] = new UiScreenDescriptor(
            "frmwithdraw",
            "Withdrawal processing and balance deduction workflow",
            "Withdrawal processing and balance deduction workflow.",
            "/ui/frmwithdraw",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Wit Hd Raw", 1, new[] { "txtdateoftransaction", "fracheque", "frawithdrawn", "lblaccountno", "lblaccounttype", "lblbalance", "lblcheque", "lblchequeissued", "lblcustid", "lblcustomerid", "lbldate", "lblfirstname", "lbllastname", "lbltag", "lbltypeofaccount", "optno", "optyes", "txtaccountno", "txttransactionid", "txtwithdrawn" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["txtdateoftransaction"] = new UiFieldDescriptor("txtdateoftransaction", "Date Of Transaction", "input", "date", false, true, 1, "Optional date of transaction", "", "text", null, "", "txtdateoftransaction", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["fracheque"] = new UiFieldDescriptor("fracheque", "Cheque", "fieldset", "text", false, false, 8, "", "", "text", null, "", "fracheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["frawithdrawn"] = new UiFieldDescriptor("frawithdrawn", "Wit Hdr Awn", "fieldset", "text", false, false, 9, "", "", "text", null, "", "frawithdrawn", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblaccountno"] = new UiFieldDescriptor("lblaccountno", "Account No", "display", "text", false, false, 12, "", "", "numeric", null, "", "lblaccountno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["lblaccounttype"] = new UiFieldDescriptor("lblaccounttype", "Account Type", "display", "text", false, false, 13, "", "", "numeric", null, "", "lblaccounttype", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbalance"] = new UiFieldDescriptor("lblbalance", "Balance", "display", "text", false, false, 14, "", "", "numeric", null, "", "lblbalance", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcheque"] = new UiFieldDescriptor("lblcheque", "Cheque", "display", "text", false, false, 15, "", "", "text", null, "", "lblcheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["lblchequeissued"] = new UiFieldDescriptor("lblchequeissued", "Cheque Issued", "display", "text", false, false, 16, "", "", "text", null, "", "lblchequeissued", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustid"] = new UiFieldDescriptor("lblcustid", "Cus Tid", "display", "text", false, false, 17, "", "", "numeric", null, "", "lblcustid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustomerid"] = new UiFieldDescriptor("lblcustomerid", "Customer ID", "display", "text", false, false, 18, "", "", "numeric", null, "", "lblcustomerid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbldate"] = new UiFieldDescriptor("lbldate", "Date", "display", "text", false, false, 19, "", "", "text", null, "", "lbldate", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblfirstname"] = new UiFieldDescriptor("lblfirstname", "First Name", "display", "text", false, false, 21, "", "", "text", null, "", "lblfirstname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbllastname"] = new UiFieldDescriptor("lbllastname", "Last Name", "display", "text", false, false, 23, "", "", "text", null, "", "lbllastname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltag"] = new UiFieldDescriptor("lbltag", "Tag", "display", "text", false, false, 24, "", "", "text", null, "", "lbltag", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltypeofaccount"] = new UiFieldDescriptor("lbltypeofaccount", "Type Of Account", "display", "text", false, false, 25, "", "", "numeric", null, "", "lbltypeofaccount", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["optno"] = new UiFieldDescriptor("optno", "No", "radio", "text", false, true, 26, "Optional no", "", "text", null, "", "yesno", "yesno", "Yes Or No", "No", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["optyes"] = new UiFieldDescriptor("optyes", "Yes", "radio", "text", false, true, 27, "Optional yes", "", "text", null, "", "yesno", "yesno", "Yes Or No", "Yes", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtaccountno"] = new UiFieldDescriptor("txtaccountno", "Account No", "input", "number", true, true, 28, "Enter account no", "Enter digits only.", "numeric", null, "", "txtaccountno", "", "", "", new[] { "Yes", "No" }, new[] { new UiValidationDescriptor("VR-txtaccountno-REQUIRED", "required", "Account No is required.", null), new UiValidationDescriptor("VR-txtaccountno-NUMERIC", "numeric", "Account No must be numeric.", null) }),
                ["txttransactionid"] = new UiFieldDescriptor("txttransactionid", "Transaction ID", "input", "number", false, true, 29, "Optional transaction id", "Enter digits only.", "numeric", null, "", "txttransactionid", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txttransactionid-NUMERIC", "numeric", "Transaction ID must be numeric.", null) }),
                ["txtwithdrawn"] = new UiFieldDescriptor("txtwithdrawn", "Wit Hdr Awn", "input", "text", false, true, 30, "Optional wit hdr awn", "", "text", null, "", "txtwithdrawn", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmcheckbalance"] = new UiScreenDescriptor(
            "frmcheckbalance",
            "Balance inquiry and reconciliation workflow",
            "Balance inquiry and reconciliation workflow.",
            "/ui/frmcheckbalance",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Che Ck Balance", 1, new[] { "dtpicker1", "cboaccountno", "lblaccno", "lblaccountno", "lblbal", "lblbalance", "lblcontacttitle", "lblcustomerid", "lbldate", "lblfirstname", "lbllastname", "txtacno", "txtcontacttitle", "txtcustomerid", "txtfirstname", "txtlastname", "txttypeofaccount" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["dtpicker1"] = new UiFieldDescriptor("dtpicker1", "Ick Er1", "input", "text", false, true, 1, "Optional ick er1", "", "text", null, "", "dtpicker1", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["cboaccountno"] = new UiFieldDescriptor("cboaccountno", "Account No", "select", "text", false, true, 2, "Optional account no", "", "text", null, "", "cboaccountno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["lblaccno"] = new UiFieldDescriptor("lblaccno", "Ac Cno", "display", "text", false, false, 7, "", "", "text", null, "", "lblaccno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["lblaccountno"] = new UiFieldDescriptor("lblaccountno", "Account No", "display", "text", false, false, 8, "", "", "numeric", null, "", "lblaccountno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbal"] = new UiFieldDescriptor("lblbal", "Bal", "display", "text", false, false, 9, "", "", "text", null, "", "lblbal", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbalance"] = new UiFieldDescriptor("lblbalance", "Balance", "display", "text", false, false, 10, "", "", "numeric", null, "", "lblbalance", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcontacttitle"] = new UiFieldDescriptor("lblcontacttitle", "Con Tac Tti Tle", "display", "text", false, false, 11, "", "", "text", null, "", "lblcontacttitle", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustomerid"] = new UiFieldDescriptor("lblcustomerid", "Customer ID", "display", "text", false, false, 12, "", "", "numeric", null, "", "lblcustomerid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbldate"] = new UiFieldDescriptor("lbldate", "Date", "display", "text", false, false, 13, "", "", "text", null, "", "lbldate", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblfirstname"] = new UiFieldDescriptor("lblfirstname", "First Name", "display", "text", false, false, 14, "", "", "text", null, "", "lblfirstname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbllastname"] = new UiFieldDescriptor("lbllastname", "Last Name", "display", "text", false, false, 15, "", "", "text", null, "", "lbllastname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtacno"] = new UiFieldDescriptor("txtacno", "A Cno", "input", "text", false, true, 16, "Optional a cno", "", "text", null, "", "txtacno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtcontacttitle"] = new UiFieldDescriptor("txtcontacttitle", "Con Tac Tti Tle", "input", "text", false, true, 17, "Optional con tac tti tle", "", "text", null, "", "txtcontacttitle", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtcustomerid"] = new UiFieldDescriptor("txtcustomerid", "Customer ID", "input", "number", true, true, 18, "Enter customer id", "Enter digits only.", "numeric", null, "", "txtcustomerid", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtcustomerid-REQUIRED", "required", "Customer ID is required.", null), new UiValidationDescriptor("VR-txtcustomerid-NUMERIC", "numeric", "Customer ID must be numeric.", null) }),
                ["txtfirstname"] = new UiFieldDescriptor("txtfirstname", "First Name", "input", "text", true, true, 19, "Enter first name", "", "text", null, "", "txtfirstname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtfirstname-REQUIRED", "required", "First Name is required.", null) }),
                ["txtlastname"] = new UiFieldDescriptor("txtlastname", "Last Name", "input", "text", true, true, 20, "Enter last name", "", "text", null, "", "txtlastname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtlastname-REQUIRED", "required", "Last Name is required.", null) }),
                ["txttypeofaccount"] = new UiFieldDescriptor("txttypeofaccount", "Type Of Account", "input", "number", true, true, 21, "Enter type of account", "Enter digits only.", "numeric", null, "", "txttypeofaccount", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, new[] { new UiValidationDescriptor("VR-txttypeofaccount-REQUIRED", "required", "Type Of Account is required.", null), new UiValidationDescriptor("VR-txttypeofaccount-NUMERIC", "numeric", "Type Of Account must be numeric.", null) }),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /transactions/deposit, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        };
}
