"use client"

import { useState } from "react"
import { useRouter } from "next/navigation"
import { useForm } from "react-hook-form"
import { zodResolver } from "@hookform/resolvers/zod"
import { z } from "zod"
import { Loader2, Save } from "lucide-react"
import { Button } from "@/components/ui/button"

// R1 — Rate must be within client engagement's contracted ceiling unless co-approver has override authority.
// R2 — Start date must be at least 5 business days from creation date.
const schema = z.object({
  email: z.string().optional(),
  firstname: z.string().optional(),
  lastname: z.string().optional(),
  contact_email: z.string().optional(),
  contact_firstname: z.string().optional(),
  contact_lastname: z.string().optional(),
  country_id: z.string().min(1),
  PRIMARY_office_id: z.string().min(1),
  end_date: z.string().datetime(),
  rp_valid_from: z.string().optional(),
  start_date: z.string().datetime(),
  assignment_type_id: z.string().min(1),
  contractor_placement_id: z.string().min(1),
  employee_id: z.string().min(1),
  employment_type_id: z.string().min(1),
  is_nonexempt: z.boolean(),
  job_title_id: z.string().min(1),
  is_passthru: z.boolean(),
  pay_freq_type_id: z.string().min(1),
  hr_payrate_ot: z.string().optional(),
  hr_payrate_st: z.string().optional(),
  referal_fee: z.string().optional(),
  valid_from: z.string().optional(),
  valid_to: z.string().optional(),
  pct_vendor_discount: z.string().optional(),
  day_per_diem: z.string().optional(),
  fts_hr_burden: z.string().optional(),
  hrs_worked_type_id: z.string().min(1),
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
  profile_id: z.string().min(1),
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
  bill_unit_type_id: z.string().min(1),
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
}).superRefine((data, ctx) => {
  // R1 — Validate rate ceiling
  // Implement validation logic here
  // R2 — Validate start date
  const startDate = new Date(data.start_date);
  const creationDate = new Date(); // Replace with actual creation date
  const fiveBusinessDays = 5 * 24 * 60 * 60 * 1000; // 5 business days in milliseconds
  if (startDate < new Date(creationDate.getTime() + fiveBusinessDays)) {
    ctx.addIssue({
      code: z.ZodIssueCode.custom,
      path: ["start_date"],
      message: "Start date must be at least 5 business days from creation date.",
    });
  }
});

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
    router.push("/ui/contractor-placement")
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
        {/* Render all fields here with appropriate input types */}
      </div>
      {serverError && (
        <div className="rounded-md border border-red-200 bg-red-50 text-red-800 text-sm px-3 py-2">{serverError}</div>
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
