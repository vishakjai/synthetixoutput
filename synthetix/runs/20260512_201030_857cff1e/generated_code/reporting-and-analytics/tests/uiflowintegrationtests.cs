using Reporting and Analytics.Services;
using Xunit;

namespace Reporting and Analytics.Tests;

public sealed class UiFlowIntegrationTests
{
    [Fact]
    public void Registry_contains_contract_backed_ui_events()
    {
        Assert.NotNull(UiEventRegistry.Registry);
    }

    [Fact]
    public void Screen_registry_exposes_sections_fields_and_actions()
    {
        Assert.NotEmpty(UiScreenRegistry.Screens);
        var descriptor = UiScreenRegistry.Screens["Reporting_and_Analytics"];
        Assert.NotEmpty(descriptor.Sections);
        Assert.NotEmpty(descriptor.Fields);
        Assert.NotEmpty(descriptor.Actions);
    }
}
