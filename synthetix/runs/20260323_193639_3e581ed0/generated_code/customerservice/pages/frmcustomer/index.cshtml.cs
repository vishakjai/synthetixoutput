using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CustomerService.Pages.Frmcustomer;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> DisplayValues { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmcustomer";
        foreach (var fieldId in new[] { "label2", "label3", "lblcheque", "lbldateofopen", "lblnominee", "lblphoneno", "lblpincode", "lbladdress", "lblbalance", "lbldateofbirth", "lblmiddlename", "lblrelationship", "lblsex", "lbltype", "lbltypeofaccount" })
        {
            DisplayValues[fieldId] = string.Empty;
        }
    }

    private IActionResult HandleEvent(string eventId)
    {
        Errors.Clear();
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtaccountno"]))
        {
            Errors["txtaccountno"] = "Account No is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtcustomerid"]))
        {
            Errors["txtcustomerid"] = "Customer ID is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtfirstname"]))
        {
            Errors["txtfirstname"] = "First Name is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtlastname"]))
        {
            Errors["txtlastname"] = "Last Name is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtmiddlename"]))
        {
            Errors["txtmiddlename"] = "M Idd Le Name is required.";
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
