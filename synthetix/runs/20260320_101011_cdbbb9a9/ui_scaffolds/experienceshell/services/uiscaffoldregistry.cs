namespace ExperienceShell.Services;

public static class UiScaffoldRegistry
{
    public const string FrmSplashDescriptorJson = @"{
  ""screen_id"": ""frmSplash"",
  ""screen_name"": ""Application startup and splash workflow"",
  ""route"": ""/ui/frmsplash"",
  ""state_model"": {
    ""screen_state_type"": ""FrmSplashState"",
    ""field_ids"": [
      ""progressbar"",
      ""progressbar1"",
      ""frasplash"",
      ""image1"",
      ""lblcompany"",
      ""lblcompanyproduct"",
      ""lblcopyright"",
      ""lbllicenseto"",
      ""lblwarning"",
      ""lbldisplay"",
      ""timer1""
    ],
    ""action_ids"": [
      ""action_cancel""
    ]
  },
  ""validation_map"": [],
  ""event_wiring"": [
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
    ""default_route"": ""/ui/frmsplash"",
    ""opens"": [
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
    public const string MenuDescriptorJson = @"{
  ""screen_id"": ""menu"",
  ""screen_name"": ""Application navigation and module routing workflow"",
  ""route"": ""/ui/menu"",
  ""state_model"": {
    ""screen_state_type"": ""MenuState"",
    ""field_ids"": [
      ""mnudepositamount"",
      ""mnuexit"",
      ""mnureports"",
      ""mnuwithdrawamount"",
      ""mnubetween"",
      ""mnuclose"",
      ""mnucustomerdetails"",
      ""mnucustomermonthly"",
      ""mnugiveinterest"",
      ""mnumaster"",
      ""mnusettings"",
      ""mnutransaction"",
      ""mnutransactions""
    ],
    ""action_ids"": [
      ""action_cancel""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""mnudepositamount"",
      ""rule_ids"": [
        ""VR-mnudepositamount-REQUIRED"",
        ""VR-mnudepositamount-NUMERIC""
      ]
    },
    {
      ""field_id"": ""mnuwithdrawamount"",
      ""rule_ids"": [
        ""VR-mnuwithdrawamount-REQUIRED"",
        ""VR-mnuwithdrawamount-NUMERIC""
      ]
    },
    {
      ""field_id"": ""mnucustomerdetails"",
      ""rule_ids"": [
        ""VR-mnucustomerdetails-REQUIRED""
      ]
    },
    {
      ""field_id"": ""mnucustomermonthly"",
      ""rule_ids"": [
        ""VR-mnucustomermonthly-REQUIRED""
      ]
    }
  ],
  ""event_wiring"": [
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
    ""default_route"": ""/ui/menu"",
    ""opens"": [
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
    public const string MdiDescriptorJson = @"{
  ""screen_id"": ""Mdi"",
  ""screen_name"": ""Application navigation and module routing workflow"",
  ""route"": ""/ui/mdi"",
  ""state_model"": {
    ""screen_state_type"": ""MdiState"",
    ""field_ids"": [
      ""mnudepositamount"",
      ""mnuexit"",
      ""mnureports"",
      ""mnuwithdrawamount"",
      ""mnuaddinterest"",
      ""mnuclose"",
      ""mnucustomerdetails"",
      ""mnuinterest"",
      ""mnumaster"",
      ""mnumonthly"",
      ""mnustatement"",
      ""mnutransaction"",
      ""mnutransactions"",
      ""mnuviewtransaction""
    ],
    ""action_ids"": [
      ""action_cancel""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""mnudepositamount"",
      ""rule_ids"": [
        ""VR-mnudepositamount-REQUIRED"",
        ""VR-mnudepositamount-NUMERIC""
      ]
    },
    {
      ""field_id"": ""mnuwithdrawamount"",
      ""rule_ids"": [
        ""VR-mnuwithdrawamount-REQUIRED"",
        ""VR-mnuwithdrawamount-NUMERIC""
      ]
    },
    {
      ""field_id"": ""mnucustomerdetails"",
      ""rule_ids"": [
        ""VR-mnucustomerdetails-REQUIRED""
      ]
    }
  ],
  ""event_wiring"": [
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
    ""default_route"": ""/ui/mdi"",
    ""opens"": [
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
            ["/frmsplash"] = "/ui/frmsplash",
            ["/menu"] = "/ui/menu",
            ["/mdi"] = "/ui/mdi",
    };
}
