using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TransactionService.Pages.Frmdeposit;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmdeposit";
    }

    public IActionResult OnPost()
    {
        Errors.Clear();
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblaccount"]))
        {
            Errors["lblaccount"] = "Lblaccount is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblbankname"]))
        {
            Errors["lblbankname"] = "Lblbankname is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblcustomer"]))
        {
            Errors["lblcustomer"] = "Lblcustomer is required.";
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
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtbankname"]))
        {
            Errors["txtbankname"] = "Txtbankname is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtsearchaccountno"]))
        {
            Errors["txtsearchaccountno"] = "Txtsearchaccountno is required.";
        }
        if (Errors.Count > 0)
        {
            return Page();
        }
        State["lastAction"] = "submitted";
        return Redirect("/ui/frmdeposit");
    }
}
