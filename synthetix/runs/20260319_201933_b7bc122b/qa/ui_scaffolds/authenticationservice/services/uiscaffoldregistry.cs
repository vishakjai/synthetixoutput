namespace AuthenticationService.Services;

public static class UiScaffoldRegistry
{
    public const string FrmLogin1DescriptorJson = @"{
  ""screen_id"": ""frmLogin1"",
  ""screen_name"": ""Authentication and credential validation workflow"",
  ""route"": ""/ui/frmlogin1"",
  ""state_model"": {
    ""screen_state_type"": ""FrmLogin1State"",
    ""field_ids"": [
      ""lbllabels"",
      ""txtpass"",
      ""txtun""
    ],
    ""action_ids"": [
      ""action_save"",
      ""action_cancel""
    ]
  },
  ""validation_map"": [],
  ""event_wiring"": [
    {
      ""id"": ""evt_save"",
      ""name"": ""Save Screen Data"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""POST /auth/login"",
        ""list""
      ]
    },
    {
      ""id"": ""evt_cancel"",
      ""name"": ""Cancel Screen"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""previous""
      ]
    }
  ],
  ""navigation_map"": {
    ""default_route"": ""/ui/frmlogin1"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmlogin1_list"",
        ""route"": ""/ui/frmlogin1"",
        ""trigger_event_id"": ""evt_save"",
        ""mode"": ""same_tab""
      },
      {
        ""screen_id"": ""previous"",
        ""route"": """",
        ""trigger_event_id"": ""evt_cancel"",
        ""mode"": ""same_tab""
      }
    ]
  },
  ""api_binding_map"": [
    {
      ""event_id"": ""evt_save"",
      ""targets"": [
        ""POST /auth/login""
      ]
    },
    {
      ""event_id"": ""evt_cancel"",
      ""targets"": []
    }
  ],
  ""accessibility_hooks"": {
    ""keyboard_navigation"": true,
    ""label_association"": true,
    ""tab_order_defined"": true
  },
  ""annotations"": [
    ""UI_SCAFFOLD"",
    ""VISUAL_DESIGN_OUT_OF_SCOPE""
  ]
}";

    public static readonly Dictionary<string, string> RouteMap = new(StringComparer.OrdinalIgnoreCase)
    {
            ["/frmlogin1"] = "/ui/frmlogin1",
    };
}
