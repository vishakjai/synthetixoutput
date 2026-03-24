using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LegacyCoreService.Services;

namespace LegacyCoreService.Pages.Frminterest;

public sealed class IndexModel : PageModel
{
    private readonly ScreenValidationService ValidationService;
    private readonly UiEventExecutionService EventExecutionService;
    private const string ScreenKey = "frminterest";
    private const string DefaultRoute = "/ui/frminterest";

    public IndexModel(ScreenValidationService validationService, UiEventExecutionService eventExecutionService)
    {
        ValidationService = validationService;
        EventExecutionService = eventExecutionService;
    }

    [BindProperty]
    public Dictionary<string, string> State { get; set; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public string LastTriggeredEvent { get; private set; } = string.Empty;
    public string FormStatus { get; private set; } = string.Empty;
    public string FormMessage { get; private set; } = string.Empty;
    public List<string> ExecutedTargets { get; } = new();
    public string NavigationTarget { get; private set; } = string.Empty;
    public Dictionary<string, string> DisplayValues { get; } = new(StringComparer.OrdinalIgnoreCase);
    public UiScreenDescriptor Descriptor => UiScreenRegistry.Screens[ScreenKey];

    public void OnGet()
    {
        EnsureDefaults();
        EnsureDisplayValues();
    }

    public string GetValue(string bindingName) => State.TryGetValue(bindingName, out var value) ? value : string.Empty;
    public string GetDisplayValue(string fieldId) => DisplayValues.TryGetValue(fieldId, out var value) && !string.IsNullOrWhiteSpace(value) ? value : "—";

    public string HandlerName(string eventId) => eventId switch
    {
        "evt_save" => "EvtSave",
        "evt_cancel" => "EvtCancel",
        _ => "Submit",
    };

    private void EnsureDefaults()
    {
        State["screenId"] = ScreenKey;
        foreach (var field in Descriptor.Fields.Values)
        {
            var key = field.BindingName;
            if (!string.IsNullOrWhiteSpace(key) && !State.ContainsKey(key))
            {
                State[key] = string.Empty;
            }
        }
    }

    private void EnsureDisplayValues()
    {
        foreach (var field in Descriptor.Fields.Values.Where(field => field.RenderKind == "display"))
        {
            if (!DisplayValues.ContainsKey(field.FieldId))
            {
                DisplayValues[field.FieldId] = string.Empty;
            }
        }
    }

    private IActionResult HandleEvent(string eventId)
    {
        EnsureDefaults();
        EnsureDisplayValues();
        ValidationService.ValidateScreen(ScreenKey, State, Errors);
        if (Errors.Count > 0)
        {
            FormStatus = "error";
            FormMessage = "Please correct the highlighted fields and retry.";
            return Page();
        }

        var result = EventExecutionService.Execute(ScreenKey, eventId, State);
        LastTriggeredEvent = eventId;
        FormStatus = "success";
        FormMessage = result.Message;
        NavigationTarget = result.NavigationTarget;
        ExecutedTargets.Clear();
        ExecutedTargets.AddRange(result.Targets);
        if (!string.IsNullOrWhiteSpace(result.NavigationTarget)
            && result.NavigationTarget.StartsWith("/")
            && !string.Equals(result.NavigationTarget, DefaultRoute, StringComparison.OrdinalIgnoreCase))
        {
            return Redirect(result.NavigationTarget);
        }

        return Page();
    }

    public IActionResult OnPostEvtSave() => HandleEvent("evt_save");
    public IActionResult OnPostEvtCancel() => HandleEvent("evt_cancel");
}
