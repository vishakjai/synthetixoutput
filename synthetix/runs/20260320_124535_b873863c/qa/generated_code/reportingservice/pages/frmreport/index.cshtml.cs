using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReportingService.Pages.Frmreport;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> DisplayValues { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmreport";
        foreach (var fieldId in new[] { "label1", "label2", "label5", "label6", "label7", "label8", "lblcustomerid" })
        {
            DisplayValues[fieldId] = "Pending lookup";
        }
    }

    private IActionResult HandleEvent(string eventId)
    {
        Errors.Clear();
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtfirstname"]))
        {
            Errors["txtfirstname"] = "First Name is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtlastname"]))
        {
            Errors["txtlastname"] = "Last Name is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtaccount"]))
        {
            Errors["txtaccount"] = "Account is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtaccountno"]))
        {
            Errors["txtaccountno"] = "Accountno is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtcustomerid"]))
        {
            Errors["txtcustomerid"] = "Customerid is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txttypeofaccount"]))
        {
            Errors["txttypeofaccount"] = "Typeofaccount is required.";
        }
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
