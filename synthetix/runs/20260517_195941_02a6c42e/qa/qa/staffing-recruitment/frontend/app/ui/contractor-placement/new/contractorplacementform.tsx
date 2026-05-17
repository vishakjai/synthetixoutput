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

// BR-CTR-001 — Rate must be within client engagement's contracted ceiling unless co-approver has override authority.
// BR-CTR-002 — Start date must be at least 5 business days from creation date.
const schema = z.object({
  email: z.string().email().optional(),
  firstname: z.string().optional(),
  lastname: z.string().optional(),
  contact_email: z.string().email().optional(),
  contact_firstname: z.string().optional(),
  contact_lastname: z.string().optional(),
  country_id: z.string(),
  PRIMARY_office_id: z.string(),
  end_date: z.string().datetime(),
  rp_valid_from: z.string().optional(),
  start_date: z.string().datetime(),
  assignment_type_id: z.string(),
  contractor_placement_id: z.string(),
  employee_id: z.string(),
  employment_type_id: z.string(),
  is_nonexempt: z.boolean(),
  job_title_id: z.string(),
  is_passthru: z.boolean(),
  pay_freq_type_id: z.string(),
  hr_payrate_ot: z.string().optional(),
  hr_payrate_st: z.string().optional(),
  referal_fee: z.string().optional(),
  valid_from: z.string().optional(),
  valid_to: z.string().optional(),
  pct_vendor_discount: z.string().optional(),
  day_per_diem: z.string().optional(),
  fts_hr_burden: z.string().optional(),
  hrs_worked_type_id: z.string(),
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
  profile_id: z.string(),
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
  bill_unit_type_id: z.string(),
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
  // BR-CTR-002 — Start date constraint
  const startDate = new Date(v.start_date)
  const today = new Date()
  const minStartDate = new Date(today.setDate(today.getDate() + 7))
  if (startDate < minStartDate) {
    ctx.addIssue({
      code: z.ZodIssueCode.custom,
      path: ["start_date"],
      message: "Start date must be at least 5 business days from creation date",
    })
  }
})

type FormValues = z.infer<typeof schema>

export default function ContractorPlacementForm() {
  const router = useRouter()
  const [serverError, setServerError] = useState("")
  const { register, handleSubmit, formState: { errors, isSubmitting } } =
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
      <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
        <div className="space-y-1">
          <Label htmlFor="email">Email</Label>
          <Input id="email" type="email" {...register("email")} />
          {errors.email && <p className="text-xs text-red-600">{errors.email.message}</p>}
        </div>
        <div className="space-y-1">
          <Label htmlFor="firstname">First Name</Label>
          <Input id="firstname" type="text" {...register("firstname")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="lastname">Last Name</Label>
          <Input id="lastname" type="text" {...register("lastname")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="contact_email">Contact Email</Label>
          <Input id="contact_email" type="email" {...register("contact_email")} />
          {errors.contact_email && <p className="text-xs text-red-600">{errors.contact_email.message}</p>}
        </div>
        <div className="space-y-1">
          <Label htmlFor="contact_firstname">Contact First Name</Label>
          <Input id="contact_firstname" type="text" {...register("contact_firstname")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="contact_lastname">Contact Last Name</Label>
          <Input id="contact_lastname" type="text" {...register("contact_lastname")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="country_id">Country ID</Label>
          <Input id="country_id" type="text" {...register("country_id")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="PRIMARY_office_id">Primary Office ID</Label>
          <Input id="PRIMARY_office_id" type="text" {...register("PRIMARY_office_id")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="end_date">End Date</Label>
          <Input id="end_date" type="datetime-local" {...register("end_date")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="start_date">Start Date</Label>
          <Input id="start_date" type="datetime-local" {...register("start_date")} />
          {errors.start_date && <p className="text-xs text-red-600">{errors.start_date.message}</p>}
        </div>
        <div className="space-y-1">
          <Label htmlFor="assignment_type_id">Assignment Type ID</Label>
          <Input id="assignment_type_id" type="text" {...register("assignment_type_id")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="contractor_placement_id">Contractor Placement ID</Label>
          <Input id="contractor_placement_id" type="text" {...register("contractor_placement_id")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="employee_id">Employee ID</Label>
          <Input id="employee_id" type="text" {...register("employee_id")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="employment_type_id">Employment Type ID</Label>
          <Input id="employment_type_id" type="text" {...register("employment_type_id")} />
        </div>
        <div className="flex items-center gap-3">
          <input id="is_nonexempt" type="checkbox" {...register("is_nonexempt")} className="h-4 w-4 rounded border-slate-300 text-violet-600 focus:ring-violet-500" />
          <Label htmlFor="is_nonexempt">Non-exempt</Label>
        </div>
        <div className="space-y-1">
          <Label htmlFor="job_title_id">Job Title ID</Label>
          <Input id="job_title_id" type="text" {...register("job_title_id")} />
        </div>
        <div className="flex items-center gap-3">
          <input id="is_passthru" type="checkbox" {...register("is_passthru")} className="h-4 w-4 rounded border-slate-300 text-violet-600 focus:ring-violet-500" />
          <Label htmlFor="is_passthru">Passthru</Label>
        </div>
        <div className="space-y-1">
          <Label htmlFor="pay_freq_type_id">Pay Frequency Type ID</Label>
          <Input id="pay_freq_type_id" type="text" {...register("pay_freq_type_id")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="hrs_worked_type_id">Hours Worked Type ID</Label>
          <Input id="hrs_worked_type_id" type="text" {...register("hrs_worked_type_id")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="profile_id">Profile ID</Label>
          <Input id="profile_id" type="text" {...register("profile_id")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="bill_unit_type_id">Bill Unit Type ID</Label>
          <Input id="bill_unit_type_id" type="text" {...register("bill_unit_type_id")} />
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
