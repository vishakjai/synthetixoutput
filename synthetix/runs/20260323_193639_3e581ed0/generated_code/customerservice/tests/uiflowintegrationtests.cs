using CustomerService.Services;
using Xunit;

namespace CustomerService.Tests;

public sealed class UiFlowIntegrationTests
{
    [Fact]
    public void Registry_contains_contract_backed_ui_events()
    {
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmcloseacount"].Keys);
        Assert.Contains("PUT /customer/closeacount", UiEventRegistry.Registry["frmcloseacount"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmcloseacount"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmcloseacount"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmcloseacount"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmcustomer"].Keys);
        Assert.Contains("PUT /customer/closeacount", UiEventRegistry.Registry["frmcustomer"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmcustomer"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmcustomer"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmcustomer"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmsettings"].Keys);
        Assert.Contains("PUT /customer/closeacount", UiEventRegistry.Registry["frmsettings"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmsettings"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmsettings"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmsettings"]["evt_cancel"].Targets);
    }

    [Fact]
    public void Screen_registry_exposes_sections_fields_and_actions()
    {
        Assert.NotEmpty(UiScreenRegistry.Screens);
        var descriptor = UiScreenRegistry.Screens["frmcloseacount"];
        Assert.NotEmpty(descriptor.Sections);
        Assert.NotEmpty(descriptor.Fields);
        Assert.NotEmpty(descriptor.Actions);
    }
}
