namespace OnboardingAndCredentialing.Services;

public static class UiScaffoldRegistry
{
    public const string OnboardingAndCredentialingDescriptorJson = @"{
  ""screen_id"": ""Onboarding_and_Credentialing"",
  ""screen_name"": ""Onboarding and Credentialing workspace"",
  ""route"": ""/ui/onboarding-and-credentialing"",
  ""state_model"": {
    ""screen_state_type"": ""OnboardingAndCredentialingState"",
    ""field_ids"": [],
    ""action_ids"": []
  },
  ""validation_map"": [],
  ""event_wiring"": [],
  ""navigation_map"": {
    ""default_route"": ""/ui/onboarding-and-credentialing"",
    ""opens"": []
  },
  ""api_binding_map"": [],
  ""accessibility_hooks"": {
    ""keyboard_navigation"": true,
    ""label_association"": true,
    ""tab_order_defined"": false
  },
  ""annotations"": [
    ""UI_SCAFFOLD"",
    ""VISUAL_DESIGN_OUT_OF_SCOPE""
  ]
}";

    public static readonly Dictionary<string, string> RouteMap = new(StringComparer.OrdinalIgnoreCase)
    {
            ["/onboarding_and_credentialing"] = "/ui/onboarding-and-credentialing",
    };
}
