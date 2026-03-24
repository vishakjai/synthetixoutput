using ExperienceShell.Services;
using Xunit;

namespace ExperienceShell.Tests;

public sealed class UiFlowIntegrationTests
{
    [Fact]
    public void Registry_contains_contract_backed_ui_events()
    {
        Assert.Contains("evt_search", UiEventRegistry.Registry["frmSplash"].Keys);
        Assert.Contains("GET /experienceshell/splash", UiEventRegistry.Registry["frmSplash"]["evt_search"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["frmSplash"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["frmSplash"]["evt_cancel"].Targets);
        Assert.Contains("evt_search", UiEventRegistry.Registry["menu"].Keys);
        Assert.Contains("GET /experienceshell/splash", UiEventRegistry.Registry["menu"]["evt_search"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["menu"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["menu"]["evt_cancel"].Targets);
        Assert.Contains("evt_search", UiEventRegistry.Registry["Mdi"].Keys);
        Assert.Contains("GET /experienceshell/splash", UiEventRegistry.Registry["Mdi"]["evt_search"].Targets);
        Assert.Contains("evt_cancel", UiEventRegistry.Registry["Mdi"].Keys);
        Assert.Contains("previous", UiEventRegistry.Registry["Mdi"]["evt_cancel"].Targets);
    }

    [Fact]
    public void Screen_registry_exposes_sections_fields_and_actions()
    {
        Assert.NotEmpty(UiScreenRegistry.Screens);
        var descriptor = UiScreenRegistry.Screens["frmSplash"];
        Assert.NotEmpty(descriptor.Sections);
        Assert.NotEmpty(descriptor.Fields);
        Assert.NotEmpty(descriptor.Actions);
    }
}
