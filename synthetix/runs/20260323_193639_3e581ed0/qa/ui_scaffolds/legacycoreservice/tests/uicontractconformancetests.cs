using LegacyCoreService.Services;
using Xunit;

namespace LegacyCoreService.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("frmaddinterest", UiScaffoldRegistry.FrmaddinterestDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmaddinterestDescriptorJson);
        Assert.Contains("/ui/frmaddinterest", UiScaffoldRegistry.RouteMap["/frmaddinterest"]);
        Assert.Contains("frmdep", UiScaffoldRegistry.FrmdepDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmdepDescriptorJson);
        Assert.Contains("/ui/frmdep", UiScaffoldRegistry.RouteMap["/frmdep"]);
        Assert.Contains("POST /legacycore/addinterest", UiScaffoldRegistry.FrmdepDescriptorJson);
        Assert.Contains("frminterest", UiScaffoldRegistry.FrminterestDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrminterestDescriptorJson);
        Assert.Contains("/ui/frminterest", UiScaffoldRegistry.RouteMap["/frminterest"]);
        Assert.Contains("POST /legacycore/addinterest", UiScaffoldRegistry.FrminterestDescriptorJson);
    }
}
