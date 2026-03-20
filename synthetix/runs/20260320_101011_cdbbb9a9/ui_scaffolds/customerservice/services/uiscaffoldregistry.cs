namespace CustomerService.Services;

public static class UiScaffoldRegistry
{
    public const string FrmcloseacountDescriptorJson = @"{
  ""screen_id"": ""frmcloseacount"",
  ""screen_name"": ""Account closure and settlement workflow"",
  ""route"": ""/ui/frmcloseacount"",
  ""state_model"": {
    ""screen_state_type"": ""FrmcloseacountState"",
    ""field_ids"": [
      ""txtdateofopen"",
      ""cbosex"",
      ""frame1"",
      ""frame2"",
      ""fracheque"",
      ""franominee"",
      ""frasearch"",
      ""lblcheque"",
      ""lbldateofopen"",
      ""lblnominee"",
      ""lblphoneno"",
      ""lblaccountno"",
      ""lbladdress"",
      ""lblbalance"",
      ""lblcustid"",
      ""lblcustomerid"",
      ""lbldateofbirth"",
      ""lblfirstname"",
      ""lbllastname"",
      ""lblmiddlename"",
      ""lblrelationship"",
      ""lblsex"",
      ""lbltype"",
      ""optmajor"",
      ""optminor"",
      ""optno"",
      ""optyes"",
      ""txtaccountno"",
      ""txtaddress"",
      ""txtbalance"",
      ""txtcustid"",
      ""txtcustomerid"",
      ""txtdob"",
      ""txtfirstname"",
      ""txtlastname"",
      ""txtmiddlename"",
      ""txtmobileno"",
      ""txtnominee"",
      ""txtphoneno"",
      ""txtpincode"",
      ""txtrelationship""
    ],
    ""action_ids"": [
      ""action_save"",
      ""action_cancel""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""lblaccountno"",
      ""rule_ids"": [
        ""VR-lblaccountno-REQUIRED"",
        ""VR-lblaccountno-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lblbalance"",
      ""rule_ids"": [
        ""VR-lblbalance-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lblcustid"",
      ""rule_ids"": [
        ""VR-lblcustid-NUMERIC""
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
      ""field_id"": ""lbllastname"",
      ""rule_ids"": [
        ""VR-lbllastname-REQUIRED""
      ]
    },
    {
      ""field_id"": ""lblmiddlename"",
      ""rule_ids"": [
        ""VR-lblmiddlename-REQUIRED"",
        ""VR-lblmiddlename-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtaccountno"",
      ""rule_ids"": [
        ""VR-txtaccountno-REQUIRED"",
        ""VR-txtaccountno-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtbalance"",
      ""rule_ids"": [
        ""VR-txtbalance-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtcustid"",
      ""rule_ids"": [
        ""VR-txtcustid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtcustomerid"",
      ""rule_ids"": [
        ""VR-txtcustomerid-REQUIRED"",
        ""VR-txtcustomerid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtfirstname"",
      ""rule_ids"": [
        ""VR-txtfirstname-REQUIRED""
      ]
    },
    {
      ""field_id"": ""txtlastname"",
      ""rule_ids"": [
        ""VR-txtlastname-REQUIRED""
      ]
    },
    {
      ""field_id"": ""txtmiddlename"",
      ""rule_ids"": [
        ""VR-txtmiddlename-REQUIRED"",
        ""VR-txtmiddlename-NUMERIC""
      ]
    }
  ],
  ""event_wiring"": [
    {
      ""id"": ""evt_save"",
      ""name"": ""Save Screen Data"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""PUT /customer/closeacount"",
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
    ""default_route"": ""/ui/frmcloseacount"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmcloseacount_list"",
        ""route"": ""/ui/frmcloseacount"",
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
        ""PUT /customer/closeacount""
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
    public const string FrmcustomerDescriptorJson = @"{
  ""screen_id"": ""frmcustomer"",
  ""screen_name"": ""Customer profile onboarding and maintenance workflow"",
  ""route"": ""/ui/frmcustomer"",
  ""state_model"": {
    ""screen_state_type"": ""FrmcustomerState"",
    ""field_ids"": [
      ""txtdateofopen"",
      ""txtdob"",
      ""cbosex"",
      ""frame1"",
      ""frame2"",
      ""fracheque"",
      ""franominee"",
      ""frasearch"",
      ""label2"",
      ""label3"",
      ""lblcheque"",
      ""lbldateofopen"",
      ""lblnominee"",
      ""lblphoneno"",
      ""lblpincode"",
      ""lbladdress"",
      ""lblbalance"",
      ""lbldateofbirth"",
      ""lblmiddlename"",
      ""lblrelationship"",
      ""lblsex"",
      ""lbltype"",
      ""lbltypeofaccount"",
      ""optmajor"",
      ""optminor"",
      ""optno"",
      ""optyes"",
      ""txtaccountno"",
      ""txtaddress"",
      ""txtbalance"",
      ""txtcustomerid"",
      ""txtfirstname"",
      ""txtlastname"",
      ""txtmiddlename"",
      ""txtmobileno"",
      ""txtnominee"",
      ""txtphoneno"",
      ""txtpincode"",
      ""txtrelationship"",
      ""txtsearch""
    ],
    ""action_ids"": [
      ""action_save"",
      ""action_cancel""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""lblbalance"",
      ""rule_ids"": [
        ""VR-lblbalance-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lblmiddlename"",
      ""rule_ids"": [
        ""VR-lblmiddlename-REQUIRED"",
        ""VR-lblmiddlename-NUMERIC""
      ]
    },
    {
      ""field_id"": ""lbltypeofaccount"",
      ""rule_ids"": [
        ""VR-lbltypeofaccount-REQUIRED"",
        ""VR-lbltypeofaccount-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtaccountno"",
      ""rule_ids"": [
        ""VR-txtaccountno-REQUIRED"",
        ""VR-txtaccountno-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtbalance"",
      ""rule_ids"": [
        ""VR-txtbalance-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtcustomerid"",
      ""rule_ids"": [
        ""VR-txtcustomerid-REQUIRED"",
        ""VR-txtcustomerid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtfirstname"",
      ""rule_ids"": [
        ""VR-txtfirstname-REQUIRED""
      ]
    },
    {
      ""field_id"": ""txtlastname"",
      ""rule_ids"": [
        ""VR-txtlastname-REQUIRED""
      ]
    },
    {
      ""field_id"": ""txtmiddlename"",
      ""rule_ids"": [
        ""VR-txtmiddlename-REQUIRED"",
        ""VR-txtmiddlename-NUMERIC""
      ]
    }
  ],
  ""event_wiring"": [
    {
      ""id"": ""evt_save"",
      ""name"": ""Save Screen Data"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""PUT /customer/closeacount"",
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
    ""default_route"": ""/ui/frmcustomer"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmcustomer_list"",
        ""route"": ""/ui/frmcustomer"",
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
        ""PUT /customer/closeacount""
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
    public const string FrmsettingsDescriptorJson = @"{
  ""screen_id"": ""frmsettings"",
  ""screen_name"": ""Account type maintenance and account setup workflow"",
  ""route"": ""/ui/frmsettings"",
  ""state_model"": {
    ""screen_state_type"": ""FrmsettingsState"",
    ""field_ids"": [
      ""frasettings"",
      ""label1"",
      ""lblfieldlabel"",
      ""lblaccountid"",
      ""txtaccountid"",
      ""txtaccounttype"",
      ""txtcheque"",
      ""txtinterestrate"",
      ""txtnocheque""
    ],
    ""action_ids"": [
      ""action_cancel"",
      ""action_save""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""lblaccountid"",
      ""rule_ids"": [
        ""VR-lblaccountid-REQUIRED"",
        ""VR-lblaccountid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtaccountid"",
      ""rule_ids"": [
        ""VR-txtaccountid-REQUIRED"",
        ""VR-txtaccountid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtaccounttype"",
      ""rule_ids"": [
        ""VR-txtaccounttype-REQUIRED"",
        ""VR-txtaccounttype-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtinterestrate"",
      ""rule_ids"": [
        ""VR-txtinterestrate-NUMERIC""
      ]
    }
  ],
  ""event_wiring"": [
    {
      ""id"": ""evt_save"",
      ""name"": ""Save Screen Data"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""PUT /customer/closeacount"",
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
    ""default_route"": ""/ui/frmsettings"",
    ""opens"": [
      {
        ""screen_id"": ""previous"",
        ""route"": """",
        ""trigger_event_id"": ""evt_cancel"",
        ""mode"": ""same_tab""
      },
      {
        ""screen_id"": ""ui_frmsettings_list"",
        ""route"": ""/ui/frmsettings"",
        ""trigger_event_id"": ""evt_save"",
        ""mode"": ""same_tab""
      }
    ]
  },
  ""api_binding_map"": [
    {
      ""event_id"": ""evt_save"",
      ""targets"": [
        ""PUT /customer/closeacount""
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
            ["/frmcloseacount"] = "/ui/frmcloseacount",
            ["/frmcustomer"] = "/ui/frmcustomer",
            ["/frmsettings"] = "/ui/frmsettings",
    };
}
