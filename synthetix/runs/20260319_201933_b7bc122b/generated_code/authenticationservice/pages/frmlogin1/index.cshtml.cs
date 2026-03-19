using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AuthenticationService.Services;

namespace AuthenticationService.Pages.FrmLogin1;

public sealed class IndexModel : PageModel
{
    private readonly ScreenValidationService ValidationService;
    private readonly UiEventExecutionService EventExecutionService;

    public IndexModel(ScreenValidationService validationService, UiEventExecutionService eventExecutionService)
    {
        ValidationService = validationService;
        EventExecutionService = eventExecutionService;
    }

    public Dictionary<string, string> State { get; } = new(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, string> Errors { get; } = new(StringComparer.OrdinalIgnoreCase);
    public string LastTriggeredEvent { get; private set; } = string.Empty;

    public void OnGet()
    {
        State["screenId"] = "frmLogin1";
    }

    public IActionResult OnPostEvtSave()
    {
        ValidationService.ValidateScreen("frmLogin1", State, Errors);
        if (Errors.Count > 0)
        {
            return Page();
        }
        LastTriggeredEvent = "evt_save";
        EventExecutionService.Execute("frmLogin1", "evt_save", State);
        return Redirect("/ui/frmlogin1");
    }

    public IActionResult OnPostEvtCancel()
    {
        ValidationService.ValidateScreen("frmLogin1", State, Errors);
        if (Errors.Count > 0)
        {
            return Page();
        }
        LastTriggeredEvent = "evt_cancel";
        EventExecutionService.Execute("frmLogin1", "evt_cancel", State);
        return Redirect("/ui/frmlogin1");
    }

}
