using Customer and Account.Services;
using Xunit;

namespace CustomerAndAccount.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("Customer_and_Account", UiScaffoldRegistry.CustomerAndAccountDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.CustomerAndAccountDescriptorJson);
        Assert.Contains("/ui/customer-and-account", UiScaffoldRegistry.RouteMap["/customer_and_account"]);
    }
}
