using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReportingService.Pages.Frmreport;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmreport";
    }

    public IActionResult OnPost()
    {
        Errors.Clear();
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblcustomerid"]))
        {
            Errors["lblcustomerid"] = "Lblcustomerid is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtfirstname"]))
        {
            Errors["txtfirstname"] = "Txtfirstname is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtlastname"]))
        {
            Errors["txtlastname"] = "Txtlastname is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtaccount"]))
        {
            Errors["txtaccount"] = "Txtaccount is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtaccountno"]))
        {
            Errors["txtaccountno"] = "Txtaccountno is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtcustomerid"]))
        {
            Errors["txtcustomerid"] = "Txtcustomerid is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txttypeofaccount"]))
        {
            Errors["txttypeofaccount"] = "Txttypeofaccount is required.";
        }
        if (Errors.Count > 0)
        {
            return Page();
        }
        State["lastAction"] = "submitted";
        return Redirect("/ui/frmreport");
    }
}
