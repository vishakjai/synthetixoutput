namespace CustomerAndAccount.Services;

public static class UiScaffoldRegistry
{
    public const string CustomerAndAccountDescriptorJson = @"{
  ""screen_id"": ""Customer_and_Account"",
  ""screen_name"": ""Customer and Account workspace"",
  ""route"": ""/ui/customer-and-account"",
  ""state_model"": {
    ""screen_state_type"": ""CustomerAndAccountState"",
    ""field_ids"": [],
    ""action_ids"": []
  },
  ""validation_map"": [],
  ""event_wiring"": [],
  ""navigation_map"": {
    ""default_route"": ""/ui/customer-and-account"",
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
            ["/customer_and_account"] = "/ui/customer-and-account",
    };
}
