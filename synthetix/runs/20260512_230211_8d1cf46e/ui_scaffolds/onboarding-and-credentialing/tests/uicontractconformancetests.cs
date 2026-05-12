using Onboarding and Credentialing.Services;
using Xunit;

namespace OnboardingAndCredentialing.Tests;

public sealed class UiContractConformanceTests
{
    [Fact]
    public void Screen_contract_descriptors_include_ui_scaffold_annotations()
    {
        Assert.Contains("Onboarding_and_Credentialing", UiScaffoldRegistry.OnboardingAndCredentialingDescriptorJson);
        Assert.Contains("UI_SCAFFOLD", UiScaffoldRegistry.OnboardingAndCredentialingDescriptorJson);
        Assert.Contains("/ui/onboarding-and-credentialing", UiScaffoldRegistry.RouteMap["/onboarding_and_credentialing"]);
    }
}
