namespace LegacyCoreService.Services;

public static class UiScaffoldRegistry
{
    public const string FrmaddinterestDescriptorJson = @"{
  ""screen_id"": ""frmaddinterest"",
  ""screen_name"": ""Interest calculation and posting workflow"",
  ""route"": ""/ui/frmaddinterest"",
  ""state_model"": {
    ""screen_state_type"": ""FrmaddinterestState"",
    ""field_ids"": [
      ""txtdate"",
      ""cbomonth"",
      ""cboyear"",
      ""frame1"",
      ""frame2"",
      ""fra"",
      ""label1"",
      ""label5"",
      ""lblamount"",
      ""lblbal"",
      ""lblbalance"",
      ""lblcurrentbalance"",
      ""lblcustomerid"",
      ""lblfirst"",
      ""lblfirstname"",
      ""lblid"",
      ""lblinterest"",
      ""lbllast"",
      ""lbllastname"",
      ""lbltransaction"",
      ""lbltransactionid"",
      ""lbltype"",
      ""txtaccountno"",
      ""txtcurrentdate""
    ],
    ""action_ids"": [
      ""action_cancel""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""lblamount"",
      ""rule_ids"": [
        ""VR-lblamount-REQUIRED"",
        ""VR-lblamount-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lblbalance"",
      ""rule_ids"": [
        ""VR-lblbalance-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lblcurrentbalance"",
      ""rule_ids"": [
        ""VR-lblcurrentbalance-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lblcustomerid"",
      ""rule_ids"": [
        ""VR-lblcustomerid-REQUIRED"",
        ""VR-lblcustomerid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lblfirstname"",
      ""rule_ids"": [
        ""VR-lblfirstname-REQUIRED""
      ]
    },
    {
      ""field_id"": ""lblid"",
      ""rule_ids"": [
        ""VR-lblid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lbllastname"",
      ""rule_ids"": [
        ""VR-lbllastname-REQUIRED""
      ]
    },
    {
      ""field_id"": ""lbltransactionid"",
      ""rule_ids"": [
        ""VR-lbltransactionid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtaccountno"",
      ""rule_ids"": [
        ""VR-txtaccountno-REQUIRED"",
        ""VR-txtaccountno-NUMERIC""
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
    ""default_route"": ""/ui/frmaddinterest"",
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
    public const string FrmdepDescriptorJson = @"{
  ""screen_id"": ""frmdep"",
  ""screen_name"": ""Business workflow executed through event-driven UI controls"",
  ""route"": ""/ui/frmdep"",
  ""state_model"": {
    ""screen_state_type"": ""FrmdepState"",
    ""field_ids"": [],
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
        ""POST /legacycore/addinterest"",
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
    ""default_route"": ""/ui/frmdep"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmdep_list"",
        ""route"": ""/ui/frmdep"",
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
        ""POST /legacycore/addinterest""
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
    ""tab_order_defined"": false
  },
  ""annotations"": [
    ""UI_SCAFFOLD"",
    ""VISUAL_DESIGN_OUT_OF_SCOPE""
  ]
}";
    public const string FrminterestDescriptorJson = @"{
  ""screen_id"": ""frminterest"",
  ""screen_name"": ""Interest calculation and posting workflow"",
  ""route"": ""/ui/frminterest"",
  ""state_model"": {
    ""screen_state_type"": ""FrminterestState"",
    ""field_ids"": [
      ""listview1""
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
        ""POST /legacycore/addinterest"",
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
    ""default_route"": ""/ui/frminterest"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frminterest_list"",
        ""route"": ""/ui/frminterest"",
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
        ""POST /legacycore/addinterest""
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
            ["/frmaddinterest"] = "/ui/frmaddinterest",
            ["/frmdep"] = "/ui/frmdep",
            ["/frminterest"] = "/ui/frminterest",
    };
}
