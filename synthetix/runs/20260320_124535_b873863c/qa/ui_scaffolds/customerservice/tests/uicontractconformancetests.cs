using CustomerService.Services;
using Xunit;

namespace CustomerService.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("frmcloseacount", UiScaffoldRegistry.FrmcloseacountDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmcloseacountDescriptorJson);
        Assert.Contains("/ui/frmcloseacount", UiScaffoldRegistry.RouteMap["/frmcloseacount"]);
        Assert.Contains("PUT /customer/closeacount", UiScaffoldRegistry.FrmcloseacountDescriptorJson);
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
