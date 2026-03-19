using AuthenticationService.Services;
using Xunit;

namespace AuthenticationService.Tests;

public sealed class UiFlowIntegrationTests
{
    [Fact]
    public void Registry_contains_contract_backed_ui_events()
    {
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmLogin1"].Keys);
        Assert.Contains("POST /auth/login", UiEventRegistry.Registry["frmLogin1"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmLogin1"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmLogin1"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmLogin1"]["evt_cancel"].Targets);
    }
}
