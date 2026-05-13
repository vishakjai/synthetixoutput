namespace AuthenticationAndIdentity.Services;

public static class UiScaffoldRegistry
{
    public const string AuthenticationAndIdentityDescriptorJson = @"{
  ""screen_id"": ""Authentication_and_Identity"",
  ""screen_name"": ""Authentication and Identity workspace"",
  ""route"": ""/ui/authentication-and-identity"",
  ""state_model"": {
    ""screen_state_type"": ""AuthenticationAndIdentityState"",
    ""field_ids"": [],
    ""action_ids"": []
  },
  ""validation_map"": [],
  ""event_wiring"": [],
  ""navigation_map"": {
    ""default_route"": ""/ui/authentication-and-identity"",
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
            ["/authentication_and_identity"] = "/ui/authentication-and-identity",
    };
}
