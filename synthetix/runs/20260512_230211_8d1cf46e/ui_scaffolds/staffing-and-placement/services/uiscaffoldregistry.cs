namespace StaffingAndPlacement.Services;

public static class UiScaffoldRegistry
{
    public const string StaffingAndPlacementDescriptorJson = @"{
  ""screen_id"": ""Staffing_and_Placement"",
  ""screen_name"": ""Staffing and Placement workspace"",
  ""route"": ""/ui/staffing-and-placement"",
  ""state_model"": {
    ""screen_state_type"": ""StaffingAndPlacementState"",
    ""field_ids"": [],
    ""action_ids"": []
  },
  ""validation_map"": [],
  ""event_wiring"": [],
  ""navigation_map"": {
    ""default_route"": ""/ui/staffing-and-placement"",
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
            ["/staffing_and_placement"] = "/ui/staffing-and-placement",
    };
}
