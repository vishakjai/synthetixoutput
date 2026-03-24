namespace CustomerService.Services;

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
        ["frmcloseacount"] = new UiScreenDescriptor(
            "frmcloseacount",
            "Account closure and settlement workflow",
            "Account closure and settlement workflow.",
            "/ui/frmcloseacount",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Close Aco Unt", 1, new[] { "txtdateofopen", "cbosex", "fracheque", "franominee", "frasearch", "lblcheque", "lbldateofopen", "lblnominee", "lblphoneno", "lblaccountno", "lbladdress", "lblbalance", "lblcustid", "lblcustomerid", "lbldateofbirth", "lblfirstname", "lbllastname", "lblmiddlename", "lblrelationship", "lblsex", "lbltype", "optmajor", "optminor", "optno", "optyes", "txtaccountno", "txtaddress", "txtbalance", "txtcustid", "txtcustomerid", "txtdob", "txtfirstname", "txtlastname", "txtmiddlename", "txtmobileno", "txtnominee", "txtphoneno", "txtpincode", "txtrelationship" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["txtdateofopen"] = new UiFieldDescriptor("txtdateofopen", "Date Ofo Pen", "input", "date", false, true, 1, "Optional date ofo pen", "", "text", null, "", "txtdateofopen", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["cbosex"] = new UiFieldDescriptor("cbosex", "Sex", "select", "text", false, true, 2, "Optional sex", "", "text", null, "", "cbosex", "", "", "", new[] { "Male", "Female", "Other" }, Array.Empty<UiValidationDescriptor>()),
                ["fracheque"] = new UiFieldDescriptor("fracheque", "Cheque", "fieldset", "text", false, false, 8, "", "", "text", null, "", "fracheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["franominee"] = new UiFieldDescriptor("franominee", "Nom I Nee", "fieldset", "text", false, false, 9, "", "", "text", null, "", "franominee", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["frasearch"] = new UiFieldDescriptor("frasearch", "Search", "fieldset", "text", false, false, 10, "", "", "text", null, "", "frasearch", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcheque"] = new UiFieldDescriptor("lblcheque", "Cheque", "display", "text", false, false, 11, "", "", "text", null, "", "lblcheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["lbldateofopen"] = new UiFieldDescriptor("lbldateofopen", "Date Of Open", "display", "text", false, false, 12, "", "", "text", null, "", "lbldateofopen", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblnominee"] = new UiFieldDescriptor("lblnominee", "Nominee", "display", "text", false, false, 13, "", "", "text", null, "", "lblnominee", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblphoneno"] = new UiFieldDescriptor("lblphoneno", "Phone No", "display", "text", false, false, 14, "", "", "text", null, "", "lblphoneno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["lblaccountno"] = new UiFieldDescriptor("lblaccountno", "Account No", "display", "text", false, false, 15, "", "", "numeric", null, "", "lblaccountno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["lbladdress"] = new UiFieldDescriptor("lbladdress", "Add R Ess", "display", "text", false, false, 16, "", "", "text", null, "", "lbladdress", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbalance"] = new UiFieldDescriptor("lblbalance", "Balance", "display", "text", false, false, 17, "", "", "numeric", null, "", "lblbalance", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustid"] = new UiFieldDescriptor("lblcustid", "Cus Tid", "display", "text", false, false, 18, "", "", "numeric", null, "", "lblcustid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcustomerid"] = new UiFieldDescriptor("lblcustomerid", "Customer ID", "display", "text", false, false, 19, "", "", "numeric", null, "", "lblcustomerid", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbldateofbirth"] = new UiFieldDescriptor("lbldateofbirth", "Date Ofb I Rth", "display", "text", false, false, 20, "", "", "text", null, "", "lbldateofbirth", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblfirstname"] = new UiFieldDescriptor("lblfirstname", "First Name", "display", "text", false, false, 21, "", "", "text", null, "", "lblfirstname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbllastname"] = new UiFieldDescriptor("lbllastname", "Last Name", "display", "text", false, false, 22, "", "", "text", null, "", "lbllastname", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblmiddlename"] = new UiFieldDescriptor("lblmiddlename", "M Idd Le Name", "display", "text", false, false, 23, "", "", "numeric", null, "", "lblmiddlename", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblrelationship"] = new UiFieldDescriptor("lblrelationship", "Rel Ati Ons Hip", "display", "text", false, false, 24, "", "", "text", null, "", "lblrelationship", "", "", "", new[] { "Self", "Spouse", "Parent", "Guardian", "Nominee" }, Array.Empty<UiValidationDescriptor>()),
                ["lblsex"] = new UiFieldDescriptor("lblsex", "Sex", "display", "text", false, false, 25, "", "", "text", null, "", "lblsex", "", "", "", new[] { "Male", "Female", "Other" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltype"] = new UiFieldDescriptor("lbltype", "Type", "display", "text", false, false, 26, "", "", "text", null, "", "lbltype", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["optmajor"] = new UiFieldDescriptor("optmajor", "Ma Jor", "radio", "text", false, true, 27, "Optional ma jor", "", "text", null, "", "majorminor", "majorminor", "Major Or Minor", "Major", new[] { "Major", "Minor" }, Array.Empty<UiValidationDescriptor>()),
                ["optminor"] = new UiFieldDescriptor("optminor", "Mi Nor", "radio", "text", false, true, 28, "Optional mi nor", "", "text", null, "", "majorminor", "majorminor", "Major Or Minor", "Minor", new[] { "Major", "Minor" }, Array.Empty<UiValidationDescriptor>()),
                ["optno"] = new UiFieldDescriptor("optno", "No", "radio", "text", false, true, 29, "Optional no", "", "text", null, "", "yesno", "yesno", "Yes Or No", "No", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["optyes"] = new UiFieldDescriptor("optyes", "Yes", "radio", "text", false, true, 30, "Optional yes", "", "text", null, "", "yesno", "yesno", "Yes Or No", "Yes", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtaccountno"] = new UiFieldDescriptor("txtaccountno", "Account No", "input", "number", true, true, 31, "Enter account no", "Enter digits only.", "numeric", null, "", "txtaccountno", "", "", "", new[] { "Yes", "No" }, new[] { new UiValidationDescriptor("VR-txtaccountno-REQUIRED", "required", "Account No is required.", null), new UiValidationDescriptor("VR-txtaccountno-NUMERIC", "numeric", "Account No must be numeric.", null) }),
                ["txtaddress"] = new UiFieldDescriptor("txtaddress", "Add R Ess", "input", "text", false, true, 32, "Optional add r ess", "", "text", null, "", "txtaddress", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtbalance"] = new UiFieldDescriptor("txtbalance", "Balance", "input", "number", false, true, 33, "Optional balance", "Enter digits only.", "numeric", null, "", "txtbalance", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtbalance-NUMERIC", "numeric", "Balance must be numeric.", null) }),
                ["txtcustid"] = new UiFieldDescriptor("txtcustid", "Cus Tid", "input", "number", false, true, 34, "Optional cus tid", "Enter digits only.", "numeric", null, "", "txtcustid", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtcustid-NUMERIC", "numeric", "Cus Tid must be numeric.", null) }),
                ["txtcustomerid"] = new UiFieldDescriptor("txtcustomerid", "Customer ID", "input", "number", true, true, 35, "Enter customer id", "Enter digits only.", "numeric", null, "", "txtcustomerid", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtcustomerid-REQUIRED", "required", "Customer ID is required.", null), new UiValidationDescriptor("VR-txtcustomerid-NUMERIC", "numeric", "Customer ID must be numeric.", null) }),
                ["txtdob"] = new UiFieldDescriptor("txtdob", "Dob", "input", "text", false, true, 36, "Optional dob", "", "text", null, "", "txtdob", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtfirstname"] = new UiFieldDescriptor("txtfirstname", "First Name", "input", "text", true, true, 37, "Enter first name", "", "text", null, "", "txtfirstname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtfirstname-REQUIRED", "required", "First Name is required.", null) }),
                ["txtlastname"] = new UiFieldDescriptor("txtlastname", "Last Name", "input", "text", true, true, 38, "Enter last name", "", "text", null, "", "txtlastname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtlastname-REQUIRED", "required", "Last Name is required.", null) }),
                ["txtmiddlename"] = new UiFieldDescriptor("txtmiddlename", "M Idd Le Name", "input", "number", true, true, 39, "Enter m idd le name", "Enter digits only.", "numeric", null, "", "txtmiddlename", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtmiddlename-REQUIRED", "required", "M Idd Le Name is required.", null), new UiValidationDescriptor("VR-txtmiddlename-NUMERIC", "numeric", "M Idd Le Name must be numeric.", null) }),
                ["txtmobileno"] = new UiFieldDescriptor("txtmobileno", "Mob Il Eno", "input", "text", false, true, 40, "Optional mob il eno", "", "text", null, "", "txtmobileno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtnominee"] = new UiFieldDescriptor("txtnominee", "Nom I Nee", "input", "text", false, true, 41, "Optional nom i nee", "", "text", null, "", "txtnominee", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtphoneno"] = new UiFieldDescriptor("txtphoneno", "Pho N Eno", "input", "text", false, true, 42, "Optional pho n eno", "", "text", null, "", "txtphoneno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtpincode"] = new UiFieldDescriptor("txtpincode", "Pin C Ode", "input", "text", false, true, 43, "Optional pin c ode", "", "text", null, "", "txtpincode", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtrelationship"] = new UiFieldDescriptor("txtrelationship", "Rel Ati Ons Hip", "input", "text", false, true, 44, "Optional rel ati ons hip", "", "text", null, "", "txtrelationship", "", "", "", new[] { "Self", "Spouse", "Parent", "Guardian", "Nominee" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "PUT /customer/closeacount, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmcustomer"] = new UiScreenDescriptor(
            "frmcustomer",
            "Customer profile onboarding and maintenance workflow",
            "Customer profile onboarding and maintenance workflow.",
            "/ui/frmcustomer",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Customer", 1, new[] { "txtdateofopen", "txtdob", "cbosex", "fracheque", "franominee", "frasearch", "lblcheque", "lbldateofopen", "lblnominee", "lblphoneno", "lblpincode", "lbladdress", "lblbalance", "lbldateofbirth", "lblmiddlename", "lblrelationship", "lblsex", "lbltype", "lbltypeofaccount", "optmajor", "optminor", "optno", "optyes", "txtaccountno", "txtaddress", "txtbalance", "txtcustomerid", "txtfirstname", "txtlastname", "txtmiddlename", "txtmobileno", "txtnominee", "txtphoneno", "txtpincode", "txtrelationship", "txtsearch" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["txtdateofopen"] = new UiFieldDescriptor("txtdateofopen", "Date Ofo Pen", "input", "date", false, true, 1, "Optional date ofo pen", "", "text", null, "", "txtdateofopen", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtdob"] = new UiFieldDescriptor("txtdob", "Dob", "input", "text", false, true, 2, "Optional dob", "", "text", null, "", "txtdob", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["cbosex"] = new UiFieldDescriptor("cbosex", "Sex", "select", "text", false, true, 3, "Optional sex", "", "text", null, "", "cbosex", "", "", "", new[] { "Male", "Female", "Other" }, Array.Empty<UiValidationDescriptor>()),
                ["fracheque"] = new UiFieldDescriptor("fracheque", "Cheque", "fieldset", "text", false, false, 7, "", "", "text", null, "", "fracheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["franominee"] = new UiFieldDescriptor("franominee", "Nom I Nee", "fieldset", "text", false, false, 8, "", "", "text", null, "", "franominee", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["frasearch"] = new UiFieldDescriptor("frasearch", "Search", "fieldset", "text", false, false, 9, "", "", "text", null, "", "frasearch", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcheque"] = new UiFieldDescriptor("lblcheque", "Cheque", "display", "text", false, false, 12, "", "", "text", null, "", "lblcheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["lbldateofopen"] = new UiFieldDescriptor("lbldateofopen", "Date Of Open", "display", "text", false, false, 13, "", "", "text", null, "", "lbldateofopen", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblnominee"] = new UiFieldDescriptor("lblnominee", "Nominee", "display", "text", false, false, 14, "", "", "text", null, "", "lblnominee", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblphoneno"] = new UiFieldDescriptor("lblphoneno", "Phone No", "display", "text", false, false, 15, "", "", "text", null, "", "lblphoneno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["lblpincode"] = new UiFieldDescriptor("lblpincode", "Pincode", "display", "text", false, false, 16, "", "", "text", null, "", "lblpincode", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbladdress"] = new UiFieldDescriptor("lbladdress", "Add R Ess", "display", "text", false, false, 17, "", "", "text", null, "", "lbladdress", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblbalance"] = new UiFieldDescriptor("lblbalance", "Balance", "display", "text", false, false, 18, "", "", "numeric", null, "", "lblbalance", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbldateofbirth"] = new UiFieldDescriptor("lbldateofbirth", "Date Ofb I Rth", "display", "text", false, false, 19, "", "", "text", null, "", "lbldateofbirth", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblmiddlename"] = new UiFieldDescriptor("lblmiddlename", "M Idd Le Name", "display", "text", false, false, 20, "", "", "numeric", null, "", "lblmiddlename", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblrelationship"] = new UiFieldDescriptor("lblrelationship", "Rel Ati Ons Hip", "display", "text", false, false, 21, "", "", "text", null, "", "lblrelationship", "", "", "", new[] { "Self", "Spouse", "Parent", "Guardian", "Nominee" }, Array.Empty<UiValidationDescriptor>()),
                ["lblsex"] = new UiFieldDescriptor("lblsex", "Sex", "display", "text", false, false, 22, "", "", "text", null, "", "lblsex", "", "", "", new[] { "Male", "Female", "Other" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltype"] = new UiFieldDescriptor("lbltype", "Type", "display", "text", false, false, 23, "", "", "text", null, "", "lbltype", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["lbltypeofaccount"] = new UiFieldDescriptor("lbltypeofaccount", "Type Of Account", "display", "text", false, false, 24, "", "", "numeric", null, "", "lbltypeofaccount", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["optmajor"] = new UiFieldDescriptor("optmajor", "Ma Jor", "radio", "text", false, true, 25, "Optional ma jor", "", "text", null, "", "majorminor", "majorminor", "Major Or Minor", "Major", new[] { "Major", "Minor" }, Array.Empty<UiValidationDescriptor>()),
                ["optminor"] = new UiFieldDescriptor("optminor", "Mi Nor", "radio", "text", false, true, 26, "Optional mi nor", "", "text", null, "", "majorminor", "majorminor", "Major Or Minor", "Minor", new[] { "Major", "Minor" }, Array.Empty<UiValidationDescriptor>()),
                ["optno"] = new UiFieldDescriptor("optno", "No", "radio", "text", false, true, 27, "Optional no", "", "text", null, "", "yesno", "yesno", "Yes Or No", "No", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["optyes"] = new UiFieldDescriptor("optyes", "Yes", "radio", "text", false, true, 28, "Optional yes", "", "text", null, "", "yesno", "yesno", "Yes Or No", "Yes", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtaccountno"] = new UiFieldDescriptor("txtaccountno", "Account No", "input", "number", true, true, 29, "Enter account no", "Enter digits only.", "numeric", null, "", "txtaccountno", "", "", "", new[] { "Yes", "No" }, new[] { new UiValidationDescriptor("VR-txtaccountno-REQUIRED", "required", "Account No is required.", null), new UiValidationDescriptor("VR-txtaccountno-NUMERIC", "numeric", "Account No must be numeric.", null) }),
                ["txtaddress"] = new UiFieldDescriptor("txtaddress", "Add R Ess", "input", "text", false, true, 30, "Optional add r ess", "", "text", null, "", "txtaddress", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtbalance"] = new UiFieldDescriptor("txtbalance", "Balance", "input", "number", false, true, 31, "Optional balance", "Enter digits only.", "numeric", null, "", "txtbalance", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtbalance-NUMERIC", "numeric", "Balance must be numeric.", null) }),
                ["txtcustomerid"] = new UiFieldDescriptor("txtcustomerid", "Customer ID", "input", "number", true, true, 32, "Enter customer id", "Enter digits only.", "numeric", null, "", "txtcustomerid", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtcustomerid-REQUIRED", "required", "Customer ID is required.", null), new UiValidationDescriptor("VR-txtcustomerid-NUMERIC", "numeric", "Customer ID must be numeric.", null) }),
                ["txtfirstname"] = new UiFieldDescriptor("txtfirstname", "First Name", "input", "text", true, true, 33, "Enter first name", "", "text", null, "", "txtfirstname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtfirstname-REQUIRED", "required", "First Name is required.", null) }),
                ["txtlastname"] = new UiFieldDescriptor("txtlastname", "Last Name", "input", "text", true, true, 34, "Enter last name", "", "text", null, "", "txtlastname", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtlastname-REQUIRED", "required", "Last Name is required.", null) }),
                ["txtmiddlename"] = new UiFieldDescriptor("txtmiddlename", "M Idd Le Name", "input", "number", true, true, 35, "Enter m idd le name", "Enter digits only.", "numeric", null, "", "txtmiddlename", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtmiddlename-REQUIRED", "required", "M Idd Le Name is required.", null), new UiValidationDescriptor("VR-txtmiddlename-NUMERIC", "numeric", "M Idd Le Name must be numeric.", null) }),
                ["txtmobileno"] = new UiFieldDescriptor("txtmobileno", "Mob Il Eno", "input", "text", false, true, 36, "Optional mob il eno", "", "text", null, "", "txtmobileno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtnominee"] = new UiFieldDescriptor("txtnominee", "Nom I Nee", "input", "text", false, true, 37, "Optional nom i nee", "", "text", null, "", "txtnominee", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtphoneno"] = new UiFieldDescriptor("txtphoneno", "Pho N Eno", "input", "text", false, true, 38, "Optional pho n eno", "", "text", null, "", "txtphoneno", "", "", "", new[] { "Yes", "No" }, Array.Empty<UiValidationDescriptor>()),
                ["txtpincode"] = new UiFieldDescriptor("txtpincode", "Pin C Ode", "input", "text", false, true, 39, "Optional pin c ode", "", "text", null, "", "txtpincode", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtrelationship"] = new UiFieldDescriptor("txtrelationship", "Rel Ati Ons Hip", "input", "text", false, true, 40, "Optional rel ati ons hip", "", "text", null, "", "txtrelationship", "", "", "", new[] { "Self", "Spouse", "Parent", "Guardian", "Nominee" }, Array.Empty<UiValidationDescriptor>()),
                ["txtsearch"] = new UiFieldDescriptor("txtsearch", "Search", "input", "text", false, true, 41, "Optional search", "", "text", null, "", "txtsearch", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "PUT /customer/closeacount, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["frmsettings"] = new UiScreenDescriptor(
            "frmsettings",
            "Account type maintenance and account setup workflow",
            "Account type maintenance and account setup workflow.",
            "/ui/frmsettings",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Settings", 1, new[] { "frasettings", "lblaccountid", "txtaccountid", "txtaccounttype", "txtcheque", "txtinterestrate", "txtnocheque" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["frasettings"] = new UiFieldDescriptor("frasettings", "Settings", "fieldset", "text", false, false, 5, "", "", "text", null, "", "frasettings", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblaccountid"] = new UiFieldDescriptor("lblaccountid", "Account ID", "display", "text", false, false, 8, "", "", "numeric", null, "", "lblaccountid", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, Array.Empty<UiValidationDescriptor>()),
                ["txtaccountid"] = new UiFieldDescriptor("txtaccountid", "Account ID", "input", "number", true, true, 9, "Enter account id", "Enter digits only.", "numeric", null, "", "txtaccountid", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, new[] { new UiValidationDescriptor("VR-txtaccountid-REQUIRED", "required", "Account ID is required.", null), new UiValidationDescriptor("VR-txtaccountid-NUMERIC", "numeric", "Account ID must be numeric.", null) }),
                ["txtaccounttype"] = new UiFieldDescriptor("txtaccounttype", "Account Type", "input", "number", true, true, 10, "Enter account type", "Enter digits only.", "numeric", null, "", "txtaccounttype", "", "", "", new[] { "Savings", "Current", "Fixed Deposit" }, new[] { new UiValidationDescriptor("VR-txtaccounttype-REQUIRED", "required", "Account Type is required.", null), new UiValidationDescriptor("VR-txtaccounttype-NUMERIC", "numeric", "Account Type must be numeric.", null) }),
                ["txtcheque"] = new UiFieldDescriptor("txtcheque", "Cheque", "input", "text", false, true, 11, "Optional cheque", "", "text", null, "", "txtcheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
                ["txtinterestrate"] = new UiFieldDescriptor("txtinterestrate", "Interest R Ate", "input", "number", false, true, 12, "Optional interest r ate", "Enter digits only.", "numeric", null, "", "txtinterestrate", "", "", "", new[] { "Select an option" }, new[] { new UiValidationDescriptor("VR-txtinterestrate-NUMERIC", "numeric", "Interest R Ate must be numeric.", null) }),
                ["txtnocheque"] = new UiFieldDescriptor("txtnocheque", "No Cheque", "input", "text", false, true, 13, "Optional no cheque", "", "text", null, "", "txtnocheque", "", "", "", new[] { "Cash", "Cheque" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "PUT /customer/closeacount, list"),
            }
        ),
        };
}
