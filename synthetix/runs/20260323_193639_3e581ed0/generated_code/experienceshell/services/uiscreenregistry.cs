namespace ExperienceShell.Services;

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
        ["frmSplash"] = new UiScreenDescriptor(
            "frmSplash",
            "Application startup and splash workflow",
            "Application startup and splash workflow.",
            "/ui/frmsplash",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Splash", 1, new[] { "progressbar", "progressbar1", "frasplash", "image1", "lblcompany", "lblcompanyproduct", "lblcopyright", "lbllicenseto", "lblwarning", "lbldisplay", "timer1" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["progressbar"] = new UiFieldDescriptor("progressbar", "Progress Bar", "input", "text", false, true, 1, "Optional progress bar", "", "text", null, "", "progressbar", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["progressbar1"] = new UiFieldDescriptor("progressbar1", "Progress Bar1", "input", "text", false, true, 2, "Optional progress bar1", "", "text", null, "", "progressbar1", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["frasplash"] = new UiFieldDescriptor("frasplash", "Spl Ash", "fieldset", "text", false, false, 3, "", "", "text", null, "", "frasplash", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["image1"] = new UiFieldDescriptor("image1", "Image1", "input", "text", false, true, 4, "Optional image1", "", "text", null, "", "image1", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcompany"] = new UiFieldDescriptor("lblcompany", "Company", "display", "text", false, false, 5, "", "", "text", null, "", "lblcompany", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcompanyproduct"] = new UiFieldDescriptor("lblcompanyproduct", "Company Product", "display", "text", false, false, 6, "", "", "text", null, "", "lblcompanyproduct", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblcopyright"] = new UiFieldDescriptor("lblcopyright", "Copyright", "display", "text", false, false, 7, "", "", "text", null, "", "lblcopyright", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbllicenseto"] = new UiFieldDescriptor("lbllicenseto", "License To", "display", "text", false, false, 8, "", "", "text", null, "", "lbllicenseto", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lblwarning"] = new UiFieldDescriptor("lblwarning", "Warning", "display", "text", false, false, 9, "", "", "text", null, "", "lblwarning", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["lbldisplay"] = new UiFieldDescriptor("lbldisplay", "Dis P Lay", "display", "text", false, false, 10, "", "", "text", null, "", "lbldisplay", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["timer1"] = new UiFieldDescriptor("timer1", "Timer1", "input", "text", false, true, 11, "Optional timer1", "", "text", null, "", "timer1", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_search", "Search", "secondary", "header", "GET /experienceshell/splash"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        ["menu"] = new UiScreenDescriptor(
            "menu",
            "Application navigation and module routing workflow",
            "Application navigation and module routing workflow.",
            "/ui/menu",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "M Enu", 1, new[] { "mnudepositamount", "mnuexit", "mnureports", "mnuwithdrawamount", "mnubetween", "mnuclose", "mnucustomerdetails", "mnucustomermonthly", "mnugiveinterest", "mnumaster", "mnusettings", "mnutransaction", "mnutransactions" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["mnudepositamount"] = new UiFieldDescriptor("mnudepositamount", "Deposit Amount", "nav", "text", false, false, 1, "", "", "text", null, "/ui/frmdeposit", "mnudepositamount", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuexit"] = new UiFieldDescriptor("mnuexit", "Exit", "nav", "text", false, false, 2, "", "", "text", null, "/", "mnuexit", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnureports"] = new UiFieldDescriptor("mnureports", "Reports", "nav", "text", false, false, 3, "", "", "text", null, "/ui/reports", "mnureports", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuwithdrawamount"] = new UiFieldDescriptor("mnuwithdrawamount", "Withdraw Amount", "nav", "text", false, false, 4, "", "", "text", null, "/ui/frmwithdraw", "mnuwithdrawamount", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnubetween"] = new UiFieldDescriptor("mnubetween", "Bet W Een", "nav", "text", false, false, 5, "", "", "text", null, "/ui/frmwithindate", "mnubetween", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuclose"] = new UiFieldDescriptor("mnuclose", "Close", "nav", "text", false, false, 6, "", "", "text", null, "/", "mnuclose", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnucustomerdetails"] = new UiFieldDescriptor("mnucustomerdetails", "Customer Details", "nav", "text", false, false, 7, "", "", "text", null, "/ui/frmcustomer", "mnucustomerdetails", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnucustomermonthly"] = new UiFieldDescriptor("mnucustomermonthly", "Customer Monthly", "nav", "text", false, false, 8, "", "", "text", null, "/ui/frmcustomer", "mnucustomermonthly", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnugiveinterest"] = new UiFieldDescriptor("mnugiveinterest", "Give Interest", "nav", "text", false, false, 9, "", "", "text", null, "/ui/addinterest", "mnugiveinterest", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnumaster"] = new UiFieldDescriptor("mnumaster", "Mas Ter", "nav", "text", false, false, 10, "", "", "text", null, "#", "mnumaster", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnusettings"] = new UiFieldDescriptor("mnusettings", "Settings", "nav", "text", false, false, 11, "", "", "text", null, "/ui/frmsettings", "mnusettings", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnutransaction"] = new UiFieldDescriptor("mnutransaction", "Transaction", "nav", "text", false, false, 12, "", "", "text", null, "/ui/frmdeposit", "mnutransaction", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnutransactions"] = new UiFieldDescriptor("mnutransactions", "Transactions", "nav", "text", false, false, 13, "", "", "text", null, "/ui/frmdeposit", "mnutransactions", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
                new UiActionDescriptor("evt_search", "Search", "secondary", "header", "GET /experienceshell/splash"),
            }
        ),
        ["Mdi"] = new UiScreenDescriptor(
            "Mdi",
            "Application navigation and module routing workflow",
            "Application navigation and module routing workflow.",
            "/ui/mdi",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Mdi", 1, new[] { "mnudepositamount", "mnuexit", "mnureports", "mnuwithdrawamount", "mnuaddinterest", "mnuclose", "mnucustomerdetails", "mnuinterest", "mnumaster", "mnumonthly", "mnustatement", "mnutransaction", "mnutransactions", "mnuviewtransaction" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["mnudepositamount"] = new UiFieldDescriptor("mnudepositamount", "Deposit Amount", "nav", "text", false, false, 1, "", "", "text", null, "/ui/frmdeposit", "mnudepositamount", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuexit"] = new UiFieldDescriptor("mnuexit", "Exit", "nav", "text", false, false, 2, "", "", "text", null, "/", "mnuexit", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnureports"] = new UiFieldDescriptor("mnureports", "Reports", "nav", "text", false, false, 3, "", "", "text", null, "/ui/reports", "mnureports", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuwithdrawamount"] = new UiFieldDescriptor("mnuwithdrawamount", "Withdraw Amount", "nav", "text", false, false, 4, "", "", "text", null, "/ui/frmwithdraw", "mnuwithdrawamount", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuaddinterest"] = new UiFieldDescriptor("mnuaddinterest", "Add Interest", "nav", "text", false, false, 5, "", "", "text", null, "/ui/addinterest", "mnuaddinterest", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuclose"] = new UiFieldDescriptor("mnuclose", "Close", "nav", "text", false, false, 6, "", "", "text", null, "/", "mnuclose", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnucustomerdetails"] = new UiFieldDescriptor("mnucustomerdetails", "Customer Details", "nav", "text", false, false, 7, "", "", "text", null, "/ui/frmcustomer", "mnucustomerdetails", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuinterest"] = new UiFieldDescriptor("mnuinterest", "Interest", "nav", "text", false, false, 8, "", "", "text", null, "/ui/addinterest", "mnuinterest", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnumaster"] = new UiFieldDescriptor("mnumaster", "Mas Ter", "nav", "text", false, false, 9, "", "", "text", null, "#", "mnumaster", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnumonthly"] = new UiFieldDescriptor("mnumonthly", "Monthly", "nav", "text", false, false, 10, "", "", "text", null, "#", "mnumonthly", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnustatement"] = new UiFieldDescriptor("mnustatement", "Statement", "nav", "text", false, false, 11, "", "", "text", null, "#", "mnustatement", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnutransaction"] = new UiFieldDescriptor("mnutransaction", "Transaction", "nav", "text", false, false, 12, "", "", "text", null, "/ui/frmdeposit", "mnutransaction", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnutransactions"] = new UiFieldDescriptor("mnutransactions", "Transactions", "nav", "text", false, false, 13, "", "", "text", null, "/ui/frmdeposit", "mnutransactions", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["mnuviewtransaction"] = new UiFieldDescriptor("mnuviewtransaction", "View Transaction", "nav", "text", false, false, 14, "", "", "text", null, "/ui/frmdeposit", "mnuviewtransaction", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_search", "Search", "secondary", "header", "GET /experienceshell/splash"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        };
}
