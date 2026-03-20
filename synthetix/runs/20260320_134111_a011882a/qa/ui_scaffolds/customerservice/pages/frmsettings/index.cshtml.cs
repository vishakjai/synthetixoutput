using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerService.Pages.Frmsettings;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> DisplayValues { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmsettings";
        foreach (var fieldId in new[] { "label1", "lblfieldlabel", "lblaccountid" })
        {
            DisplayValues[fieldId] = "Pending lookup";
        }
    }

    private IActionResult HandleEvent(string eventId)
    {
        Errors.Clear();
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtaccountid"]))
        {
            Errors["txtaccountid"] = "Account Id is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtaccounttype"]))
        {
            Errors["txtaccounttype"] = "Account Type is required.";
        }
        if (Errors.Count > 0)
        {
            return Page();
        }
        switch (eventId)
        {
            case "evt_save":
                State["lastTriggeredEvent"] = "evt_save";
                State["lastApiTarget"] = "PUT /customer/closeacount";
                // TODO: Inject IHttpClientFactory and call PUT /customer/closeacount using the Screen Contract request bindings.
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
