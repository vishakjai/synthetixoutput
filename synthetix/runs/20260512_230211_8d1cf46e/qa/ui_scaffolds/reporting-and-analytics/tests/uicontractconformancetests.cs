using Reporting and Analytics.Services;
using Xunit;

namespace ReportingAndAnalytics.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("Reporting_and_Analytics", UiScaffoldRegistry.ReportingAndAnalyticsDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.ReportingAndAnalyticsDescriptorJson);
        Assert.Contains("/ui/reporting-and-analytics", UiScaffoldRegistry.RouteMap["/reporting_and_analytics"]);
    }
}
