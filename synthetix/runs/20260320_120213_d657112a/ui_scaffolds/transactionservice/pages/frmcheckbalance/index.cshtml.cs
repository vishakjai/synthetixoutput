using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TransactionService.Pages.Frmcheckbalance;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmcheckbalance";
    }

    public IActionResult OnPost()
    {
        Errors.Clear();
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["cboaccountno"]))
        {
            Errors["cboaccountno"] = "Cboaccountno is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["lblaccountno"]))
        {
            Errors["lblaccountno"] = "Lblaccountno is required.";
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
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtcustomerid"]))
        {
            Errors["txtcustomerid"] = "Txtcustomerid is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtfirstname"]))
        {
            Errors["txtfirstname"] = "Txtfirstname is required.";
        }
        if (!Request.HasFormContentType || string.IsNullOrWhiteSpace(Request.Form["txtlastname"]))
        {
            Errors["txtlastname"] = "Txtlastname is required.";
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
        return Redirect("/ui/frmcheckbalance");
    }
}
