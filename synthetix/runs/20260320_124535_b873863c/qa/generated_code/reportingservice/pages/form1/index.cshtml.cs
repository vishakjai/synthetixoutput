using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReportingService.Pages.Form1;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> DisplayValues { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "Form1";
        foreach (var fieldId in new[] { "label1", "label2" })
        {
            DisplayValues[fieldId] = "Pending lookup";
        }
    }

    private IActionResult HandleEvent(string eventId)
    {
        Errors.Clear();
        // No contract validation rules were provided.
        if (Errors.Count > 0)
        {
            return Page();
        }
        switch (eventId)
        {
            case "evt_save":
                State["lastTriggeredEvent"] = "evt_save";
                State["lastApiTarget"] = "POST /reporting/expireitemswithindate";
                // TODO: Inject IHttpClientFactory and call POST /reporting/expireitemswithindate using the Screen Contract request bindings.
                State["lastNavigationTarget"] = "/";
                break;
            case "evt_cancel":
                State["lastTriggeredEvent"] = "evt_cancel";
                State["lastNavigationTarget"] = "/";
                break;
            default:
                State["lastTriggeredEvent"] = eventId;
                break;
        }
        return Page();
    }

    public IActionResult OnPostEvtSave() => HandleEvent("evt_save");
    public IActionResult OnPostEvtCancel() => HandleEvent("evt_cancel");
}
