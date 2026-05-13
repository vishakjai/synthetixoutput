namespace ReportingAndAnalytics.Services;

public static class UiScaffoldRegistry
{
    public const string ReportingAndAnalyticsDescriptorJson = @"{
  ""screen_id"": ""Reporting_and_Analytics"",
  ""screen_name"": ""Reporting and Analytics workspace"",
  ""route"": ""/ui/reporting-and-analytics"",
  ""state_model"": {
    ""screen_state_type"": ""ReportingAndAnalyticsState"",
    ""field_ids"": [],
    ""action_ids"": []
  },
  ""validation_map"": [],
  ""event_wiring"": [],
  ""navigation_map"": {
    ""default_route"": ""/ui/reporting-and-analytics"",
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
            ["/reporting_and_analytics"] = "/ui/reporting-and-analytics",
    };
}
