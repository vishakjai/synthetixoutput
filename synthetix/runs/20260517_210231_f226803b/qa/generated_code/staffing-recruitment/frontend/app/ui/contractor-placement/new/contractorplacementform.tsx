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

// BR-CTR-001 — Rate must be within client engagement's contracted ceiling unless co-approver has override authority.
// BR-CTR-002 — Start date must be at least 5 business days from creation date.
const schema = z.object({
  email: z.string().email().optional().or(z.literal("")),
  firstname: z.string().optional(),
  lastname: z.string().optional(),
  contact_email: z.string().email().optional().or(z.literal("")),
  contact_firstname: z.string().optional(),
  contact_lastname: z.string().optional(),
  country_id: z.string().min(1, "Country is required"),
  PRIMARY_office_id: z.string().min(1, "Primary office is required"),
  end_date: z.string().datetime("End date must be a valid datetime"),
  rp_valid_from: z.string().optional(),
  start_date: z.string().datetime("Start date must be a valid datetime"),
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
  profile_id: z.string().min(1, "Profile is required"),
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
}).superRefine((v, ctx) => {
  // BR-CTR-002 — Start date must be at least 5 business days from creation date
  if (v.start_date) {
    const startDate = new Date(v.start_date)
    const today = new Date()
    const daysUntilStart = Math.floor((startDate.getTime() - today.getTime()) / (1000 * 60 * 60 * 24))
    // Approximate 5 business days as 7 calendar days
    if (daysUntilStart < 7) {
      ctx.addIssue({
        code: z.ZodIssueCode.custom,
        path: ["start_date"],
        message: "Start date must be at least 5 business days from creation date",
      })
    }
  }
})

type FormValues = z.infer<typeof schema>

