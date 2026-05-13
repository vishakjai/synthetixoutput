using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerAndAccount.Pages.CustomerAndAccount;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> DisplayValues { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "Customer_and_Account";
        foreach (var fieldId in new[] {  })
        {
            DisplayValues[fieldId] = string.Empty;
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

            default:
                State["lastTriggeredEvent"] = eventId;
                break;
        }
        return Page();
    }

    public IActionResult OnPost() => Page();
}
