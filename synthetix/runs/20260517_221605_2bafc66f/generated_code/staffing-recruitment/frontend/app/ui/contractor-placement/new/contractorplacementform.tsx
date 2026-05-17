"use client"

import { useState } from "react"
import { useRouter } from "next/navigation"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import { z } from "zod"
import { Loader2, Save } from "lucide-react"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Combobox } from "@/components/ui/combobox"
import { DatePicker } from "@/components/ui/date-picker"
import { Textarea } from "@/components/ui/textarea"

// BR-CTR-001 — Rate must be within client engagement's contracted ceiling unless co-approver has override authority.
// BR-CTR-002 — Start date must be at least 5 business days from creation date.
const schema = z.object({
  // Contractor Identity
  email: z.string().email("Invalid email address").optional().or(z.literal("")),
  firstname: z.string().optional(),
  lastname: z.string().optional(),
  // Client Contact
  contact_email: z.string().email("Invalid email address").optional().or(z.literal("")),
  contact_firstname: z.string().optional(),
  contact_lastname: z.string().optional(),
  // Address
  country_id: z.string().optional(),
  // Ownership
  PRIMARY_office_id: z.string().optional(),
  // Dates
  end_date: z.coerce.date().optional(),
  rp_valid_from: z.coerce.date().optional(),
  start_date: z.coerce.date().optional(),
  // Identifiers
  assignment_type_id: z.string().optional(),
  contractor_placement_id: z.string().optional(),
  employee_id: z.string().optional(),
  employment_type_id: z.string().optional(),
  // Rates & Pay
  is_nonexempt: z.boolean().default(false),
  job_title_id: z.string().optional(),
  is_passthru: z.boolean().default(false),
  pay_freq_type_id: z.string().optional(),
  hr_payrate_ot: z.coerce.number().optional(),
  hr_payrate_st: z.coerce.number().optional(),
  referal_fee: z.coerce.number().optional(),
  valid_from: z.coerce.date().optional(),
  valid_to: z.coerce.date().optional(),
  pct_vendor_discount: z.coerce.number().optional(),
  day_per_diem: z.coerce.number().optional(),
  fts_hr_burden: z.coerce.number().optional(),
  hrs_worked_type_id: z.string().optional(),
  hr_burden_dt: z.coerce.number().optional(),
  hr_burden_ot: z.coerce.number().optional(),
  hr_burden: z.coerce.number().optional(),
  hr_facility_fee: z.coerce.number().optional(),
  hr_fringe_benefit: z.coerce.number().optional(),
  hr_per_diem: z.coerce.number().optional(),
  pct_discount_inv: z.coerce.number().optional(),
  pci_total_cost: z.coerce.number().optional(),
  hr_payrate_dt: z.coerce.number().optional(),
  payrate_given_fts: z.coerce.number().optional(),
  burden: z.coerce.number().optional(),
  burden_sick: z.coerce.number().optional(),
  burden_sick_state: z.coerce.number().optional(),
  burden_sick_zip: z.coerce.number().optional(),
  profile_id: z.string().optional(),
  recent_profile: z.string().optional(),
  referal_fee_dt: z.coerce.number().optional(),
  referal_fee_ot: z.coerce.number().optional(),
  referal_fee_st: z.coerce.number().optional(),
  pct_discount: z.coerce.number().optional(),
  pct_vms_fee: z.coerce.number().optional(),
  amt_vendor_rate_reduction: z.coerce.number().optional(),
  pct_vendor_rate_reduction: z.coerce.number().optional(),
  pct_discount_vol: z.coerce.number().optional(),
  waiver_fee: z.coerce.number().optional(),
  // Billing
  bill_unit_type_id: z.string().optional(),
  hr_billrate_dt: z.coerce.number().optional(),
  hr_billrate_ot: z.coerce.number().optional(),
  hr_billrate_st: z.coerce.number().optional(),
  // Other
  apply_pct: z.boolean().default(false),
  field_label: z.string().optional(),
  field_name: z.string().optional(),
  file: z.any().optional(),
  id: z.string().min(1, "ID is required"),
  pt_emp_ids: z.string().optional(),
  referred_by: z.string().optional(),
  remarks: z.string().optional(),
  rp_remarks: z.string().optional(),
  // Customer Invoice Address
  customer_invaddr__custmaster_id: z.string().optional(),
  customer_invaddr__addrmaster_id: z.string().optional(),
  // Customer Invoice Format
  customer_invfmt__custmaster_id: z.string().optional(),
  customer_invfmt__invfmt_id: z.string().optional(),
  // Customer Invoice Frequency
  customer_invfreq__custmaster_id: z.string().optional(),
  customer_invfreq__invfreq_id: z.string().optional(),
  // Customer Invoice Terms
  customer_invterm__custmaster_id: z.string().optional(),
  customer_invterm__invterm_id: z.string().optional(),
  // Emergency Contact
  emg_contact__firstname: z.string().optional(),
  emg_contact__lastname: z.string().optional(),
  emg_contact__relationship: z.string().optional(),
  emg_contact__address_id: z.string().optional(),
  emg_contact__phone: z.string().optional(),
  emg_contact__email: z.string().email("Invalid email address").optional().or(z.literal("")),
  // Billing Cadence
  hours_type__hours_type_id: z.string().optional(),
  // HR Onboarding Checklist
  hr_checklist_details__onb_hr_checklist_type_id: z.string().optional(),
  hr_checklist_details__expiration_date: z.coerce.date().optional(),
  hr_checklist_details__create_ts: z.coerce.date().optional(),
  // Mentor / Buddy Assignment
  mentor__assignment_type_id: z.string().optional(),
  mentor__mentor_usertype_id: z.string().optional(),
  mentor__mentor_id: z.string().optional(),
  mentor__created_by_id: z.string().optional(),
  mentor__is_active: z.string().optional(),
  mentor__created_ts: z.coerce.date().optional(),
  // Milestone Resources
  milestone_resource__firstname: z.string().optional(),
  milestone_resource__lastname: z.string().optional(),
  milestone_resource__start_date: z.coerce.date().optional(),
  milestone_resource__end_date: z.coerce.date().optional(),
  milestone_resource__is_active: z.string().optional(),
  milestone_resource__create_ts: z.coerce.date().optional(),
  milestone_resource__created_by: z.string().optional(),
  milestone_resource__milestone_resource_id: z.string().optional(),
  // Milestone History
  milestone_status_history__contractor_placement_milestone_id: z.string().optional(),
  milestone_status_history__contractor_placement_milestone_status_id: z.string().optional(),
  milestone_status_history__milestone_date: z.coerce.date().optional(),
  milestone_status_history__user_id: z.string().optional(),
  milestone_status_history__create_ts: z.coerce.date().optional(),
  // Overdue Status Audit
  overdue_status_audit_log__week_start: z.string().optional(),
  overdue_status_audit_log__overdue_ts_status_type_id: z.string().optional(),
  overdue_status_audit_log__updated_by_id: z.string().optional(),
  overdue_status_audit_log__updated_ts: z.coerce.date().optional(),
  // Overdue Status
  overdue_status__week_start: z.string().optional(),
  overdue_status__overdue_ts_status_type_id: z.string().optional(),
  overdue_status__is_approved: z.string().optional(),
  overdue_status__updated_by_id: z.string().optional(),
  overdue_status__updated_ts: z.coerce.date().optional(),
  // Remote-Work Survey
  remote_survey__work_mode_id: z.string().optional(),
  remote_survey__year_num: z.coerce.number().int().optional(),
  remote_survey__quarter_num: z.coerce.number().int().optional(),
  remote_survey__request_json: z.string().optional(),
  remote_survey__create_ts: z.coerce.date().optional(),
  remote_survey__is_ccp_req_generated: z.coerce.number().optional(),
  remote_survey__rate_loc_valid_from: z.coerce.number().optional(),
  remote_survey__cur_valid_from: z.string().optional(),
  remote_survey__rate_loc_remarks: z.coerce.number().optional(),
  remote_survey__loc_valid_from: z.string().optional(),
  remote_survey__st1: z.string().optional(),
  remote_survey__st2: z.string().optional(),
  remote_survey__st3: z.string().optional(),
  remote_survey__city: z.string().optional(),
  remote_survey__work_state: z.string().optional(),
  remote_survey__zip: z.string().optional(),
  remote_survey__country_id: z.string().optional(),
  remote_survey__cur_work_address: z.string().optional(),
  remote_survey__new_work_address: z.string().optional(),
  remote_survey__state_burdens: z.coerce.number().optional(),
  remote_survey__zipBurdens: z.coerce.number().optional(),
  remote_survey__old_burden_sick: z.coerce.number().optional(),
  remote_survey__old_burden_sick_zip: z.coerce.number().optional(),
  remote_survey__old_burden_sick_state: z.coerce.number().optional(),
  remote_survey__new_burden_sick: z.coerce.number().optional(),
  remote_survey__new_burden_sick_zip: z.coerce.number().optional(),
  remote_survey__new_burden_sick_state: z.coerce.number().optional(),
  remote_survey__new_psl_jurisdicton: z.string().optional(),
  remote_survey__old_psl_jurisdicton: z.string().optional(),
  remote_survey__home_st1: z.string().optional(),
  remote_survey__home_st2: z.string().optional(),
  remote_survey__home_st3: z.string().optional(),
  remote_survey__home_city: z.string().optional(),
  remote_survey__home_state: z.string().optional(),
  remote_survey__home_zip: z.string().optional(),
  remote_survey__home_country_id: z.string().optional(),
  remote_survey__cur_home_address_id: z.string().optional(),
  remote_survey__cur_home_address: z.string().optional(),
  remote_survey__new_home_address: z.string().optional(),
  remote_survey__ccp_status_id: z.string().optional(),
  remote_survey__valid_from: z.string().optional(),
  remote_survey__effdate: z.string().optional(),
  remote_survey__valid_to: z.string().optional(),
  remote_survey__field_name: z.string().optional(),
  remote_survey__worksite_addr_id: z.string().optional(),
  remote_survey__field_label: z.string().optional(),
  remote_survey__remarks: z.string().optional(),
  // Training & Certifications
  training_type__training_type_id: z.string().optional(),
  training_type__state_id: z.string().optional(),
  training_type__city: z.string().optional(),
  training_type__onboard_doc_id: z.string().optional(),
  training_type__date_completed: z.coerce.date().optional(),
  training_type__create_ts: z.coerce.date().optional(),
  training_type__s_no: z.string().optional(),
  training_type__contractor: z.string().optional(),
  training_type__state: z.string().optional(),
  training_type__customer: z.string().optional(),
  training_type__employment_type: z.string().optional(),
  training_type__start_date: z.coerce.date().optional(),
  training_type__cca: z.string().optional(),
  training_type__training_type: z.string().optional(),
  training_type__last_date_completed: z.coerce.date().optional(),
  training_type__due_in: z.string().optional(),
  // Vaccination Disclosure
  vaccination_answers__vaccination_status_type_id: z.string().optional(),
  vaccination_answers__document_id: z.string().optional(),
  vaccination_answers__vac_date: z.coerce.date().optional(),
  vaccination_answers__created_by_id: z.string().optional(),
  vaccination_answers__create_ts: z.coerce.date().optional(),
  // Vendor Assignment
  vendor__vendor_id: z.string().optional(),
  vendor__vendor_name: z.string().optional(),
})

