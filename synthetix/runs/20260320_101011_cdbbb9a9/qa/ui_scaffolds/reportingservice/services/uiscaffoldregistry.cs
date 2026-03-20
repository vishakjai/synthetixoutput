namespace ReportingService.Services;

public static class UiScaffoldRegistry
{
    public const string FrmWithinDateDescriptorJson = @"{
  ""screen_id"": ""frmWithinDate"",
  ""screen_name"": ""Business workflow executed through event-driven UI controls"",
  ""route"": ""/ui/frmwithindate"",
  ""state_model"": {
    ""screen_state_type"": ""FrmWithinDateState"",
    ""field_ids"": [
      ""dtfrom"",
      ""dtto"",
      ""frame1"",
      ""frame2"",
      ""frawithindate"",
      ""label1"",
      ""label2""
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
    ""default_route"": ""/ui/frmwithindate"",
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
    public const string FrmdailyDescriptorJson = @"{
  ""screen_id"": ""frmdaily"",
  ""screen_name"": ""Business workflow executed through event-driven UI controls"",
  ""route"": ""/ui/frmdaily"",
  ""state_model"": {
    ""screen_state_type"": ""FrmdailyState"",
    ""field_ids"": [
      ""txtdaily"",
      ""fradaily""
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
    ""default_route"": ""/ui/frmdaily"",
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
    public const string FrmmonthlyreportDescriptorJson = @"{
  ""screen_id"": ""frmmonthlyreport"",
  ""screen_name"": ""Operational reporting and statement generation workflow"",
  ""route"": ""/ui/frmmonthlyreport"",
  ""state_model"": {
    ""screen_state_type"": ""FrmmonthlyreportState"",
    ""field_ids"": [
      ""dtpfrom"",
      ""dtpto"",
      ""cmbcustomerid"",
      ""frame1"",
      ""label1"",
      ""lblfrom"",
      ""lblto""
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
    ""default_route"": ""/ui/frmmonthlyreport"",
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
    public const string FrmstatementDescriptorJson = @"{
  ""screen_id"": ""frmstatement"",
  ""screen_name"": ""Operational reporting and statement generation workflow"",
  ""route"": ""/ui/frmstatement"",
  ""state_model"": {
    ""screen_state_type"": ""FrmstatementState"",
    ""field_ids"": [],
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
    ""default_route"": ""/ui/frmstatement"",
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
    ""tab_order_defined"": false
  },
  ""annotations"": [
    ""UI_SCAFFOLD"",
    ""VISUAL_DESIGN_OUT_OF_SCOPE""
  ]
}";
    public const string Form1DescriptorJson = @"{
  ""screen_id"": ""Form1"",
  ""screen_name"": ""Business workflow executed through event-driven UI controls"",
  ""route"": ""/ui/form1"",
  ""state_model"": {
    ""screen_state_type"": ""Form1State"",
    ""field_ids"": [
      ""dtpicker1"",
      ""dtpicker2"",
      ""frame1"",
      ""frame2"",
      ""label1"",
      ""label2"",
      ""shape5""
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
        ""POST /reporting/expireitemswithindate"",
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
    ""default_route"": ""/ui/form1"",
    ""opens"": [
      {
        ""screen_id"": ""ui_form1_list"",
        ""route"": ""/ui/form1"",
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
        ""POST /reporting/expireitemswithindate""
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
    public const string FrmExpireItemsWithinDateDescriptorJson = @"{
  ""screen_id"": ""frmExpireItemsWithinDate"",
  ""screen_name"": ""Business workflow executed through event-driven UI controls"",
  ""route"": ""/ui/frmexpireitemswithindate"",
  ""state_model"": {
    ""screen_state_type"": ""FrmExpireItemsWithinDateState"",
    ""field_ids"": [
      ""dtfrom"",
      ""dtto"",
      ""frame1"",
      ""frame2"",
      ""label1"",
      ""label2""
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
        ""POST /reporting/expireitemswithindate"",
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
    ""default_route"": ""/ui/frmexpireitemswithindate"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmexpireitemswithindate_list"",
        ""route"": ""/ui/frmexpireitemswithindate"",
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
        ""POST /reporting/expireitemswithindate""
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
    public const string FrmMonthlyDescriptorJson = @"{
  ""screen_id"": ""frmMonthly"",
  ""screen_name"": ""Operational reporting and statement generation workflow"",
  ""route"": ""/ui/frmmonthly"",
  ""state_model"": {
    ""screen_state_type"": ""FrmMonthlyState"",
    ""field_ids"": [
      ""cmbreport""
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
        ""POST /reporting/expireitemswithindate"",
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
    ""default_route"": ""/ui/frmmonthly"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmmonthly_list"",
        ""route"": ""/ui/frmmonthly"",
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
        ""POST /reporting/expireitemswithindate""
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
    public const string FrmreportDescriptorJson = @"{
  ""screen_id"": ""frmreport"",
  ""screen_name"": ""Operational reporting and statement generation workflow"",
  ""route"": ""/ui/frmreport"",
  ""state_model"": {
    ""screen_state_type"": ""FrmreportState"",
    ""field_ids"": [
      ""dtpfromdate"",
      ""dtptodate"",
      ""frame3"",
      ""frame4"",
      ""frame5"",
      ""frame7"",
      ""frareport"",
      ""frasearch"",
      ""label1"",
      ""label2"",
      ""label5"",
      ""label6"",
      ""label7"",
      ""label8"",
      ""lblcustomerid"",
      ""txtbalance"",
      ""txtfirstname"",
      ""txtlastname"",
      ""txtaccount"",
      ""txtaccountno"",
      ""txtcustomerid"",
      ""txttypeofaccount""
    ],
    ""action_ids"": [
      ""action_cancel""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""lblcustomerid"",
      ""rule_ids"": [
        ""VR-lblcustomerid-REQUIRED"",
        ""VR-lblcustomerid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txtbalance"",
      ""rule_ids"": [
        ""VR-txtbalance-NUMERIC""
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
      ""field_id"": ""txtaccount"",
      ""rule_ids"": [
        ""VR-txtaccount-REQUIRED"",
        ""VR-txtaccount-NUMERIC""
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
      ""field_id"": ""txtcustomerid"",
      ""rule_ids"": [
        ""VR-txtcustomerid-REQUIRED"",
        ""VR-txtcustomerid-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txttypeofaccount"",
      ""rule_ids"": [
        ""VR-txttypeofaccount-REQUIRED"",
        ""VR-txttypeofaccount-NUMERIC""
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
    ""default_route"": ""/ui/frmreport"",
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
    public const string FrmtransactionDescriptorJson = @"{
  ""screen_id"": ""frmtransaction"",
  ""screen_name"": ""Transaction ledger management and adjustment workflow"",
  ""route"": ""/ui/frmtransaction"",
  ""state_model"": {
    ""screen_state_type"": ""FrmtransactionState"",
    ""field_ids"": [
      ""lvwtransactions"",
      ""cboaccno"",
      ""frame1"",
      ""frame3"",
      ""fraaccountno"",
      ""label2"",
      ""label5"",
      ""option1"",
      ""option2""
    ],
    ""action_ids"": [
      ""action_save""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""fraaccountno"",
      ""rule_ids"": [
        ""VR-fraaccountno-REQUIRED"",
        ""VR-fraaccountno-NUMERIC""
      ]
    }
  ],
  ""event_wiring"": [
    {
      ""id"": ""evt_save"",
      ""name"": ""Save Screen Data"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""POST /reporting/expireitemswithindate"",
        ""list""
      ]
    }
  ],
  ""navigation_map"": {
    ""default_route"": ""/ui/frmtransaction"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmtransaction_list"",
        ""route"": ""/ui/frmtransaction"",
        ""trigger_event_id"": ""evt_save"",
        ""mode"": ""same_tab""
      }
    ]
  },
  ""api_binding_map"": [
    {
      ""event_id"": ""evt_save"",
      ""targets"": [
        ""POST /reporting/expireitemswithindate""
      ]
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
    public const string FrmwithDescriptorJson = @"{
  ""screen_id"": ""frmwith"",
  ""screen_name"": ""Transaction ledger management and adjustment workflow"",
  ""route"": ""/ui/frmwith"",
  ""state_model"": {
    ""screen_state_type"": ""FrmwithState"",
    ""field_ids"": [],
    ""action_ids"": [
      ""action_save""
    ]
  },
  ""validation_map"": [],
  ""event_wiring"": [
    {
      ""id"": ""evt_save"",
      ""name"": ""Save Screen Data"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""POST /reporting/expireitemswithindate"",
        ""list""
      ]
    }
  ],
  ""navigation_map"": {
    ""default_route"": ""/ui/frmwith"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmwith_list"",
        ""route"": ""/ui/frmwith"",
        ""trigger_event_id"": ""evt_save"",
        ""mode"": ""same_tab""
      }
    ]
  },
  ""api_binding_map"": [
    {
      ""event_id"": ""evt_save"",
      ""targets"": [
        ""POST /reporting/expireitemswithindate""
      ]
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

    public static readonly Dictionary<string, string> RouteMap = new(StringComparer.OrdinalIgnoreCase)
    {
            ["/frmwithindate"] = "/ui/frmwithindate",
            ["/frmdaily"] = "/ui/frmdaily",
            ["/frmmonthlyreport"] = "/ui/frmmonthlyreport",
            ["/frmstatement"] = "/ui/frmstatement",
            ["/form1"] = "/ui/form1",
            ["/frmexpireitemswithindate"] = "/ui/frmexpireitemswithindate",
            ["/frmmonthly"] = "/ui/frmmonthly",
            ["/frmreport"] = "/ui/frmreport",
            ["/frmtransaction"] = "/ui/frmtransaction",
            ["/frmwith"] = "/ui/frmwith",
    };
}
