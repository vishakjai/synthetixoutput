using TransactionService.Services;
using Xunit;

namespace TransactionService.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("frmdeposit", UiScaffoldRegistry.FrmdepositDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmdepositDescriptorJson);
        Assert.Contains("/ui/frmdeposit", UiScaffoldRegistry.RouteMap["/frmdeposit"]);
        Assert.Contains("POST /transactions/deposit", UiScaffoldRegistry.FrmdepositDescriptorJson);
        Assert.Contains("frmwithdraw", UiScaffoldRegistry.FrmwithdrawDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmwithdrawDescriptorJson);
        Assert.Contains("/ui/frmwithdraw", UiScaffoldRegistry.RouteMap["/frmwithdraw"]);
        Assert.Contains("frmcheckbalance", UiScaffoldRegistry.FrmcheckbalanceDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmcheckbalanceDescriptorJson);
        Assert.Contains("/ui/frmcheckbalance", UiScaffoldRegistry.RouteMap["/frmcheckbalance"]);
        Assert.Contains("POST /transactions/deposit", UiScaffoldRegistry.FrmcheckbalanceDescriptorJson);
    }
}
