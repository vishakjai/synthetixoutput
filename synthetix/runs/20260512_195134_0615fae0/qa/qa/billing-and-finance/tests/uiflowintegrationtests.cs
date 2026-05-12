using Billing and Finance.Services;
using Xunit;

namespace Billing and Finance.Tests;

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
        var descriptor = UiScreenRegistry.Screens["Billing_and_Finance"];
        Assert.NotEmpty(descriptor.Sections);
        Assert.NotEmpty(descriptor.Fields);
        Assert.NotEmpty(descriptor.Actions);
    }
}
