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

const schema = z.object({
  email: z.string().optional(),
  firstname: z.string().optional(),
  lastname: z.string().optional(),
  contact_email: z.string().optional(),
  contact_firstname: z.string().optional(),
  contact_lastname: z.string().optional(),
  country_id: z.string().min(1, "Country is required"),
  PRIMARY_office_id: z.string().min(1, "Primary office is required"),
  end_date: z.string().datetime(),
  rp_valid_from: z.string().optional(),
  start_date: z.string().datetime(),
  assignment_type_id: z.string().min(1, "Assignment type is required"),
  contractor_placement_id: z.string().min(1, "Contractor placement ID is required"),
  employee_id: z.string().min(1, "Employee ID is required"),
  employment_type_id: z.string().min(1, "Employment type is required"),
  is_nonexempt: z.boolean(),
  job_title_id: z.string().min(1, "Job title is required"),
  is_passthru: z.boolean(),
  pay_freq_type_id: z.string().min(1, "Pay frequency type is required"),
  hr_payrate_ot: z.string().optional(),
  hr_payrate_st: z.string().optional(),
  referal_fee: z.string().optional(),
  valid_from: z.string().optional(),
  valid_to: z.string().optional(),
  pct_vendor_discount: z.string().optional(),
  day_per_diem: z.string().optional(),
  fts_hr_burden: z.string().optional(),
  hrs_worked_type_id: z.string().min(1, "Hours worked type is required"),
  hr_burden_dt: z.string().optional(),
  hr_burden_ot: z.string().optional(),
  hr_burden: z.string().optional(),
  hr_facility_fee: z.string().optional(),
  hr_fringe_benefit: z.string().optional(),
  hr_per_diem: z.string().optional(),
  pct_discount_inv: z.string().optional(),
  pci_total_cost: z.coerce.number().optional(),
  hr_payrate_dt: z.string().optional(),
  payrate_given_fts: z.string().optional(),
  burden: z.string().optional(),
  burden_sick: z.string().optional(),
  burden_sick_state: z.string().optional(),
  burden_sick_zip: z.string().optional(),
  profile_id: z.string().min(1, "Profile ID is required"),
  recent_profile: z.string().optional(),
  referal_fee_dt: z.string().optional(),
  referal_fee_ot: z.string().optional(),
  referal_fee_st: z.string().optional(),
  pct_discount: z.string().optional(),
  pct_vms_fee: z.string().optional(),
  amt_vendor_rate_reduction: z.string().optional(),
  pct_vendor_rate_reduction: z.string().optional(),
  pct_discount_vol: z.string().optional(),
  waiver_fee: z.string().optional(),
  bill_unit_type_id: z.string().min(1, "Bill unit type is required"),
  hr_billrate_dt: z.string().optional(),
  hr_billrate_ot: z.string().optional(),
  hr_billrate_st: z.string().optional(),
  apply_pct: z.string().optional(),
  field_label: z.string().optional(),
  field_name: z.string().optional(),
  file: z.string().optional(),
  pt_emp_ids: z.string().optional(),
  referred_by: z.string().optional(),
  remarks: z.string().optional(),
  rp_remarks: z.string().optional(),
})
type FormValues = z.infer<typeof schema>

