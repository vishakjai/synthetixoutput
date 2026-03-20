using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ReportingService.Pages.FrmMonthly;

public sealed class IndexModel : PageModel
{
    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);

    public void OnGet()
    {
        State["screenId"] = "frmMonthly";
    }

    public IActionResult OnPost()
    {
        Errors.Clear();
        // No contract validation rules were provided.
        if (Errors.Count > 0)
        {
            return Page();
        }
        State["lastAction"] = "submitted";
        return Redirect("/ui/frmmonthly");
    }
}
