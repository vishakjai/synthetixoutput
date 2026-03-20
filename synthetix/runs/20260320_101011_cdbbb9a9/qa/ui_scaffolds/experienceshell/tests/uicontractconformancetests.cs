using ExperienceShell.Services;
using Xunit;

namespace ExperienceShell.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("frmSplash", UiScaffoldRegistry.FrmSplashDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmSplashDescriptorJson);
        Assert.Contains("/ui/frmsplash", UiScaffoldRegistry.RouteMap["/frmsplash"]);
        Assert.Contains("menu", UiScaffoldRegistry.MenuDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.MenuDescriptorJson);
        Assert.Contains("/ui/menu", UiScaffoldRegistry.RouteMap["/menu"]);
        Assert.Contains("Mdi", UiScaffoldRegistry.MdiDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.MdiDescriptorJson);
        Assert.Contains("/ui/mdi", UiScaffoldRegistry.RouteMap["/mdi"]);
    }
}