type FormValues = z.infer<typeof schema>

export default function ContractorPlacementForm() {
  const router = useRouter()
  const [serverError, setServerError] = useState("")
  const { register, handleSubmit, formState: { errors, isSubmitting }, setValue, watch } =
    useForm<FormValues>({ resolver: zodResolver(schema) })

  async function onSubmit(values: FormValues) {
    setServerError("")
    const r = await fetch("/api/v1/contractor", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(values),
    })
    if (!r.ok) {
      setServerError(`Save failed (HTTP ${r.status})`)
      return
    }
    router.push("/ui/contractor-placement/approvals")
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      {/* Contractor Identity */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Contractor Identity</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="email">Email</Label>
              <Input id="email" type="email" {...register("email")} />
              {errors.email && <p className="text-xs text-red-600">{errors.email.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="firstname">First Name</Label>
              <Input id="firstname" type="text" {...register("firstname")} />
              {errors.firstname && <p className="text-xs text-red-600">{errors.firstname.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="lastname">Last Name</Label>
              <Input id="lastname" type="text" {...register("lastname")} />
              {errors.lastname && <p className="text-xs text-red-600">{errors.lastname.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Client Contact */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Client Contact</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="contact_email">Contact Email</Label>
              <Input id="contact_email" type="email" {...register("contact_email")} />
              {errors.contact_email && <p className="text-xs text-red-600">{errors.contact_email.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="contact_firstname">Contact First Name</Label>
              <Input id="contact_firstname" type="text" {...register("contact_firstname")} />
              {errors.contact_firstname && <p className="text-xs text-red-600">{errors.contact_firstname.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="contact_lastname">Contact Last Name</Label>
              <Input id="contact_lastname" type="text" {...register("contact_lastname")} />
              {errors.contact_lastname && <p className="text-xs text-red-600">{errors.contact_lastname.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Address */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Address</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="country_id">Country</Label>
              <Combobox
                id="country_id"
                placeholder="Select country"
                value={watch("country_id") || ""}
                onValueChange={(v) => setValue("country_id", v)}
                options={[]} />
              {errors.country_id && <p className="text-xs text-red-600">{errors.country_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Ownership */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Ownership</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="PRIMARY_office_id">Primary Office</Label>
              <Combobox
                id="PRIMARY_office_id"
                placeholder="Select office"
                value={watch("PRIMARY_office_id") || ""}
                onValueChange={(v) => setValue("PRIMARY_office_id", v)}
                options={[]} />
              {errors.PRIMARY_office_id && <p className="text-xs text-red-600">{errors.PRIMARY_office_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Dates */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Dates</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="end_date">End Date</Label>
              <DatePicker id="end_date" value={watch("end_date")} onChange={(d) => setValue("end_date", d)} />
              {errors.end_date && <p className="text-xs text-red-600">{errors.end_date.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="rp_valid_from">RP Valid From</Label>
              <DatePicker id="rp_valid_from" value={watch("rp_valid_from")} onChange={(d) => setValue("rp_valid_from", d)} />
              {errors.rp_valid_from && <p className="text-xs text-red-600">{errors.rp_valid_from.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="start_date">Start Date</Label>
              <DatePicker id="start_date" value={watch("start_date")} onChange={(d) => setValue("start_date", d)} />
              {errors.start_date && <p className="text-xs text-red-600">{errors.start_date.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Identifiers */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Identifiers</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="assignment_type_id">Assignment Type</Label>
              <Combobox
                id="assignment_type_id"
                placeholder="Select assignment type"
                value={watch("assignment_type_id") || ""}
                onValueChange={(v) => setValue("assignment_type_id", v)}
                options={[]} />
              {errors.assignment_type_id && <p className="text-xs text-red-600">{errors.assignment_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="contractor_placement_id">Contractor Placement</Label>
              <Combobox
                id="contractor_placement_id"
                placeholder="Select placement"
                value={watch("contractor_placement_id") || ""}
                onValueChange={(v) => setValue("contractor_placement_id", v)}
                options={[]} />
              {errors.contractor_placement_id && <p className="text-xs text-red-600">{errors.contractor_placement_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="employee_id">Employee</Label>
              <Combobox
                id="employee_id"
                placeholder="Select employee"
                value={watch("employee_id") || ""}
                onValueChange={(v) => setValue("employee_id", v)}
                options={[]} />
              {errors.employee_id && <p className="text-xs text-red-600">{errors.employee_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="employment_type_id">Employment Type</Label>
              <Combobox
                id="employment_type_id"
                placeholder="Select employment type"
                value={watch("employment_type_id") || ""}
                onValueChange={(v) => setValue("employment_type_id", v)}
                options={[]} />
              {errors.employment_type_id && <p className="text-xs text-red-600">{errors.employment_type_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Rates & Pay */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Rates & Pay</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="is_nonexempt">
                <input id="is_nonexempt" type="checkbox" {...register("is_nonexempt")} className="mr-2" />
                Is Non-Exempt
              </Label>
              {errors.is_nonexempt && <p className="text-xs text-red-600">{errors.is_nonexempt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="job_title_id">Job Title</Label>
              <Combobox
                id="job_title_id"
                placeholder="Select job title"
                value={watch("job_title_id") || ""}
                onValueChange={(v) => setValue("job_title_id", v)}
                options={[]} />
              {errors.job_title_id && <p className="text-xs text-red-600">{errors.job_title_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="is_passthru">
                <input id="is_passthru" type="checkbox" {...register("is_passthru")} className="mr-2" />
                Is Pass-Through
              </Label>
              {errors.is_passthru && <p className="text-xs text-red-600">{errors.is_passthru.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pay_freq_type_id">Pay Frequency Type</Label>
              <Combobox
                id="pay_freq_type_id"
                placeholder="Select pay frequency"
                value={watch("pay_freq_type_id") || ""}
                onValueChange={(v) => setValue("pay_freq_type_id", v)}
                options={[]} />
              {errors.pay_freq_type_id && <p className="text-xs text-red-600">{errors.pay_freq_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_payrate_ot">HR Pay Rate OT</Label>
              <Input id="hr_payrate_ot" type="number" step="0.01" {...register("hr_payrate_ot")} />
              {errors.hr_payrate_ot && <p className="text-xs text-red-600">{errors.hr_payrate_ot.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_payrate_st">HR Pay Rate ST</Label>
              <Input id="hr_payrate_st" type="number" step="0.01" {...register("hr_payrate_st")} />
              {errors.hr_payrate_st && <p className="text-xs text-red-600">{errors.hr_payrate_st.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referal_fee">Referral Fee</Label>
              <Input id="referal_fee" type="number" step="0.01" {...register("referal_fee")} />
              {errors.referal_fee && <p className="text-xs text-red-600">{errors.referal_fee.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="valid_from">Valid From</Label>
              <DatePicker id="valid_from" value={watch("valid_from")} onChange={(d) => setValue("valid_from", d)} />
              {errors.valid_from && <p className="text-xs text-red-600">{errors.valid_from.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="valid_to">Valid To</Label>
              <DatePicker id="valid_to" value={watch("valid_to")} onChange={(d) => setValue("valid_to", d)} />
              {errors.valid_to && <p className="text-xs text-red-600">{errors.valid_to.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_vendor_discount">Vendor Discount %</Label>
              <Input id="pct_vendor_discount" type="number" step="0.01" {...register("pct_vendor_discount")} />
              {errors.pct_vendor_discount && <p className="text-xs text-red-600">{errors.pct_vendor_discount.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="day_per_diem">Day Per Diem</Label>
              <Input id="day_per_diem" type="number" step="0.01" {...register("day_per_diem")} />
              {errors.day_per_diem && <p className="text-xs text-red-600">{errors.day_per_diem.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="fts_hr_burden">FTS HR Burden</Label>
              <Input id="fts_hr_burden" type="number" step="0.01" {...register("fts_hr_burden")} />
              {errors.fts_hr_burden && <p className="text-xs text-red-600">{errors.fts_hr_burden.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hrs_worked_type_id">Hours Worked Type</Label>
              <Combobox
                id="hrs_worked_type_id"
                placeholder="Select hours worked type"
                value={watch("hrs_worked_type_id") || ""}
                onValueChange={(v) => setValue("hrs_worked_type_id", v)}
                options={[]} />
              {errors.hrs_worked_type_id && <p className="text-xs text-red-600">{errors.hrs_worked_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_burden_dt">HR Burden DT</Label>
              <Input id="hr_burden_dt" type="number" step="0.01" {...register("hr_burden_dt")} />
              {errors.hr_burden_dt && <p className="text-xs text-red-600">{errors.hr_burden_dt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_burden_ot">HR Burden OT</Label>
              <Input id="hr_burden_ot" type="number" step="0.01" {...register("hr_burden_ot")} />
              {errors.hr_burden_ot && <p className="text-xs text-red-600">{errors.hr_burden_ot.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_burden">HR Burden</Label>
              <Input id="hr_burden" type="number" step="0.01" {...register("hr_burden")} />
              {errors.hr_burden && <p className="text-xs text-red-600">{errors.hr_burden.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_facility_fee">HR Facility Fee</Label>
              <Input id="hr_facility_fee" type="number" step="0.01" {...register("hr_facility_fee")} />
              {errors.hr_facility_fee && <p className="text-xs text-red-600">{errors.hr_facility_fee.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_fringe_benefit">HR Fringe Benefit</Label>
              <Input id="hr_fringe_benefit" type="number" step="0.01" {...register("hr_fringe_benefit")} />
              {errors.hr_fringe_benefit && <p className="text-xs text-red-600">{errors.hr_fringe_benefit.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_per_diem">HR Per Diem</Label>
              <Input id="hr_per_diem" type="number" step="0.01" {...register("hr_per_diem")} />
              {errors.hr_per_diem && <p className="text-xs text-red-600">{errors.hr_per_diem.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_discount_inv">Discount Invoice %</Label>
              <Input id="pct_discount_inv" type="number" step="0.01" {...register("pct_discount_inv")} />
              {errors.pct_discount_inv && <p className="text-xs text-red-600">{errors.pct_discount_inv.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pci_total_cost">Total Cost</Label>
              <Input id="pci_total_cost" type="number" step="0.01" {...register("pci_total_cost")} />
              {errors.pci_total_cost && <p className="text-xs text-red-600">{errors.pci_total_cost.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_payrate_dt">HR Pay Rate DT</Label>
              <Input id="hr_payrate_dt" type="number" step="0.01" {...register("hr_payrate_dt")} />
              {errors.hr_payrate_dt && <p className="text-xs text-red-600">{errors.hr_payrate_dt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="payrate_given_fts">Pay Rate Given FTS</Label>
              <Input id="payrate_given_fts" type="number" step="0.01" {...register("payrate_given_fts")} />
              {errors.payrate_given_fts && <p className="text-xs text-red-600">{errors.payrate_given_fts.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="burden">Burden</Label>
              <Input id="burden" type="number" step="0.01" {...register("burden")} />
              {errors.burden && <p className="text-xs text-red-600">{errors.burden.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="burden_sick">Burden Sick</Label>
              <Input id="burden_sick" type="number" step="0.01" {...register("burden_sick")} />
              {errors.burden_sick && <p className="text-xs text-red-600">{errors.burden_sick.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="burden_sick_state">Burden Sick State</Label>
              <Input id="burden_sick_state" type="number" step="0.01" {...register("burden_sick_state")} />
              {errors.burden_sick_state && <p className="text-xs text-red-600">{errors.burden_sick_state.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="burden_sick_zip">Burden Sick Zip</Label>
              <Input id="burden_sick_zip" type="number" step="0.01" {...register("burden_sick_zip")} />
              {errors.burden_sick_zip && <p className="text-xs text-red-600">{errors.burden_sick_zip.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="profile_id">Profile</Label>
              <Combobox
                id="profile_id"
                placeholder="Select profile"
                value={watch("profile_id") || ""}
                onValueChange={(v) => setValue("profile_id", v)}
                options={[]} />
              {errors.profile_id && <p className="text-xs text-red-600">{errors.profile_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="recent_profile">Recent Profile</Label>
              <Input id="recent_profile" type="text" {...register("recent_profile")} />
              {errors.recent_profile && <p className="text-xs text-red-600">{errors.recent_profile.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referal_fee_dt">Referral Fee DT</Label>
              <Input id="referal_fee_dt" type="number" step="0.01" {...register("referal_fee_dt")} />
              {errors.referal_fee_dt && <p className="text-xs text-red-600">{errors.referal_fee_dt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referal_fee_ot">Referral Fee OT</Label>
              <Input id="referal_fee_ot" type="number" step="0.01" {...register("referal_fee_ot")} />
              {errors.referal_fee_ot && <p className="text-xs text-red-600">{errors.referal_fee_ot.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referal_fee_st">Referral Fee ST</Label>
              <Input id="referal_fee_st" type="number" step="0.01" {...register("referal_fee_st")} />
              {errors.referal_fee_st && <p className="text-xs text-red-600">{errors.referal_fee_st.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_discount">Discount %</Label>
              <Input id="pct_discount" type="number" step="0.01" {...register("pct_discount")} />
              {errors.pct_discount && <p className="text-xs text-red-600">{errors.pct_discount.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_vms_fee">VMS Fee %</Label>
              <Input id="pct_vms_fee" type="number" step="0.01" {...register("pct_vms_fee")} />
              {errors.pct_vms_fee && <p className="text-xs text-red-600">{errors.pct_vms_fee.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="amt_vendor_rate_reduction">Vendor Rate Reduction Amount</Label>
              <Input id="amt_vendor_rate_reduction" type="number" step="0.01" {...register("amt_vendor_rate_reduction")} />
              {errors.amt_vendor_rate_reduction && <p className="text-xs text-red-600">{errors.amt_vendor_rate_reduction.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_vendor_rate_reduction">Vendor Rate Reduction %</Label>
              <Input id="pct_vendor_rate_reduction" type="number" step="0.01" {...register("pct_vendor_rate_reduction")} />
              {errors.pct_vendor_rate_reduction && <p className="text-xs text-red-600">{errors.pct_vendor_rate_reduction.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_discount_vol">Volume Discount %</Label>
              <Input id="pct_discount_vol" type="number" step="0.01" {...register("pct_discount_vol")} />
              {errors.pct_discount_vol && <p className="text-xs text-red-600">{errors.pct_discount_vol.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="waiver_fee">Waiver Fee</Label>
              <Input id="waiver_fee" type="number" step="0.01" {...register("waiver_fee")} />
              {errors.waiver_fee && <p className="text-xs text-red-600">{errors.waiver_fee.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Billing */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Billing</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="bill_unit_type_id">Bill Unit Type</Label>
              <Combobox
                id="bill_unit_type_id"
                placeholder="Select bill unit type"
                value={watch("bill_unit_type_id") || ""}
                onValueChange={(v) => setValue("bill_unit_type_id", v)}
                options={[]} />
              {errors.bill_unit_type_id && <p className="text-xs text-red-600">{errors.bill_unit_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_billrate_dt">HR Bill Rate DT</Label>
              <Input id="hr_billrate_dt" type="number" step="0.01" {...register("hr_billrate_dt")} />
              {errors.hr_billrate_dt && <p className="text-xs text-red-600">{errors.hr_billrate_dt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_billrate_ot">HR Bill Rate OT</Label>
              <Input id="hr_billrate_ot" type="number" step="0.01" {...register("hr_billrate_ot")} />
              {errors.hr_billrate_ot && <p className="text-xs text-red-600">{errors.hr_billrate_ot.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_billrate_st">HR Bill Rate ST</Label>
              <Input id="hr_billrate_st" type="number" step="0.01" {...register("hr_billrate_st")} />
              {errors.hr_billrate_st && <p className="text-xs text-red-600">{errors.hr_billrate_st.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Other */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Other</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="apply_pct">
                <input id="apply_pct" type="checkbox" {...register("apply_pct")} className="mr-2" />
                Apply Percentage
              </Label>
              {errors.apply_pct && <p className="text-xs text-red-600">{errors.apply_pct.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="field_label">Field Label</Label>
              <Input id="field_label" type="text" {...register("field_label")} />
              {errors.field_label && <p className="text-xs text-red-600">{errors.field_label.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="field_name">Field Name</Label>
              <Input id="field_name" type="text" {...register("field_name")} />
              {errors.field_name && <p className="text-xs text-red-600">{errors.field_name.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="file">File</Label>
              <Input id="file" type="file" {...register("file")} />
              {errors.file && <p className="text-xs text-red-600">{errors.file.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="id">ID <span className="text-red-600">*</span></Label>
              <Input id="id" type="text" {...register("id")} />
              {errors.id && <p className="text-xs text-red-600">{errors.id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pt_emp_ids">PT Employee IDs</Label>
              <Input id="pt_emp_ids" type="text" {...register("pt_emp_ids")} />
              {errors.pt_emp_ids && <p className="text-xs text-red-600">{errors.pt_emp_ids.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referred_by">Referred By</Label>
              <Input id="referred_by" type="text" {...register("referred_by")} />
              {errors.referred_by && <p className="text-xs text-red-600">{errors.referred_by.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remarks">Remarks</Label>
              <Textarea id="remarks" {...register("remarks")} />
              {errors.remarks && <p className="text-xs text-red-600">{errors.remarks.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="rp_remarks">RP Remarks</Label>
              <Textarea id="rp_remarks" {...register("rp_remarks")} />
              {errors.rp_remarks && <p className="text-xs text-red-600">{errors.rp_remarks.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Customer Invoice Address */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Customer Invoice Address</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="customer_invaddr__custmaster_id">Customer Master</Label>
              <Combobox
                id="customer_invaddr__custmaster_id"
                placeholder="Select customer"
                value={watch("customer_invaddr__custmaster_id") || ""}
                onValueChange={(v) => setValue("customer_invaddr__custmaster_id", v)}
                options={[]} />
              {errors.customer_invaddr__custmaster_id && <p className="text-xs text-red-600">{errors.customer_invaddr__custmaster_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="customer_invaddr__addrmaster_id">Address Master</Label>
              <Combobox
                id="customer_invaddr__addrmaster_id"
                placeholder="Select address"
                value={watch("customer_invaddr__addrmaster_id") || ""}
                onValueChange={(v) => setValue("customer_invaddr__addrmaster_id", v)}
                options={[]} />
              {errors.customer_invaddr__addrmaster_id && <p className="text-xs text-red-600">{errors.customer_invaddr__addrmaster_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Customer Invoice Format */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Customer Invoice Format</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="customer_invfmt__custmaster_id">Customer Master</Label>
              <Combobox
                id="customer_invfmt__custmaster_id"
                placeholder="Select customer"
                value={watch("customer_invfmt__custmaster_id") || ""}
                onValueChange={(v) => setValue("customer_invfmt__custmaster_id", v)}
                options={[]} />
              {errors.customer_invfmt__custmaster_id && <p className="text-xs text-red-600">{errors.customer_invfmt__custmaster_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="customer_invfmt__invfmt_id">Invoice Format</Label>
              <Combobox
                id="customer_invfmt__invfmt_id"
                placeholder="Select format"
                value={watch("customer_invfmt__invfmt_id") || ""}
                onValueChange={(v) => setValue("customer_invfmt__invfmt_id", v)}
                options={[]} />
              {errors.customer_invfmt__invfmt_id && <p className="text-xs text-red-600">{errors.customer_invfmt__invfmt_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Customer Invoice Frequency */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Customer Invoice Frequency</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="customer_invfreq__custmaster_id">Customer Master</Label>
              <Combobox
                id="customer_invfreq__custmaster_id"
                placeholder="Select customer"
                value={watch("customer_invfreq__custmaster_id") || ""}
                onValueChange={(v) => setValue("customer_invfreq__custmaster_id", v)}
                options={[]} />
              {errors.customer_invfreq__custmaster_id && <p className="text-xs text-red-600">{errors.customer_invfreq__custmaster_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="customer_invfreq__invfreq_id">Invoice Frequency</Label>
              <Combobox
                id="customer_invfreq__invfreq_id"
                placeholder="Select frequency"
                value={watch("customer_invfreq__invfreq_id") || ""}
                onValueChange={(v) => setValue("customer_invfreq__invfreq_id", v)}
                options={[]} />
              {errors.customer_invfreq__invfreq_id && <p className="text-xs text-red-600">{errors.customer_invfreq__invfreq_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Customer Invoice Terms */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Customer Invoice Terms</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="customer_invterm__custmaster_id">Customer Master</Label>
              <Combobox
                id="customer_invterm__custmaster_id"
                placeholder="Select customer"
                value={watch("customer_invterm__custmaster_id") || ""}
                onValueChange={(v) => setValue("customer_invterm__custmaster_id", v)}
                options={[]} />
              {errors.customer_invterm__custmaster_id && <p className="text-xs text-red-600">{errors.customer_invterm__custmaster_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="customer_invterm__invterm_id">Invoice Terms</Label>
              <Combobox
                id="customer_invterm__invterm_id"
                placeholder="Select terms"
                value={watch("customer_invterm__invterm_id") || ""}
                onValueChange={(v) => setValue("customer_invterm__invterm_id", v)}
                options={[]} />
              {errors.customer_invterm__invterm_id && <p className="text-xs text-red-600">{errors.customer_invterm__invterm_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Emergency Contact */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Emergency Contact</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="emg_contact__firstname">First Name</Label>
              <Input id="emg_contact__firstname" type="text" {...register("emg_contact__firstname")} />
              {errors.emg_contact__firstname && <p className="text-xs text-red-600">{errors.emg_contact__firstname.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="emg_contact__lastname">Last Name</Label>
              <Input id="emg_contact__lastname" type="text" {...register("emg_contact__lastname")} />
              {errors.emg_contact__lastname && <p className="text-xs text-red-600">{errors.emg_contact__lastname.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="emg_contact__relationship">Relationship</Label>
              <Input id="emg_contact__relationship" type="text" {...register("emg_contact__relationship")} />
              {errors.emg_contact__relationship && <p className="text-xs text-red-600">{errors.emg_contact__relationship.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="emg_contact__address_id">Address</Label>
              <Combobox
                id="emg_contact__address_id"
                placeholder="Select address"
                value={watch("emg_contact__address_id") || ""}
                onValueChange={(v) => setValue("emg_contact__address_id", v)}
                options={[]} />
              {errors.emg_contact__address_id && <p className="text-xs text-red-600">{errors.emg_contact__address_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="emg_contact__phone">Phone</Label>
              <Input id="emg_contact__phone" type="text" {...register("emg_contact__phone")} />
              {errors.emg_contact__phone && <p className="text-xs text-red-600">{errors.emg_contact__phone.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="emg_contact__email">Email</Label>
              <Input id="emg_contact__email" type="email" {...register("emg_contact__email")} />
              {errors.emg_contact__email && <p className="text-xs text-red-600">{errors.emg_contact__email.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Billing Cadence */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Billing Cadence</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="hours_type__hours_type_id">Hours Type</Label>
              <Combobox
                id="hours_type__hours_type_id"
                placeholder="Select hours type"
                value={watch("hours_type__hours_type_id") || ""}
                onValueChange={(v) => setValue("hours_type__hours_type_id", v)}
                options={[]} />
              {errors.hours_type__hours_type_id && <p className="text-xs text-red-600">{errors.hours_type__hours_type_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* HR Onboarding Checklist */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>HR Onboarding Checklist</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="hr_checklist_details__onb_hr_checklist_type_id">Checklist Type</Label>
              <Combobox
                id="hr_checklist_details__onb_hr_checklist_type_id"
                placeholder="Select checklist type"
                value={watch("hr_checklist_details__onb_hr_checklist_type_id") || ""}
                onValueChange={(v) => setValue("hr_checklist_details__onb_hr_checklist_type_id", v)}
                options={[]} />
              {errors.hr_checklist_details__onb_hr_checklist_type_id && <p className="text-xs text-red-600">{errors.hr_checklist_details__onb_hr_checklist_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_checklist_details__expiration_date">Expiration Date</Label>
              <DatePicker id="hr_checklist_details__expiration_date" value={watch("hr_checklist_details__expiration_date")} onChange={(d) => setValue("hr_checklist_details__expiration_date", d)} />
              {errors.hr_checklist_details__expiration_date && <p className="text-xs text-red-600">{errors.hr_checklist_details__expiration_date.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_checklist_details__create_ts">Created</Label>
              <DatePicker id="hr_checklist_details__create_ts" value={watch("hr_checklist_details__create_ts")} onChange={(d) => setValue("hr_checklist_details__create_ts", d)} />
              {errors.hr_checklist_details__create_ts && <p className="text-xs text-red-600">{errors.hr_checklist_details__create_ts.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Mentor / Buddy Assignment */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Mentor / Buddy Assignment</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="mentor__assignment_type_id">Assignment Type</Label>
              <Combobox
                id="mentor__assignment_type_id"
                placeholder="Select assignment type"
                value={watch("mentor__assignment_type_id") || ""}
                onValueChange={(v) => setValue("mentor__assignment_type_id", v)}
                options={[]} />
              {errors.mentor__assignment_type_id && <p className="text-xs text-red-600">{errors.mentor__assignment_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="mentor__mentor_usertype_id">Mentor User Type</Label>
              <Combobox
                id="mentor__mentor_usertype_id"
                placeholder="Select user type"
                value={watch("mentor__mentor_usertype_id") || ""}
                onValueChange={(v) => setValue("mentor__mentor_usertype_id", v)}
                options={[]} />
              {errors.mentor__mentor_usertype_id && <p className="text-xs text-red-600">{errors.mentor__mentor_usertype_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="mentor__mentor_id">Mentor</Label>
              <Combobox
                id="mentor__mentor_id"
                placeholder="Select mentor"
                value={watch("mentor__mentor_id") || ""}
                onValueChange={(v) => setValue("mentor__mentor_id", v)}
                options={[]} />
              {errors.mentor__mentor_id && <p className="text-xs text-red-600">{errors.mentor__mentor_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="mentor__created_by_id">Created By</Label>
              <Combobox
                id="mentor__created_by_id"
                placeholder="Select user"
                value={watch("mentor__created_by_id") || ""}
                onValueChange={(v) => setValue("mentor__created_by_id", v)}
                options={[]} />
              {errors.mentor__created_by_id && <p className="text-xs text-red-600">{errors.mentor__created_by_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="mentor__is_active">Is Active</Label>
              <Input id="mentor__is_active" type="text" {...register("mentor__is_active")} />
              {errors.mentor__is_active && <p className="text-xs text-red-600">{errors.mentor__is_active.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="mentor__created_ts">Created</Label>
              <DatePicker id="mentor__created_ts" value={watch("mentor__created_ts")} onChange={(d) => setValue("mentor__created_ts", d)} />
              {errors.mentor__created_ts && <p className="text-xs text-red-600">{errors.mentor__created_ts.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Milestone Resources */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Milestone Resources</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="milestone_resource__firstname">First Name</Label>
              <Input id="milestone_resource__firstname" type="text" {...register("milestone_resource__firstname")} />
              {errors.milestone_resource__firstname && <p className="text-xs text-red-600">{errors.milestone_resource__firstname.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_resource__lastname">Last Name</Label>
              <Input id="milestone_resource__lastname" type="text" {...register("milestone_resource__lastname")} />
              {errors.milestone_resource__lastname && <p className="text-xs text-red-600">{errors.milestone_resource__lastname.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_resource__start_date">Start Date</Label>
              <DatePicker id="milestone_resource__start_date" value={watch("milestone_resource__start_date")} onChange={(d) => setValue("milestone_resource__start_date", d)} />
              {errors.milestone_resource__start_date && <p className="text-xs text-red-600">{errors.milestone_resource__start_date.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_resource__end_date">End Date</Label>
              <DatePicker id="milestone_resource__end_date" value={watch("milestone_resource__end_date")} onChange={(d) => setValue("milestone_resource__end_date", d)} />
              {errors.milestone_resource__end_date && <p className="text-xs text-red-600">{errors.milestone_resource__end_date.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_resource__is_active">Is Active</Label>
              <Input id="milestone_resource__is_active" type="text" {...register("milestone_resource__is_active")} />
              {errors.milestone_resource__is_active && <p className="text-xs text-red-600">{errors.milestone_resource__is_active.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_resource__create_ts">Created</Label>
              <DatePicker id="milestone_resource__create_ts" value={watch("milestone_resource__create_ts")} onChange={(d) => setValue("milestone_resource__create_ts", d)} />
              {errors.milestone_resource__create_ts && <p className="text-xs text-red-600">{errors.milestone_resource__create_ts.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_resource__created_by">Created By</Label>
              <Input id="milestone_resource__created_by" type="text" {...register("milestone_resource__created_by")} />
              {errors.milestone_resource__created_by && <p className="text-xs text-red-600">{errors.milestone_resource__created_by.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_resource__milestone_resource_id">Milestone Resource</Label>
              <Combobox
                id="milestone_resource__milestone_resource_id"
                placeholder="Select resource"
                value={watch("milestone_resource__milestone_resource_id") || ""}
                onValueChange={(v) => setValue("milestone_resource__milestone_resource_id", v)}
                options={[]} />
              {errors.milestone_resource__milestone_resource_id && <p className="text-xs text-red-600">{errors.milestone_resource__milestone_resource_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Milestone History */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Milestone History</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="milestone_status_history__contractor_placement_milestone_id">Milestone</Label>
              <Combobox
                id="milestone_status_history__contractor_placement_milestone_id"
                placeholder="Select milestone"
                value={watch("milestone_status_history__contractor_placement_milestone_id") || ""}
                onValueChange={(v) => setValue("milestone_status_history__contractor_placement_milestone_id", v)}
                options={[]} />
              {errors.milestone_status_history__contractor_placement_milestone_id && <p className="text-xs text-red-600">{errors.milestone_status_history__contractor_placement_milestone_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_status_history__contractor_placement_milestone_status_id">Status</Label>
              <Combobox
                id="milestone_status_history__contractor_placement_milestone_status_id"
                placeholder="Select status"
                value={watch("milestone_status_history__contractor_placement_milestone_status_id") || ""}
                onValueChange={(v) => setValue("milestone_status_history__contractor_placement_milestone_status_id", v)}
                options={[]} />
              {errors.milestone_status_history__contractor_placement_milestone_status_id && <p className="text-xs text-red-600">{errors.milestone_status_history__contractor_placement_milestone_status_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_status_history__milestone_date">Milestone Date</Label>
              <DatePicker id="milestone_status_history__milestone_date" value={watch("milestone_status_history__milestone_date")} onChange={(d) => setValue("milestone_status_history__milestone_date", d)} />
              {errors.milestone_status_history__milestone_date && <p className="text-xs text-red-600">{errors.milestone_status_history__milestone_date.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_status_history__user_id">User</Label>
              <Combobox
                id="milestone_status_history__user_id"
                placeholder="Select user"
                value={watch("milestone_status_history__user_id") || ""}
                onValueChange={(v) => setValue("milestone_status_history__user_id", v)}
                options={[]} />
              {errors.milestone_status_history__user_id && <p className="text-xs text-red-600">{errors.milestone_status_history__user_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="milestone_status_history__create_ts">Created</Label>
              <DatePicker id="milestone_status_history__create_ts" value={watch("milestone_status_history__create_ts")} onChange={(d) => setValue("milestone_status_history__create_ts", d)} />
              {errors.milestone_status_history__create_ts && <p className="text-xs text-red-600">{errors.milestone_status_history__create_ts.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Overdue Status Audit */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Overdue Status Audit</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="overdue_status_audit_log__week_start">Week Start</Label>
              <Input id="overdue_status_audit_log__week_start" type="text" {...register("overdue_status_audit_log__week_start")} />
              {errors.overdue_status_audit_log__week_start && <p className="text-xs text-red-600">{errors.overdue_status_audit_log__week_start.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="overdue_status_audit_log__overdue_ts_status_type_id">Status Type</Label>
              <Combobox
                id="overdue_status_audit_log__overdue_ts_status_type_id"
                placeholder="Select status type"
                value={watch("overdue_status_audit_log__overdue_ts_status_type_id") || ""}
                onValueChange={(v) => setValue("overdue_status_audit_log__overdue_ts_status_type_id", v)}
                options={[]} />
              {errors.overdue_status_audit_log__overdue_ts_status_type_id && <p className="text-xs text-red-600">{errors.overdue_status_audit_log__overdue_ts_status_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="overdue_status_audit_log__updated_by_id">Updated By</Label>
              <Combobox
                id="overdue_status_audit_log__updated_by_id"
                placeholder="Select user"
                value={watch("overdue_status_audit_log__updated_by_id") || ""}
                onValueChange={(v) => setValue("overdue_status_audit_log__updated_by_id", v)}
                options={[]} />
              {errors.overdue_status_audit_log__updated_by_id && <p className="text-xs text-red-600">{errors.overdue_status_audit_log__updated_by_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="overdue_status_audit_log__updated_ts">Updated</Label>
              <DatePicker id="overdue_status_audit_log__updated_ts" value={watch("overdue_status_audit_log__updated_ts")} onChange={(d) => setValue("overdue_status_audit_log__updated_ts", d)} />
              {errors.overdue_status_audit_log__updated_ts && <p className="text-xs text-red-600">{errors.overdue_status_audit_log__updated_ts.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Overdue Status */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Overdue Status</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="overdue_status__week_start">Week Start</Label>
              <Input id="overdue_status__week_start" type="text" {...register("overdue_status__week_start")} />
              {errors.overdue_status__week_start && <p className="text-xs text-red-600">{errors.overdue_status__week_start.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="overdue_status__overdue_ts_status_type_id">Status Type</Label>
              <Combobox
                id="overdue_status__overdue_ts_status_type_id"
                placeholder="Select status type"
                value={watch("overdue_status__overdue_ts_status_type_id") || ""}
                onValueChange={(v) => setValue("overdue_status__overdue_ts_status_type_id", v)}
                options={[]} />
              {errors.overdue_status__overdue_ts_status_type_id && <p className="text-xs text-red-600">{errors.overdue_status__overdue_ts_status_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="overdue_status__is_approved">Is Approved</Label>
              <Input id="overdue_status__is_approved" type="text" {...register("overdue_status__is_approved")} />
              {errors.overdue_status__is_approved && <p className="text-xs text-red-600">{errors.overdue_status__is_approved.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="overdue_status__updated_by_id">Updated By</Label>
              <Combobox
                id="overdue_status__updated_by_id"
                placeholder="Select user"
                value={watch("overdue_status__updated_by_id") || ""}
                onValueChange={(v) => setValue("overdue_status__updated_by_id", v)}
                options={[]} />
              {errors.overdue_status__updated_by_id && <p className="text-xs text-red-600">{errors.overdue_status__updated_by_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="overdue_status__updated_ts">Updated</Label>
              <DatePicker id="overdue_status__updated_ts" value={watch("overdue_status__updated_ts")} onChange={(d) => setValue("overdue_status__updated_ts", d)} />
              {errors.overdue_status__updated_ts && <p className="text-xs text-red-600">{errors.overdue_status__updated_ts.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Remote-Work Survey */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Remote-Work Survey</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="remote_survey__work_mode_id">Work Mode</Label>
              <Combobox
                id="remote_survey__work_mode_id"
                placeholder="Select work mode"
                value={watch("remote_survey__work_mode_id") || ""}
                onValueChange={(v) => setValue("remote_survey__work_mode_id", v)}
                options={[]} />
              {errors.remote_survey__work_mode_id && <p className="text-xs text-red-600">{errors.remote_survey__work_mode_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__year_num">Year</Label>
              <Input id="remote_survey__year_num" type="number" step="1" {...register("remote_survey__year_num")} />
              {errors.remote_survey__year_num && <p className="text-xs text-red-600">{errors.remote_survey__year_num.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__quarter_num">Quarter</Label>
              <Input id="remote_survey__quarter_num" type="number" step="1" {...register("remote_survey__quarter_num")} />
              {errors.remote_survey__quarter_num && <p className="text-xs text-red-600">{errors.remote_survey__quarter_num.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__request_json">Request JSON</Label>
              <Input id="remote_survey__request_json" type="text" {...register("remote_survey__request_json")} />
              {errors.remote_survey__request_json && <p className="text-xs text-red-600">{errors.remote_survey__request_json.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__create_ts">Created</Label>
              <DatePicker id="remote_survey__create_ts" value={watch("remote_survey__create_ts")} onChange={(d) => setValue("remote_survey__create_ts", d)} />
              {errors.remote_survey__create_ts && <p className="text-xs text-red-600">{errors.remote_survey__create_ts.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__is_ccp_req_generated">CCP Request Generated</Label>
              <Input id="remote_survey__is_ccp_req_generated" type="number" step="0.01" {...register("remote_survey__is_ccp_req_generated")} />
              {errors.remote_survey__is_ccp_req_generated && <p className="text-xs text-red-600">{errors.remote_survey__is_ccp_req_generated.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__rate_loc_valid_from">Rate Location Valid From</Label>
              <Input id="remote_survey__rate_loc_valid_from" type="number" step="0.01" {...register("remote_survey__rate_loc_valid_from")} />
              {errors.remote_survey__rate_loc_valid_from && <p className="text-xs text-red-600">{errors.remote_survey__rate_loc_valid_from.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__cur_valid_from">Current Valid From</Label>
              <Input id="remote_survey__cur_valid_from" type="text" {...register("remote_survey__cur_valid_from")} />
              {errors.remote_survey__cur_valid_from && <p className="text-xs text-red-600">{errors.remote_survey__cur_valid_from.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__rate_loc_remarks">Rate Location Remarks</Label>
              <Input id="remote_survey__rate_loc_remarks" type="number" step="0.01" {...register("remote_survey__rate_loc_remarks")} />
              {errors.remote_survey__rate_loc_remarks && <p className="text-xs text-red-600">{errors.remote_survey__rate_loc_remarks.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__loc_valid_from">Location Valid From</Label>
              <Input id="remote_survey__loc_valid_from" type="text" {...register("remote_survey__loc_valid_from")} />
              {errors.remote_survey__loc_valid_from && <p className="text-xs text-red-600">{errors.remote_survey__loc_valid_from.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__st1">Street 1</Label>
              <Input id="remote_survey__st1" type="text" {...register("remote_survey__st1")} />
              {errors.remote_survey__st1 && <p className="text-xs text-red-600">{errors.remote_survey__st1.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__st2">Street 2</Label>
              <Input id="remote_survey__st2" type="text" {...register("remote_survey__st2")} />
              {errors.remote_survey__st2 && <p className="text-xs text-red-600">{errors.remote_survey__st2.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__st3">Street 3</Label>
              <Input id="remote_survey__st3" type="text" {...register("remote_survey__st3")} />
              {errors.remote_survey__st3 && <p className="text-xs text-red-600">{errors.remote_survey__st3.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__city">City</Label>
              <Input id="remote_survey__city" type="text" {...register("remote_survey__city")} />
              {errors.remote_survey__city && <p className="text-xs text-red-600">{errors.remote_survey__city.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__work_state">Work State</Label>
              <Input id="remote_survey__work_state" type="text" {...register("remote_survey__work_state")} />
              {errors.remote_survey__work_state && <p className="text-xs text-red-600">{errors.remote_survey__work_state.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__zip">Zip</Label>
              <Input id="remote_survey__zip" type="text" {...register("remote_survey__zip")} />
              {errors.remote_survey__zip && <p className="text-xs text-red-600">{errors.remote_survey__zip.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__country_id">Country</Label>
              <Combobox
                id="remote_survey__country_id"
                placeholder="Select country"
                value={watch("remote_survey__country_id") || ""}
                onValueChange={(v) => setValue("remote_survey__country_id", v)}
                options={[]} />
              {errors.remote_survey__country_id && <p className="text-xs text-red-600">{errors.remote_survey__country_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__cur_work_address">Current Work Address</Label>
              <Input id="remote_survey__cur_work_address" type="text" {...register("remote_survey__cur_work_address")} />
              {errors.remote_survey__cur_work_address && <p className="text-xs text-red-600">{errors.remote_survey__cur_work_address.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__new_work_address">New Work Address</Label>
              <Input id="remote_survey__new_work_address" type="text" {...register("remote_survey__new_work_address")} />
              {errors.remote_survey__new_work_address && <p className="text-xs text-red-600">{errors.remote_survey__new_work_address.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__state_burdens">State Burdens</Label>
              <Input id="remote_survey__state_burdens" type="number" step="0.01" {...register("remote_survey__state_burdens")} />
              {errors.remote_survey__state_burdens && <p className="text-xs text-red-600">{errors.remote_survey__state_burdens.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__zipBurdens">Zip Burdens</Label>
              <Input id="remote_survey__zipBurdens" type="number" step="0.01" {...register("remote_survey__zipBurdens")} />
              {errors.remote_survey__zipBurdens && <p className="text-xs text-red-600">{errors.remote_survey__zipBurdens.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__old_burden_sick">Old Burden Sick</Label>
              <Input id="remote_survey__old_burden_sick" type="number" step="0.01" {...register("remote_survey__old_burden_sick")} />
              {errors.remote_survey__old_burden_sick && <p className="text-xs text-red-600">{errors.remote_survey__old_burden_sick.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__old_burden_sick_zip">Old Burden Sick Zip</Label>
              <Input id="remote_survey__old_burden_sick_zip" type="number" step="0.01" {...register("remote_survey__old_burden_sick_zip")} />
              {errors.remote_survey__old_burden_sick_zip && <p className="text-xs text-red-600">{errors.remote_survey__old_burden_sick_zip.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__old_burden_sick_state">Old Burden Sick State</Label>
              <Input id="remote_survey__old_burden_sick_state" type="number" step="0.01" {...register("remote_survey__old_burden_sick_state")} />
              {errors.remote_survey__old_burden_sick_state && <p className="text-xs text-red-600">{errors.remote_survey__old_burden_sick_state.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__new_burden_sick">New Burden Sick</Label>
              <Input id="remote_survey__new_burden_sick" type="number" step="0.01" {...register("remote_survey__new_burden_sick")} />
              {errors.remote_survey__new_burden_sick && <p className="text-xs text-red-600">{errors.remote_survey__new_burden_sick.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__new_burden_sick_zip">New Burden Sick Zip</Label>
              <Input id="remote_survey__new_burden_sick_zip" type="number" step="0.01" {...register("remote_survey__new_burden_sick_zip")} />
              {errors.remote_survey__new_burden_sick_zip && <p className="text-xs text-red-600">{errors.remote_survey__new_burden_sick_zip.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__new_burden_sick_state">New Burden Sick State</Label>
              <Input id="remote_survey__new_burden_sick_state" type="number" step="0.01" {...register("remote_survey__new_burden_sick_state")} />
              {errors.remote_survey__new_burden_sick_state && <p className="text-xs text-red-600">{errors.remote_survey__new_burden_sick_state.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__new_psl_jurisdicton">New PSL Jurisdiction</Label>
              <Input id="remote_survey__new_psl_jurisdicton" type="text" {...register("remote_survey__new_psl_jurisdicton")} />
              {errors.remote_survey__new_psl_jurisdicton && <p className="text-xs text-red-600">{errors.remote_survey__new_psl_jurisdicton.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__old_psl_jurisdicton">Old PSL Jurisdiction</Label>
              <Input id="remote_survey__old_psl_jurisdicton" type="text" {...register("remote_survey__old_psl_jurisdicton")} />
              {errors.remote_survey__old_psl_jurisdicton && <p className="text-xs text-red-600">{errors.remote_survey__old_psl_jurisdicton.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__home_st1">Home Street 1</Label>
              <Input id="remote_survey__home_st1" type="text" {...register("remote_survey__home_st1")} />
              {errors.remote_survey__home_st1 && <p className="text-xs text-red-600">{errors.remote_survey__home_st1.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__home_st2">Home Street 2</Label>
              <Input id="remote_survey__home_st2" type="text" {...register("remote_survey__home_st2")} />
              {errors.remote_survey__home_st2 && <p className="text-xs text-red-600">{errors.remote_survey__home_st2.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__home_st3">Home Street 3</Label>
              <Input id="remote_survey__home_st3" type="text" {...register("remote_survey__home_st3")} />
              {errors.remote_survey__home_st3 && <p className="text-xs text-red-600">{errors.remote_survey__home_st3.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__home_city">Home City</Label>
              <Input id="remote_survey__home_city" type="text" {...register("remote_survey__home_city")} />
              {errors.remote_survey__home_city && <p className="text-xs text-red-600">{errors.remote_survey__home_city.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__home_state">Home State</Label>
              <Input id="remote_survey__home_state" type="text" {...register("remote_survey__home_state")} />
              {errors.remote_survey__home_state && <p className="text-xs text-red-600">{errors.remote_survey__home_state.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__home_zip">Home Zip</Label>
              <Input id="remote_survey__home_zip" type="text" {...register("remote_survey__home_zip")} />
              {errors.remote_survey__home_zip && <p className="text-xs text-red-600">{errors.remote_survey__home_zip.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__home_country_id">Home Country</Label>
              <Combobox
                id="remote_survey__home_country_id"
                placeholder="Select country"
                value={watch("remote_survey__home_country_id") || ""}
                onValueChange={(v) => setValue("remote_survey__home_country_id", v)}
                options={[]} />
              {errors.remote_survey__home_country_id && <p className="text-xs text-red-600">{errors.remote_survey__home_country_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__cur_home_address_id">Current Home Address</Label>
              <Combobox
                id="remote_survey__cur_home_address_id"
                placeholder="Select address"
                value={watch("remote_survey__cur_home_address_id") || ""}
                onValueChange={(v) => setValue("remote_survey__cur_home_address_id", v)}
                options={[]} />
              {errors.remote_survey__cur_home_address_id && <p className="text-xs text-red-600">{errors.remote_survey__cur_home_address_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__cur_home_address">Current Home Address Text</Label>
              <Input id="remote_survey__cur_home_address" type="text" {...register("remote_survey__cur_home_address")} />
              {errors.remote_survey__cur_home_address && <p className="text-xs text-red-600">{errors.remote_survey__cur_home_address.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__new_home_address">New Home Address</Label>
              <Input id="remote_survey__new_home_address" type="text" {...register("remote_survey__new_home_address")} />
              {errors.remote_survey__new_home_address && <p className="text-xs text-red-600">{errors.remote_survey__new_home_address.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__ccp_status_id">CCP Status</Label>
              <Combobox
                id="remote_survey__ccp_status_id"
                placeholder="Select status"
                value={watch("remote_survey__ccp_status_id") || ""}
                onValueChange={(v) => setValue("remote_survey__ccp_status_id", v)}
                options={[]} />
              {errors.remote_survey__ccp_status_id && <p className="text-xs text-red-600">{errors.remote_survey__ccp_status_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__valid_from">Valid From</Label>
              <Input id="remote_survey__valid_from" type="text" {...register("remote_survey__valid_from")} />
              {errors.remote_survey__valid_from && <p className="text-xs text-red-600">{errors.remote_survey__valid_from.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__effdate">Effective Date</Label>
              <Input id="remote_survey__effdate" type="text" {...register("remote_survey__effdate")} />
              {errors.remote_survey__effdate && <p className="text-xs text-red-600">{errors.remote_survey__effdate.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__valid_to">Valid To</Label>
              <Input id="remote_survey__valid_to" type="text" {...register("remote_survey__valid_to")} />
              {errors.remote_survey__valid_to && <p className="text-xs text-red-600">{errors.remote_survey__valid_to.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__field_name">Field Name</Label>
              <Input id="remote_survey__field_name" type="text" {...register("remote_survey__field_name")} />
              {errors.remote_survey__field_name && <p className="text-xs text-red-600">{errors.remote_survey__field_name.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__worksite_addr_id">Worksite Address</Label>
              <Combobox
                id="remote_survey__worksite_addr_id"
                placeholder="Select address"
                value={watch("remote_survey__worksite_addr_id") || ""}
                onValueChange={(v) => setValue("remote_survey__worksite_addr_id", v)}
                options={[]} />
              {errors.remote_survey__worksite_addr_id && <p className="text-xs text-red-600">{errors.remote_survey__worksite_addr_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__field_label">Field Label</Label>
              <Input id="remote_survey__field_label" type="text" {...register("remote_survey__field_label")} />
              {errors.remote_survey__field_label && <p className="text-xs text-red-600">{errors.remote_survey__field_label.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="remote_survey__remarks">Remarks</Label>
              <Textarea id="remote_survey__remarks" {...register("remote_survey__remarks")} />
              {errors.remote_survey__remarks && <p className="text-xs text-red-600">{errors.remote_survey__remarks.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Training & Certifications */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Training & Certifications</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="training_type__training_type_id">Training Type</Label>
              <Combobox
                id="training_type__training_type_id"
                placeholder="Select training type"
                value={watch("training_type__training_type_id") || ""}
                onValueChange={(v) => setValue("training_type__training_type_id", v)}
                options={[]} />
              {errors.training_type__training_type_id && <p className="text-xs text-red-600">{errors.training_type__training_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__state_id">State</Label>
              <Combobox
                id="training_type__state_id"
                placeholder="Select state"
                value={watch("training_type__state_id") || ""}
                onValueChange={(v) => setValue("training_type__state_id", v)}
                options={[]} />
              {errors.training_type__state_id && <p className="text-xs text-red-600">{errors.training_type__state_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__city">City</Label>
              <Input id="training_type__city" type="text" {...register("training_type__city")} />
              {errors.training_type__city && <p className="text-xs text-red-600">{errors.training_type__city.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__onboard_doc_id">Onboard Document</Label>
              <Combobox
                id="training_type__onboard_doc_id"
                placeholder="Select document"
                value={watch("training_type__onboard_doc_id") || ""}
                onValueChange={(v) => setValue("training_type__onboard_doc_id", v)}
                options={[]} />
              {errors.training_type__onboard_doc_id && <p className="text-xs text-red-600">{errors.training_type__onboard_doc_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__date_completed">Date Completed</Label>
              <DatePicker id="training_type__date_completed" value={watch("training_type__date_completed")} onChange={(d) => setValue("training_type__date_completed", d)} />
              {errors.training_type__date_completed && <p className="text-xs text-red-600">{errors.training_type__date_completed.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__create_ts">Created</Label>
              <DatePicker id="training_type__create_ts" value={watch("training_type__create_ts")} onChange={(d) => setValue("training_type__create_ts", d)} />
              {errors.training_type__create_ts && <p className="text-xs text-red-600">{errors.training_type__create_ts.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__s_no">Serial Number</Label>
              <Input id="training_type__s_no" type="text" {...register("training_type__s_no")} />
              {errors.training_type__s_no && <p className="text-xs text-red-600">{errors.training_type__s_no.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__contractor">Contractor</Label>
              <Input id="training_type__contractor" type="text" {...register("training_type__contractor")} />
              {errors.training_type__contractor && <p className="text-xs text-red-600">{errors.training_type__contractor.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__state">State</Label>
              <Input id="training_type__state" type="text" {...register("training_type__state")} />
              {errors.training_type__state && <p className="text-xs text-red-600">{errors.training_type__state.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__customer">Customer</Label>
              <Input id="training_type__customer" type="text" {...register("training_type__customer")} />
              {errors.training_type__customer && <p className="text-xs text-red-600">{errors.training_type__customer.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__employment_type">Employment Type</Label>
              <Input id="training_type__employment_type" type="text" {...register("training_type__employment_type")} />
              {errors.training_type__employment_type && <p className="text-xs text-red-600">{errors.training_type__employment_type.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__start_date">Start Date</Label>
              <DatePicker id="training_type__start_date" value={watch("training_type__start_date")} onChange={(d) => setValue("training_type__start_date", d)} />
              {errors.training_type__start_date && <p className="text-xs text-red-600">{errors.training_type__start_date.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__cca">CCA</Label>
              <Input id="training_type__cca" type="text" {...register("training_type__cca")} />
              {errors.training_type__cca && <p className="text-xs text-red-600">{errors.training_type__cca.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__training_type">Training Type Name</Label>
              <Input id="training_type__training_type" type="text" {...register("training_type__training_type")} />
              {errors.training_type__training_type && <p className="text-xs text-red-600">{errors.training_type__training_type.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__last_date_completed">Last Date Completed</Label>
              <DatePicker id="training_type__last_date_completed" value={watch("training_type__last_date_completed")} onChange={(d) => setValue("training_type__last_date_completed", d)} />
              {errors.training_type__last_date_completed && <p className="text-xs text-red-600">{errors.training_type__last_date_completed.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="training_type__due_in">Due In</Label>
              <Input id="training_type__due_in" type="text" {...register("training_type__due_in")} />
              {errors.training_type__due_in && <p className="text-xs text-red-600">{errors.training_type__due_in.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Vaccination Disclosure */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Vaccination Disclosure</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="vaccination_answers__vaccination_status_type_id">Vaccination Status</Label>
              <Combobox
                id="vaccination_answers__vaccination_status_type_id"
                placeholder="Select status"
                value={watch("vaccination_answers__vaccination_status_type_id") || ""}
                onValueChange={(v) => setValue("vaccination_answers__vaccination_status_type_id", v)}
                options={[]} />
              {errors.vaccination_answers__vaccination_status_type_id && <p className="text-xs text-red-600">{errors.vaccination_answers__vaccination_status_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="vaccination_answers__document_id">Document</Label>
              <Combobox
                id="vaccination_answers__document_id"
                placeholder="Select document"
                value={watch("vaccination_answers__document_id") || ""}
                onValueChange={(v) => setValue("vaccination_answers__document_id", v)}
                options={[]} />
              {errors.vaccination_answers__document_id && <p className="text-xs text-red-600">{errors.vaccination_answers__document_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="vaccination_answers__vac_date">Vaccination Date</Label>
              <DatePicker id="vaccination_answers__vac_date" value={watch("vaccination_answers__vac_date")} onChange={(d) => setValue("vaccination_answers__vac_date", d)} />
              {errors.vaccination_answers__vac_date && <p className="text-xs text-red-600">{errors.vaccination_answers__vac_date.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="vaccination_answers__created_by_id">Created By</Label>
              <Combobox
                id="vaccination_answers__created_by_id"
                placeholder="Select user"
                value={watch("vaccination_answers__created_by_id") || ""}
                onValueChange={(v) => setValue("vaccination_answers__created_by_id", v)}
                options={[]} />
              {errors.vaccination_answers__created_by_id && <p className="text-xs text-red-600">{errors.vaccination_answers__created_by_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="vaccination_answers__create_ts">Created</Label>
              <DatePicker id="vaccination_answers__create_ts" value={watch("vaccination_answers__create_ts")} onChange={(d) => setValue("vaccination_answers__create_ts", d)} />
              {errors.vaccination_answers__create_ts && <p className="text-xs text-red-600">{errors.vaccination_answers__create_ts.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Vendor Assignment */}
      <Card className="shadow-sm">
        <CardHeader>
          <CardTitle>Vendor Assignment</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="vendor__vendor_id">Vendor</Label>
              <Combobox
                id="vendor__vendor_id"
                placeholder="Select vendor"
                value={watch("vendor__vendor_id") || ""}
                onValueChange={(v) => setValue("vendor__vendor_id", v)}
                options={[]} />
              {errors.vendor__vendor_id && <p className="text-xs text-red-600">{errors.vendor__vendor_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="vendor__vendor_name">Vendor Name</Label>
              <Input id="vendor__vendor_name" type="text" {...register("vendor__vendor_name")} />
              {errors.vendor__vendor_name && <p className="text-xs text-red-600">{errors.vendor__vendor_name.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {serverError && (
        <div className="rounded-md border border-red-200 bg-red-50 text-red-800 text-sm px-3 py-2">
          {serverError}
        </div>
      )}

      <div className="flex items-center justify-end gap-2 pt-2 border-t border-slate-100">
        <Button type="submit" disabled={isSubmitting} className="bg-violet-600 hover:bg-violet-700 text-white">
          {isSubmitting ? <Loader2 className="h-4 w-4 animate-spin mr-2" /> : <Save className="h-4 w-4 mr-2" />}
          Create Placement
        </Button>
      </div>
    </form>
  )
}
