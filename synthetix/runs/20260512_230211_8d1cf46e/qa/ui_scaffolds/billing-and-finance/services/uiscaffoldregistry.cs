namespace BillingAndFinance.Services;

public static class UiScaffoldRegistry
{
    public const string BillingAndFinanceDescriptorJson = @"{
  ""screen_id"": ""Billing_and_Finance"",
  ""screen_name"": ""Billing and Finance workspace"",
  ""route"": ""/ui/billing-and-finance"",
  ""state_model"": {
    ""screen_state_type"": ""BillingAndFinanceState"",
    ""field_ids"": [],
    ""action_ids"": []
  },
  ""validation_map"": [],
  ""event_wiring"": [],
  ""navigation_map"": {
    ""default_route"": ""/ui/billing-and-finance"",
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
            ["/billing_and_finance"] = "/ui/billing-and-finance",
    };
}
