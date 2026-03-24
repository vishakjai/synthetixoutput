using LegacyCoreService.Services;
using Xunit;

namespace LegacyCoreService.Tests;

public sealed class UiFlowIntegrationTests
{
    [Fact]
    public void Registry_contains_contract_backed_ui_events()
    {
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmaddinterest"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmaddinterest"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frmdep"].Keys);
        Assert.Contains("POST /legacycore/addinterest", UiEventRegistry.Registry["frmdep"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frmdep"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmdep"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmdep"]["evt_cancel"].Targets);
        Assert.Contains("evt_save", UiEventRegistry.Registry["frminterest"].Keys);
        Assert.Contains("POST /legacycore/addinterest", UiEventRegistry.Registry["frminterest"]["evt_save"].Targets);
        Assert.Contains("list", UiEventRegistry.Registry["frminterest"]["evt_save"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frminterest"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frminterest"]["evt_cancel"].Targets);
    }

    [Fact]
    public void Screen_registry_exposes_sections_fields_and_actions()
    {
        Assert.NotEmpty(UiScreenRegistry.Screens);
        var descriptor = UiScreenRegistry.Screens["frmaddinterest"];
        Assert.NotEmpty(descriptor.Sections);
        Assert.NotEmpty(descriptor.Fields);
        Assert.NotEmpty(descriptor.Actions);
    }
}