export default function ContractorPlacementForm() {
  const router = useRouter()
  const [serverError, setServerError] = useState("")
  const { register, handleSubmit, formState: { errors, isSubmitting } } = useForm<FormValues>({ resolver: zodResolver(schema) })

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
    router.push("/ui/contractor-placement")
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
        <div>
          <Label htmlFor="email">Email</Label>
          <Input type="text" {...register("email")} />
        </div>
        <div>
          <Label htmlFor="firstname">First Name</Label>
          <Input type="text" {...register("firstname")} />
        </div>
        <div>
          <Label htmlFor="lastname">Last Name</Label>
          <Input type="text" {...register("lastname")} />
        </div>
        <div>
          <Label htmlFor="contact_email">Contact Email</Label>
          <Input type="text" {...register("contact_email")} />
        </div>
        <div>
          <Label htmlFor="contact_firstname">Contact First Name</Label>
          <Input type="text" {...register("contact_firstname")} />
        </div>
        <div>
          <Label htmlFor="contact_lastname">Contact Last Name</Label>
          <Input type="text" {...register("contact_lastname")} />
        </div>
        <div>
          <Label htmlFor="country_id">Country</Label>
          <Input type="text" {...register("country_id")} />
        </div>
        <div>
          <Label htmlFor="PRIMARY_office_id">Primary Office</Label>
          <Input type="text" {...register("PRIMARY_office_id")} />
        </div>
        <div>
          <Label htmlFor="end_date">End Date</Label>
          <Input type="datetime-local" {...register("end_date")} />
        </div>
        <div>
          <Label htmlFor="rp_valid_from">RP Valid From</Label>
          <Input type="text" {...register("rp_valid_from")} />
        </div>
        <div>
          <Label htmlFor="start_date">Start Date</Label>
          <Input type="datetime-local" {...register("start_date")} />
        </div>
        <div>
          <Label htmlFor="assignment_type_id">Assignment Type</Label>
          <Input type="text" {...register("assignment_type_id")} />
        </div>
        <div>
          <Label htmlFor="contractor_placement_id">Contractor Placement ID</Label>
          <Input type="text" {...register("contractor_placement_id")} />
        </div>
        <div>
          <Label htmlFor="employee_id">Employee ID</Label>
          <Input type="text" {...register("employee_id")} />
        </div>
        <div>
          <Label htmlFor="employment_type_id">Employment Type</Label>
          <Input type="text" {...register("employment_type_id")} />
        </div>
        <div>
          <Label htmlFor="is_nonexempt">Is Nonexempt</Label>
          <Input type="checkbox" {...register("is_nonexempt")} />
        </div>
        <div>
          <Label htmlFor="job_title_id">Job Title</Label>
          <Input type="text" {...register("job_title_id")} />
        </div>
        <div>
          <Label htmlFor="is_passthru">Is Passthru</Label>
          <Input type="checkbox" {...register("is_passthru")} />
        </div>
        <div>
          <Label htmlFor="pay_freq_type_id">Pay Frequency Type</Label>
          <Input type="text" {...register("pay_freq_type_id")} />
        </div>
        <div>
          <Label htmlFor="hr_payrate_ot">HR Pay Rate OT</Label>
          <Input type="text" {...register("hr_payrate_ot")} />
        </div>
        <div>
          <Label htmlFor="hr_payrate_st">HR Pay Rate ST</Label>
          <Input type="text" {...register("hr_payrate_st")} />
        </div>
        <div>
          <Label htmlFor="referal_fee">Referral Fee</Label>
          <Input type="text" {...register("referal_fee")} />
        </div>
        <div>
          <Label htmlFor="valid_from">Valid From</Label>
          <Input type="text" {...register("valid_from")} />
        </div>
        <div>
          <Label htmlFor="valid_to">Valid To</Label>
          <Input type="text" {...register("valid_to")} />
        </div>
        <div>
          <Label htmlFor="pct_vendor_discount">Percentage Vendor Discount</Label>
          <Input type="text" {...register("pct_vendor_discount")} />
        </div>
        <div>
          <Label htmlFor="day_per_diem">Day Per Diem</Label>
          <Input type="text" {...register("day_per_diem")} />
        </div>
        <div>
          <Label htmlFor="fts_hr_burden">FTS HR Burden</Label>
          <Input type="text" {...register("fts_hr_burden")} />
        </div>
        <div>
          <Label htmlFor="hrs_worked_type_id">Hours Worked Type</Label>
          <Input type="text" {...register("hrs_worked_type_id")} />
        </div>
        <div>
          <Label htmlFor="hr_burden_dt">HR Burden DT</Label>
          <Input type="text" {...register("hr_burden_dt")} />
        </div>
        <div>
          <Label htmlFor="hr_burden_ot">HR Burden OT</Label>
          <Input type="text" {...register("hr_burden_ot")} />
        </div>
        <div>
          <Label htmlFor="hr_burden">HR Burden</Label>
          <Input type="text" {...register("hr_burden")} />
        </div>
        <div>
          <Label htmlFor="hr_facility_fee">HR Facility Fee</Label>
          <Input type="text" {...register("hr_facility_fee")} />
        </div>
        <div>
          <Label htmlFor="hr_fringe_benefit">HR Fringe Benefit</Label>
          <Input type="text" {...register("hr_fringe_benefit")} />
        </div>
        <div>
          <Label htmlFor="hr_per_diem">HR Per Diem</Label>
          <Input type="text" {...register("hr_per_diem")} />
        </div>
        <div>
          <Label htmlFor="pct_discount_inv">Percentage Discount Invoice</Label>
          <Input type="text" {...register("pct_discount_inv")} />
        </div>
        <div>
          <Label htmlFor="pci_total_cost">PCI Total Cost</Label>
          <Input type="number" {...register("pci_total_cost")} />
        </div>
        <div>
          <Label htmlFor="hr_payrate_dt">HR Pay Rate DT</Label>
          <Input type="text" {...register("hr_payrate_dt")} />
        </div>
        <div>
          <Label htmlFor="payrate_given_fts">Pay Rate Given FTS</Label>
          <Input type="text" {...register("payrate_given_fts")} />
        </div>
        <div>
          <Label htmlFor="burden">Burden</Label>
          <Input type="text" {...register("burden")} />
        </div>
        <div>
          <Label htmlFor="burden_sick">Burden Sick</Label>
          <Input type="text" {...register("burden_sick")} />
        </div>
        <div>
          <Label htmlFor="burden_sick_state">Burden Sick State</Label>
          <Input type="text" {...register("burden_sick_state")} />
        </div>
        <div>
          <Label htmlFor="burden_sick_zip">Burden Sick Zip</Label>
          <Input type="text" {...register("burden_sick_zip")} />
        </div>
        <div>
          <Label htmlFor="profile_id">Profile ID</Label>
          <Input type="text" {...register("profile_id")} />
        </div>
        <div>
          <Label htmlFor="recent_profile">Recent Profile</Label>
          <Input type="text" {...register("recent_profile")} />
        </div>
        <div>
          <Label htmlFor="referal_fee_dt">Referral Fee DT</Label>
          <Input type="text" {...register("referal_fee_dt")} />
        </div>
        <div>
          <Label htmlFor="referal_fee_ot">Referral Fee OT</Label>
          <Input type="text" {...register("referal_fee_ot")} />
        </div>
        <div>
          <Label htmlFor="referal_fee_st">Referral Fee ST</Label>
          <Input type="text" {...register("referal_fee_st")} />
        </div>
        <div>
          <Label htmlFor="pct_discount">Percentage Discount</Label>
          <Input type="text" {...register("pct_discount")} />
        </div>
        <div>
          <Label htmlFor="pct_vms_fee">Percentage VMS Fee</Label>
          <Input type="text" {...register("pct_vms_fee")} />
        </div>
        <div>
          <Label htmlFor="amt_vendor_rate_reduction">Amount Vendor Rate Reduction</Label>
          <Input type="text" {...register("amt_vendor_rate_reduction")} />
        </div>
        <div>
          <Label htmlFor="pct_vendor_rate_reduction">Percentage Vendor Rate Reduction</Label>
          <Input type="text" {...register("pct_vendor_rate_reduction")} />
        </div>
        <div>
          <Label htmlFor="pct_discount_vol">Percentage Discount Volume</Label>
          <Input type="text" {...register("pct_discount_vol")} />
        </div>
        <div>
          <Label htmlFor="waiver_fee">Waiver Fee</Label>
          <Input type="text" {...register("waiver_fee")} />
        </div>
        <div>
          <Label htmlFor="bill_unit_type_id">Bill Unit Type</Label>
          <Input type="text" {...register("bill_unit_type_id")} />
        </div>
        <div>
          <Label htmlFor="hr_billrate_dt">HR Bill Rate DT</Label>
          <Input type="text" {...register("hr_billrate_dt")} />
        </div>
        <div>
          <Label htmlFor="hr_billrate_ot">HR Bill Rate OT</Label>
          <Input type="text" {...register("hr_billrate_ot")} />
        </div>
        <div>
          <Label htmlFor="hr_billrate_st">HR Bill Rate ST</Label>
          <Input type="text" {...register("hr_billrate_st")} />
        </div>
        <div>
          <Label htmlFor="apply_pct">Apply Percentage</Label>
          <Input type="text" {...register("apply_pct")} />
        </div>
        <div>
          <Label htmlFor="field_label">Field Label</Label>
          <Input type="text" {...register("field_label")} />
        </div>
        <div>
          <Label htmlFor="field_name">Field Name</Label>
          <Input type="text" {...register("field_name")} />
        </div>
        <div>
          <Label htmlFor="file">File</Label>
          <Input type="text" {...register("file")} />
        </div>
        <div>
          <Label htmlFor="pt_emp_ids">PT Employee IDs</Label>
          <Input type="text" {...register("pt_emp_ids")} />
        </div>
        <div>
          <Label htmlFor="referred_by">Referred By</Label>
          <Input type="text" {...register("referred_by")} />
        </div>
        <div>
          <Label htmlFor="remarks">Remarks</Label>
          <Input type="text" {...register("remarks")} />
        </div>
        <div>
          <Label htmlFor="rp_remarks">RP Remarks</Label>
          <Input type="text" {...register("rp_remarks")} />
        </div>
      </div>
      {serverError && (
        <div className="rounded-md border border-red-200 bg-red-50 text-red-800 text-sm px-3 py-2">{serverError}</div>
      )}
      <div className="flex items-center justify-end gap-2 pt-2 border-t border-slate-100">
        <Button type="submit" disabled={isSubmitting} className="bg-violet-600 hover:bg-violet-700 text-white">
          {isSubmitting ? <Loader2 className="h-4 w-4 animate-spin mr-2" /> : <Save className="h-4 w-4 mr-2" />}
          Save Contractor Placement
        </Button>
      </div>
    </form>
  )
}
