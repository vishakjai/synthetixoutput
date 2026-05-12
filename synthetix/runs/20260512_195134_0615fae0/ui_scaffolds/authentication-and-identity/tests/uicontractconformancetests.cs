using Authentication and Identity.Services;
using Xunit;

namespace Authentication and Identity.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("Authentication_and_Identity", UiScaffoldRegistry.AuthenticationAndIdentityDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.AuthenticationAndIdentityDescriptorJson);
        Assert.Contains("/ui/authentication-and-identity", UiScaffoldRegistry.RouteMap["/authentication_and_identity"]);
    }
}