export default function ContractorPlacementForm() {
  const router = useRouter()
  const [serverError, setServerError] = useState("")
  const { register, handleSubmit, formState: { errors, isSubmitting } } =
    useForm<FormValues>({
      resolver: zodResolver(schema),
      defaultValues: {
        is_nonexempt: false,
        is_passthru: false,
      },
    })

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
      {/* Contractor Identity */}
      <Card className="shadow-md">
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
      <Card className="shadow-md">
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
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Address</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="country_id">Country<span className="text-red-600 ml-1">*</span></Label>
              <Input id="country_id" type="text" {...register("country_id")} />
              {errors.country_id && <p className="text-xs text-red-600">{errors.country_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Ownership */}
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Ownership</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="PRIMARY_office_id">Primary Office<span className="text-red-600 ml-1">*</span></Label>
              <Input id="PRIMARY_office_id" type="text" {...register("PRIMARY_office_id")} />
              {errors.PRIMARY_office_id && <p className="text-xs text-red-600">{errors.PRIMARY_office_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Dates */}
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Dates</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="end_date">End Date<span className="text-red-600 ml-1">*</span></Label>
              <Input id="end_date" type="datetime-local" {...register("end_date")} />
              {errors.end_date && <p className="text-xs text-red-600">{errors.end_date.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="rp_valid_from">RP Valid From</Label>
              <Input id="rp_valid_from" type="text" {...register("rp_valid_from")} />
              {errors.rp_valid_from && <p className="text-xs text-red-600">{errors.rp_valid_from.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="start_date">Start Date<span className="text-red-600 ml-1">*</span></Label>
              <Input id="start_date" type="datetime-local" {...register("start_date")} />
              {errors.start_date && <p className="text-xs text-red-600">{errors.start_date.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Identifiers */}
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Identifiers</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="assignment_type_id">Assignment Type<span className="text-red-600 ml-1">*</span></Label>
              <Input id="assignment_type_id" type="text" {...register("assignment_type_id")} />
              {errors.assignment_type_id && <p className="text-xs text-red-600">{errors.assignment_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="contractor_placement_id">Contractor Placement ID<span className="text-red-600 ml-1">*</span></Label>
              <Input id="contractor_placement_id" type="text" {...register("contractor_placement_id")} />
              {errors.contractor_placement_id && <p className="text-xs text-red-600">{errors.contractor_placement_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="employee_id">Employee ID<span className="text-red-600 ml-1">*</span></Label>
              <Input id="employee_id" type="text" {...register("employee_id")} />
              {errors.employee_id && <p className="text-xs text-red-600">{errors.employee_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="employment_type_id">Employment Type<span className="text-red-600 ml-1">*</span></Label>
              <Input id="employment_type_id" type="text" {...register("employment_type_id")} />
              {errors.employment_type_id && <p className="text-xs text-red-600">{errors.employment_type_id.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Rates & Pay */}
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Rates & Pay</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="flex items-center gap-3">
              <input id="is_nonexempt" type="checkbox" {...register("is_nonexempt")} className="h-4 w-4 rounded border-slate-300 text-violet-600 focus:ring-violet-500" />
              <Label htmlFor="is_nonexempt">Non-Exempt</Label>
            </div>
            <div className="space-y-1">
              <Label htmlFor="job_title_id">Job Title<span className="text-red-600 ml-1">*</span></Label>
              <Input id="job_title_id" type="text" {...register("job_title_id")} />
              {errors.job_title_id && <p className="text-xs text-red-600">{errors.job_title_id.message}</p>}
            </div>
            <div className="flex items-center gap-3">
              <input id="is_passthru" type="checkbox" {...register("is_passthru")} className="h-4 w-4 rounded border-slate-300 text-violet-600 focus:ring-violet-500" />
              <Label htmlFor="is_passthru">Pass-Through</Label>
            </div>
            <div className="space-y-1">
              <Label htmlFor="pay_freq_type_id">Pay Frequency Type<span className="text-red-600 ml-1">*</span></Label>
              <Input id="pay_freq_type_id" type="text" {...register("pay_freq_type_id")} />
              {errors.pay_freq_type_id && <p className="text-xs text-red-600">{errors.pay_freq_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_payrate_ot">HR Pay Rate OT</Label>
              <Input id="hr_payrate_ot" type="text" {...register("hr_payrate_ot")} />
              {errors.hr_payrate_ot && <p className="text-xs text-red-600">{errors.hr_payrate_ot.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_payrate_st">HR Pay Rate ST</Label>
              <Input id="hr_payrate_st" type="text" {...register("hr_payrate_st")} />
              {errors.hr_payrate_st && <p className="text-xs text-red-600">{errors.hr_payrate_st.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referal_fee">Referral Fee</Label>
              <Input id="referal_fee" type="text" {...register("referal_fee")} />
              {errors.referal_fee && <p className="text-xs text-red-600">{errors.referal_fee.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="valid_from">Valid From</Label>
              <Input id="valid_from" type="text" {...register("valid_from")} />
              {errors.valid_from && <p className="text-xs text-red-600">{errors.valid_from.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="valid_to">Valid To</Label>
              <Input id="valid_to" type="text" {...register("valid_to")} />
              {errors.valid_to && <p className="text-xs text-red-600">{errors.valid_to.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_vendor_discount">Vendor Discount %</Label>
              <Input id="pct_vendor_discount" type="text" {...register("pct_vendor_discount")} />
              {errors.pct_vendor_discount && <p className="text-xs text-red-600">{errors.pct_vendor_discount.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="day_per_diem">Daily Per Diem</Label>
              <Input id="day_per_diem" type="text" {...register("day_per_diem")} />
              {errors.day_per_diem && <p className="text-xs text-red-600">{errors.day_per_diem.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="fts_hr_burden">FTS HR Burden</Label>
              <Input id="fts_hr_burden" type="text" {...register("fts_hr_burden")} />
              {errors.fts_hr_burden && <p className="text-xs text-red-600">{errors.fts_hr_burden.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hrs_worked_type_id">Hours Worked Type<span className="text-red-600 ml-1">*</span></Label>
              <Input id="hrs_worked_type_id" type="text" {...register("hrs_worked_type_id")} />
              {errors.hrs_worked_type_id && <p className="text-xs text-red-600">{errors.hrs_worked_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_burden_dt">HR Burden DT</Label>
              <Input id="hr_burden_dt" type="text" {...register("hr_burden_dt")} />
              {errors.hr_burden_dt && <p className="text-xs text-red-600">{errors.hr_burden_dt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_burden_ot">HR Burden OT</Label>
              <Input id="hr_burden_ot" type="text" {...register("hr_burden_ot")} />
              {errors.hr_burden_ot && <p className="text-xs text-red-600">{errors.hr_burden_ot.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_burden">HR Burden</Label>
              <Input id="hr_burden" type="text" {...register("hr_burden")} />
              {errors.hr_burden && <p className="text-xs text-red-600">{errors.hr_burden.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_facility_fee">HR Facility Fee</Label>
              <Input id="hr_facility_fee" type="text" {...register("hr_facility_fee")} />
              {errors.hr_facility_fee && <p className="text-xs text-red-600">{errors.hr_facility_fee.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_fringe_benefit">HR Fringe Benefit</Label>
              <Input id="hr_fringe_benefit" type="text" {...register("hr_fringe_benefit")} />
              {errors.hr_fringe_benefit && <p className="text-xs text-red-600">{errors.hr_fringe_benefit.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_per_diem">HR Per Diem</Label>
              <Input id="hr_per_diem" type="text" {...register("hr_per_diem")} />
              {errors.hr_per_diem && <p className="text-xs text-red-600">{errors.hr_per_diem.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_discount_inv">Discount % Invoice</Label>
              <Input id="pct_discount_inv" type="text" {...register("pct_discount_inv")} />
              {errors.pct_discount_inv && <p className="text-xs text-red-600">{errors.pct_discount_inv.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pci_total_cost">Total Cost</Label>
              <Input id="pci_total_cost" type="number" step="0.01" {...register("pci_total_cost")} />
              {errors.pci_total_cost && <p className="text-xs text-red-600">{errors.pci_total_cost.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_payrate_dt">HR Pay Rate DT</Label>
              <Input id="hr_payrate_dt" type="text" {...register("hr_payrate_dt")} />
              {errors.hr_payrate_dt && <p className="text-xs text-red-600">{errors.hr_payrate_dt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="payrate_given_fts">Pay Rate Given FTS</Label>
              <Input id="payrate_given_fts" type="text" {...register("payrate_given_fts")} />
              {errors.payrate_given_fts && <p className="text-xs text-red-600">{errors.payrate_given_fts.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="burden">Burden</Label>
              <Input id="burden" type="text" {...register("burden")} />
              {errors.burden && <p className="text-xs text-red-600">{errors.burden.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="burden_sick">Burden Sick</Label>
              <Input id="burden_sick" type="text" {...register("burden_sick")} />
              {errors.burden_sick && <p className="text-xs text-red-600">{errors.burden_sick.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="burden_sick_state">Burden Sick State</Label>
              <Input id="burden_sick_state" type="text" {...register("burden_sick_state")} />
              {errors.burden_sick_state && <p className="text-xs text-red-600">{errors.burden_sick_state.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="burden_sick_zip">Burden Sick Zip</Label>
              <Input id="burden_sick_zip" type="text" {...register("burden_sick_zip")} />
              {errors.burden_sick_zip && <p className="text-xs text-red-600">{errors.burden_sick_zip.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="profile_id">Profile<span className="text-red-600 ml-1">*</span></Label>
              <Input id="profile_id" type="text" {...register("profile_id")} />
              {errors.profile_id && <p className="text-xs text-red-600">{errors.profile_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="recent_profile">Recent Profile</Label>
              <Input id="recent_profile" type="text" {...register("recent_profile")} />
              {errors.recent_profile && <p className="text-xs text-red-600">{errors.recent_profile.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referal_fee_dt">Referral Fee DT</Label>
              <Input id="referal_fee_dt" type="text" {...register("referal_fee_dt")} />
              {errors.referal_fee_dt && <p className="text-xs text-red-600">{errors.referal_fee_dt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referal_fee_ot">Referral Fee OT</Label>
              <Input id="referal_fee_ot" type="text" {...register("referal_fee_ot")} />
              {errors.referal_fee_ot && <p className="text-xs text-red-600">{errors.referal_fee_ot.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="referal_fee_st">Referral Fee ST</Label>
              <Input id="referal_fee_st" type="text" {...register("referal_fee_st")} />
              {errors.referal_fee_st && <p className="text-xs text-red-600">{errors.referal_fee_st.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_discount">Discount %</Label>
              <Input id="pct_discount" type="text" {...register("pct_discount")} />
              {errors.pct_discount && <p className="text-xs text-red-600">{errors.pct_discount.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_vms_fee">VMS Fee %</Label>
              <Input id="pct_vms_fee" type="text" {...register("pct_vms_fee")} />
              {errors.pct_vms_fee && <p className="text-xs text-red-600">{errors.pct_vms_fee.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="amt_vendor_rate_reduction">Vendor Rate Reduction Amount</Label>
              <Input id="amt_vendor_rate_reduction" type="text" {...register("amt_vendor_rate_reduction")} />
              {errors.amt_vendor_rate_reduction && <p className="text-xs text-red-600">{errors.amt_vendor_rate_reduction.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_vendor_rate_reduction">Vendor Rate Reduction %</Label>
              <Input id="pct_vendor_rate_reduction" type="text" {...register("pct_vendor_rate_reduction")} />
              {errors.pct_vendor_rate_reduction && <p className="text-xs text-red-600">{errors.pct_vendor_rate_reduction.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="pct_discount_vol">Volume Discount %</Label>
              <Input id="pct_discount_vol" type="text" {...register("pct_discount_vol")} />
              {errors.pct_discount_vol && <p className="text-xs text-red-600">{errors.pct_discount_vol.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="waiver_fee">Waiver Fee</Label>
              <Input id="waiver_fee" type="text" {...register("waiver_fee")} />
              {errors.waiver_fee && <p className="text-xs text-red-600">{errors.waiver_fee.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Billing */}
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Billing</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="bill_unit_type_id">Bill Unit Type<span className="text-red-600 ml-1">*</span></Label>
              <Input id="bill_unit_type_id" type="text" {...register("bill_unit_type_id")} />
              {errors.bill_unit_type_id && <p className="text-xs text-red-600">{errors.bill_unit_type_id.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_billrate_dt">HR Bill Rate DT</Label>
              <Input id="hr_billrate_dt" type="text" {...register("hr_billrate_dt")} />
              {errors.hr_billrate_dt && <p className="text-xs text-red-600">{errors.hr_billrate_dt.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_billrate_ot">HR Bill Rate OT</Label>
              <Input id="hr_billrate_ot" type="text" {...register("hr_billrate_ot")} />
              {errors.hr_billrate_ot && <p className="text-xs text-red-600">{errors.hr_billrate_ot.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="hr_billrate_st">HR Bill Rate ST</Label>
              <Input id="hr_billrate_st" type="text" {...register("hr_billrate_st")} />
              {errors.hr_billrate_st && <p className="text-xs text-red-600">{errors.hr_billrate_st.message}</p>}
            </div>
          </div>
        </CardContent>
      </Card>

      {/* Other */}
      <Card className="shadow-md">
        <CardHeader>
          <CardTitle>Other</CardTitle>
        </CardHeader>
        <CardContent>
          <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
            <div className="space-y-1">
              <Label htmlFor="apply_pct">Apply %</Label>
              <Input id="apply_pct" type="text" {...register("apply_pct")} />
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
              <Input id="file" type="text" {...register("file")} />
              {errors.file && <p className="text-xs text-red-600">{errors.file.message}</p>}
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
              <Input id="remarks" type="text" {...register("remarks")} />
              {errors.remarks && <p className="text-xs text-red-600">{errors.remarks.message}</p>}
            </div>
            <div className="space-y-1">
              <Label htmlFor="rp_remarks">RP Remarks</Label>
              <Input id="rp_remarks" type="text" {...register("rp_remarks")} />
              {errors.rp_remarks && <p className="text-xs text-red-600">{errors.rp_remarks.message}</p>}
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
          Save Placement
        </Button>
      </div>
    </form>
  )
}
