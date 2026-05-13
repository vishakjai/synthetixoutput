using Billing and Finance.Services;
using Xunit;

namespace BillingAndFinance.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("Billing_and_Finance", UiScaffoldRegistry.BillingAndFinanceDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.BillingAndFinanceDescriptorJson);
        Assert.Contains("/ui/billing-and-finance", UiScaffoldRegistry.RouteMap["/billing_and_finance"]);
    }
}
