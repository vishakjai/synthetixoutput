using Staffing and Placement.Services;
using Xunit;

namespace StaffingAndPlacement.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("Staffing_and_Placement", UiScaffoldRegistry.StaffingAndPlacementDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.StaffingAndPlacementDescriptorJson);
        Assert.Contains("/ui/staffing-and-placement", UiScaffoldRegistry.RouteMap["/staffing_and_placement"]);
    }
}
