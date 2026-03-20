namespace TransactionService.Services;

public static class UiScaffoldRegistry
{
    public const string FrmdepositDescriptorJson = @"{
  ""screen_id"": ""frmdeposit"",
  ""screen_name"": ""Deposit capture and balance posting workflow"",
  ""route"": ""/ui/frmdeposit"",
  ""state_model"": {
    ""screen_state_type"": ""FrmdepositState"",
    ""field_ids"": [
      ""txtdateoftransaction"",
      ""frame1"",
      ""frame2"",
      ""frame3"",
      ""fracheque"",
      ""fraext"",
      ""framode"",
      ""label2"",
      ""lblfieldlabel"",
      ""lblaccount"",
      ""lblbalance"",
      ""lblbankname"",
      ""lblchequeissued"",
      ""lblcustomer"",
      ""lblcustomerid"",
      ""lbldate"",
      ""lblfirst"",
      ""lblfirstname"",
      ""lbllast"",
      ""lbllastname"",
      ""lbltypeofaccount"",
      ""optcash"",
      ""optcheque"",
      ""optno"",
      ""optyes"",
      ""txtbankname"",
      ""txtchequeno"",
      ""txtsearchaccountno""
    ],
    ""action_ids"": [
      ""action_save"",
      ""action_cancel""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""txtbankname"",
      ""rule_ids"": [
        ""VR-txtbankname-REQUIRED""
      ]
    },
    {
      ""field_id"": ""txtsearchaccountno"",
      ""rule_ids"": [
        ""VR-txtsearchaccountno-REQUIRED"",
        ""VR-txtsearchaccountno-NUMERIC""
      ]
    }
  ],
  ""event_wiring"": [
    {
      ""id"": ""evt_save"",
      ""name"": ""Save Screen Data"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""POST /transactions/deposit"",
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
    ""default_route"": ""/ui/frmdeposit"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmdeposit_list"",
        ""route"": ""/ui/frmdeposit"",
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
        ""POST /transactions/deposit""
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
    public const string FrmwithdrawDescriptorJson = @"{
  ""screen_id"": ""frmwithdraw"",
  ""screen_name"": ""Withdrawal processing and balance deduction workflow"",
  ""route"": ""/ui/frmwithdraw"",
  ""state_model"": {
    ""screen_state_type"": ""FrmwithdrawState"",
    ""field_ids"": [
      ""txtdateoftransaction"",
      ""frame1"",
      ""frame3"",
      ""fracheque"",
      ""frawithdrawn"",
      ""label2"",
      ""lblfieldlabel"",
      ""lblaccountno"",
      ""lblaccounttype"",
      ""lblbalance"",
      ""lblcheque"",
      ""lblchequeissued"",
      ""lblcustid"",
      ""lblcustomerid"",
      ""lbldate"",
      ""lblfirst"",
      ""lblfirstname"",
      ""lbllast"",
      ""lbllastname"",
      ""lbltag"",
      ""lbltypeofaccount"",
      ""optno"",
      ""optyes"",
      ""txtaccountno"",
      ""txttransactionid"",
      ""txtwithdrawn""
    ],
    ""action_ids"": [
      ""action_cancel""
    ]
  },
  ""validation_map"": [
    {
      ""field_id"": ""txtaccountno"",
      ""rule_ids"": [
        ""VR-txtaccountno-REQUIRED"",
        ""VR-txtaccountno-NUMERIC""
      ]
    },
    {
      ""field_id"": ""txttransactionid"",
      ""rule_ids"": [
        ""VR-txttransactionid-NUMERIC""
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
    ""default_route"": ""/ui/frmwithdraw"",
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
    public const string FrmcheckbalanceDescriptorJson = @"{
  ""screen_id"": ""frmcheckbalance"",
  ""screen_name"": ""Balance inquiry and reconciliation workflow"",
  ""route"": ""/ui/frmcheckbalance"",
  ""state_model"": {
    ""screen_state_type"": ""FrmcheckbalanceState"",
    ""field_ids"": [
      ""dtpicker1"",
      ""cboaccountno"",
      ""frame1"",
      ""frame2"",
      ""label5"",
      ""lblaccno"",
      ""lblaccountno"",
      ""lblbal"",
      ""lblbalance"",
      ""lblcontacttitle"",
      ""lblcustomerid"",
      ""lbldate"",
      ""lblfirstname"",
      ""lbllastname"",
      ""txtacno"",
      ""txtcontacttitle"",
      ""txtcustomerid"",
      ""txtfirstname"",
      ""txtlastname"",
      ""txttypeofaccount""
    ],
    ""action_ids"": [
      ""action_save"",
      ""action_cancel""
    ]
  },
  ""validation_map"": [
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
      ""field_id"": ""txttypeofaccount"",
      ""rule_ids"": [
        ""VR-txttypeofaccount-REQUIRED"",
        ""VR-txttypeofaccount-NUMERIC""
      ]
    }
  ],
  ""event_wiring"": [
    {
      ""id"": ""evt_save"",
      ""name"": ""Save Screen Data"",
      ""trigger"": ""on_click"",
      ""targets"": [
        ""POST /transactions/deposit"",
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
    ""default_route"": ""/ui/frmcheckbalance"",
    ""opens"": [
      {
        ""screen_id"": ""ui_frmcheckbalance_list"",
        ""route"": ""/ui/frmcheckbalance"",
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
        ""POST /transactions/deposit""
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
            ["/frmdeposit"] = "/ui/frmdeposit",
            ["/frmwithdraw"] = "/ui/frmwithdraw",
            ["/frmcheckbalance"] = "/ui/frmcheckbalance",
    };
}
