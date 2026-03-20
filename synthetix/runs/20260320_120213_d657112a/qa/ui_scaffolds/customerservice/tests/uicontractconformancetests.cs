using CustomerService.Services;
using Xunit;

namespace CustomerService.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("frmcloseaccount", UiScaffoldRegistry.FrmcloseaccountDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmcloseaccountDescriptorJson);
        Assert.Contains("/ui/frmcloseaccount", UiScaffoldRegistry.RouteMap["/frmcloseaccount"]);
        Assert.Contains("PUT /customer/closeacount", UiScaffoldRegistry.FrmcloseaccountDescriptorJson);
        Assert.Contains("frmcustomer", UiScaffoldRegistry.FrmcustomerDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmcustomerDescriptorJson);
        Assert.Contains("/ui/frmcustomer", UiScaffoldRegistry.RouteMap["/frmcustomer"]);
        Assert.Contains("PUT /customer/closeacount", UiScaffoldRegistry.FrmcustomerDescriptorJson);
        Assert.Contains("frmsettings", UiScaffoldRegistry.FrmsettingsDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmsettingsDescriptorJson);
        Assert.Contains("/ui/frmsettings", UiScaffoldRegistry.RouteMap["/frmsettings"]);
        Assert.Contains("PUT /customer/closeacount", UiScaffoldRegistry.FrmsettingsDescriptorJson);
    }
}
