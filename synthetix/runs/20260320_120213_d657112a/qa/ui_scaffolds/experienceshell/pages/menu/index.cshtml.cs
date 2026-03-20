using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExperienceShell.Pages.Menu;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> DisplayValues { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "menu";
        foreach (var fieldId in new[] {  })
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

    public IActionResult OnPostEvtCancel() => HandleEvent("evt_cancel");
}
