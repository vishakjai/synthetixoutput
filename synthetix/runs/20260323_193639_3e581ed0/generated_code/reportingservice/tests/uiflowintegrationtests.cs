using ReportingService.Services;
using Xunit;

namespace ReportingService.Tests;

public sealed class UiFlowIntegrationTests
{
    [Fact]
    public void Registry_contains_contract_backed_ui_events()
    {
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmWithinDate"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmWithinDate"]["evt_cancel"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmdaily"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmdaily"]["evt_cancel"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmmonthlyreport"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmmonthlyreport"]["evt_cancel"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmstatement"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmstatement"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["Form1"].Keys);
        Assert.Contains("POST /reporting/expireitemswithindate", UiEventRegistry.Registry["Form1"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["Form1"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["Form1"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["Form1"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmExpireItemsWithinDate"].Keys);
        Assert.Contains("POST /reporting/expireitemswithindate", UiEventRegistry.Registry["frmExpireItemsWithinDate"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmExpireItemsWithinDate"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmExpireItemsWithinDate"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmExpireItemsWithinDate"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmMonthly"].Keys);
        Assert.Contains("POST /reporting/expireitemswithindate", UiEventRegistry.Registry["frmMonthly"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmMonthly"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmMonthly"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmMonthly"]["evt_cancel"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmreport"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmreport"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmtransaction"].Keys);
        Assert.Contains("POST /reporting/expireitemswithindate", UiEventRegistry.Registry["frmtransaction"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmtransaction"]["evt_save"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmwith"].Keys);
        Assert.Contains("POST /reporting/expireitemswithindate", UiEventRegistry.Registry["frmwith"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmwith"]["evt_save"].Targets);
    }

    [Fact]
    public void Screen_registry_exposes_sections_fields_and_actions()
    {
        Assert.NotEmpty(UiScreenRegistry.Screens);
        var descriptor = UiScreenRegistry.Screens["frmWithinDate"];
        Assert.NotEmpty(descriptor.Sections);
        Assert.NotEmpty(descriptor.Fields);
        Assert.NotEmpty(descriptor.Actions);
    }
}
