using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TransactionService.Pages.Frmwithdraw;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmwithdraw";
    }

    public IActionResult OnPost()
    {
        Errors.Clear();
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblaccountno"]))
        {
            Errors["lblaccountno"] = "Lblaccountno is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblaccounttype"]))
        {
            Errors["lblaccounttype"] = "Lblaccounttype is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblcustomerid"]))
        {
            Errors["lblcustomerid"] = "Lblcustomerid is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblfirstname"]))
        {
            Errors["lblfirstname"] = "Lblfirstname is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lbllastname"]))
        {
            Errors["lbllastname"] = "Lbllastname is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lbltypeofaccount"]))
        {
            Errors["lbltypeofaccount"] = "Lbltypeofaccount is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtaccountno"]))
        {
            Errors["txtaccountno"] = "Txtaccountno is required.";
        }
        if (Errors.Count > 0)
        {
            return Page();
        }
        State["lastAction"] = "submitted";
        return Redirect("/ui/frmwithdraw");
    }
}
