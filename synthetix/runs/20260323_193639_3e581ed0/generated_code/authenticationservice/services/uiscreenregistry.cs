namespace AuthenticationService.Services;

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
        ["frmLogin1"] = new UiScreenDescriptor(
            "frmLogin1",
            "Authentication and credential validation workflow",
            "Authentication and credential validation workflow.",
            "/ui/frmlogin1",
            "high",
            0.87,
            new[]
            {
                new UiSectionDescriptor("sec_main", "Login1", 1, new[] { "lbllabels", "txtpass", "txtun" }),
            },
            new Dictionary<string, UiFieldDescriptor>(StringComparer.OrdinalIgnoreCase)
            {
                ["lbllabels"] = new UiFieldDescriptor("lbllabels", "S", "display", "text", false, false, 3, "", "", "text", null, "", "lbllabels", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtpass"] = new UiFieldDescriptor("txtpass", "Pass", "input", "password", false, true, 4, "Optional pass", "", "text", null, "", "txtpass", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
                ["txtun"] = new UiFieldDescriptor("txtun", "Un", "input", "text", false, true, 5, "Optional un", "", "text", null, "", "txtun", "", "", "", new[] { "Select an option" }, Array.Empty<UiValidationDescriptor>()),
            },
            new[]
            {
                new UiActionDescriptor("evt_save", "Save", "primary", "footer", "POST /auth/login, list"),
                new UiActionDescriptor("evt_cancel", "Cancel", "secondary", "footer", "previous"),
            }
        ),
        };
}
