using AuthenticationService.Services;
using Xunit;

namespace AuthenticationService.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("frmLogin1", UiScaffoldRegistry.FrmLogin1DescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmLogin1DescriptorJson);
        Assert.Contains("/ui/frmlogin1", UiScaffoldRegistry.RouteMap["/frmlogin1"]);
        Assert.Contains("POST /auth/login", UiScaffoldRegistry.FrmLogin1DescriptorJson);
    }
}
