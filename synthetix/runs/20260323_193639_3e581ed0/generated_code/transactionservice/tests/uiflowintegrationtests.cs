using TransactionService.Services;
using Xunit;

namespace TransactionService.Tests;

public sealed class UiFlowIntegrationTests
{
    [Fact]
    public void Registry_contains_contract_backed_ui_events()
    {
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmdeposit"].Keys);
        Assert.Contains("POST /transactions/deposit", UiEventRegistry.Registry["frmdeposit"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmdeposit"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmdeposit"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmdeposit"]["evt_cancel"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmwithdraw"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmwithdraw"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmcheckbalance"].Keys);
        Assert.Contains("POST /transactions/deposit", UiEventRegistry.Registry["frmcheckbalance"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmcheckbalance"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmcheckbalance"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmcheckbalance"]["evt_cancel"].Targets);
    }

    [Fact]
    public void Screen_registry_exposes_sections_fields_and_actions()
    {
        Assert.NotEmpty(UiScreenRegistry.Screens);
        var descriptor = UiScreenRegistry.Screens["frmdeposit"];
        Assert.NotEmpty(descriptor.Sections);
        Assert.NotEmpty(descriptor.Fields);
        Assert.NotEmpty(descriptor.Actions);
    }
}
