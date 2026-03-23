using ReportingService.Services;
using Xunit;

namespace ReportingService.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("frmWithinDate", UiScaffoldRegistry.FrmWithinDateDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmWithinDateDescriptorJson);
        Assert.Contains("/ui/frmwithindate", UiScaffoldRegistry.RouteMap["/frmwithindate"]);
        Assert.Contains("frmdaily", UiScaffoldRegistry.FrmdailyDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmdailyDescriptorJson);
        Assert.Contains("/ui/frmdaily", UiScaffoldRegistry.RouteMap["/frmdaily"]);
        Assert.Contains("frmmonthlyreport", UiScaffoldRegistry.FrmmonthlyreportDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmmonthlyreportDescriptorJson);
        Assert.Contains("/ui/frmmonthlyreport", UiScaffoldRegistry.RouteMap["/frmmonthlyreport"]);
        Assert.Contains("frmstatement", UiScaffoldRegistry.FrmstatementDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmstatementDescriptorJson);
        Assert.Contains("/ui/frmstatement", UiScaffoldRegistry.RouteMap["/frmstatement"]);
        Assert.Contains("Form1", UiScaffoldRegistry.Form1DescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.Form1DescriptorJson);
        Assert.Contains("/ui/form1", UiScaffoldRegistry.RouteMap["/form1"]);
        Assert.Contains("POST /reporting/expireitemswithindate", UiScaffoldRegistry.Form1DescriptorJson);
        Assert.Contains("frmExpireItemsWithinDate", UiScaffoldRegistry.FrmExpireItemsWithinDateDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmExpireItemsWithinDateDescriptorJson);
        Assert.Contains("/ui/frmexpireitemswithindate", UiScaffoldRegistry.RouteMap["/frmexpireitemswithindate"]);
        Assert.Contains("POST /reporting/expireitemswithindate", UiScaffoldRegistry.FrmExpireItemsWithinDateDescriptorJson);
        Assert.Contains("frmMonthly", UiScaffoldRegistry.FrmMonthlyDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmMonthlyDescriptorJson);
        Assert.Contains("/ui/frmmonthly", UiScaffoldRegistry.RouteMap["/frmmonthly"]);
        Assert.Contains("POST /reporting/expireitemswithindate", UiScaffoldRegistry.FrmMonthlyDescriptorJson);
        Assert.Contains("frmreport", UiScaffoldRegistry.FrmreportDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmreportDescriptorJson);
        Assert.Contains("/ui/frmreport", UiScaffoldRegistry.RouteMap["/frmreport"]);
        Assert.Contains("frmtransaction", UiScaffoldRegistry.FrmtransactionDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmtransactionDescriptorJson);
        Assert.Contains("/ui/frmtransaction", UiScaffoldRegistry.RouteMap["/frmtransaction"]);
        Assert.Contains("POST /reporting/expireitemswithindate", UiScaffoldRegistry.FrmtransactionDescriptorJson);
        Assert.Contains("frmwith", UiScaffoldRegistry.FrmwithDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.FrmwithDescriptorJson);
        Assert.Contains("/ui/frmwith", UiScaffoldRegistry.RouteMap["/frmwith"]);
        Assert.Contains("POST /reporting/expireitemswithindate", UiScaffoldRegistry.FrmwithDescriptorJson);
    }
}
