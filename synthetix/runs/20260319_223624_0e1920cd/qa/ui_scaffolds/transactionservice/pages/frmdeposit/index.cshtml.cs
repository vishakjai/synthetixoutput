using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TransactionService.Pages.Frmdeposit;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> DisplayValues { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmdeposit";
        foreach (var fieldId in new[] { "label2", "lblfieldlabel", "lblaccount", "lblbalance", "lblbankname", "lblchequeissued", "lblcustomer", "lblcustomerid", "lbldate", "lblfirst", "lblfirstname", "lbllast", "lbllastname", "lbltypeofaccount" })
        {
            DisplayValues[fieldId] = string.Empty;
        }
    }

    private IActionResult HandleEvent(string eventId)
    {
        Errors.Clear();
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtbankname"]))
        {
            Errors["txtbankname"] = "Bank Name is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtsearchaccountno"]))
        {
            Errors["txtsearchaccountno"] = "Search Account No is required.";
        }
        if (Errors.Count > 0)
        {
            return Page();
        }
        switch (eventId)
        {
            case "evt_save":
                State["lastTriggeredEvent"] = "evt_save";
                State["lastApiTarget"] = "POST /transactions/deposit";
                // TODO: Inject IHttpClientFactory and call POST /transactions/deposit using the Screen Contract request bindings.
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
