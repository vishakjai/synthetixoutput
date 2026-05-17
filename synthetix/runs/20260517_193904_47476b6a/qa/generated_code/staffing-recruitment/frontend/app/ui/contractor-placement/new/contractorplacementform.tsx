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
  contractor_id: z.string().min(1, "Contractor ID is required"),
  engagement_id: z.string().min(1, "Engagement ID is required"),
  rate: z.string().optional(),
  start_date: z.string().datetime().superRefine((value, ctx) => {
    const startDate = new Date(value)
    const today = new Date()
    const diffTime = Math.abs(startDate.getTime() - today.getTime())
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24))
    if (diffDays < 5) {
      ctx.addIssue({
        code: z.ZodIssueCode.custom,
        message: "Start date must be at least 5 business days from today",
      })
    }
  }),
  office_id: z.string().min(1, "Office ID is required"),
  placement_status: z.string().optional(),
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
    router.push("/ui/contractor-placement")
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
      <div className="grid grid-cols-1 md:grid-cols-2 gap-x-6 gap-y-4">
        <div className="space-y-1">
          <Label htmlFor="contractor_id">Contractor ID<span className="text-red-600 ml-1">*</span></Label>
          <Input id="contractor_id" type="text" {...register("contractor_id")} />
          {errors.contractor_id && <p className="text-xs text-red-600">{errors.contractor_id.message}</p>}
        </div>
        <div className="space-y-1">
          <Label htmlFor="engagement_id">Engagement ID<span className="text-red-600 ml-1">*</span></Label>
          <Input id="engagement_id" type="text" {...register("engagement_id")} />
          {errors.engagement_id && <p className="text-xs text-red-600">{errors.engagement_id.message}</p>}
        </div>
        <div className="space-y-1">
          <Label htmlFor="rate">Rate</Label>
          <Input id="rate" type="text" {...register("rate")} />
        </div>
        <div className="space-y-1">
          <Label htmlFor="start_date">Start Date<span className="text-red-600 ml-1">*</span></Label>
          <Input id="start_date" type="datetime-local" {...register("start_date")} />
          {errors.start_date && <p className="text-xs text-red-600">{errors.start_date.message}</p>}
        </div>
        <div className="space-y-1">
          <Label htmlFor="office_id">Office ID<span className="text-red-600 ml-1">*</span></Label>
          <Input id="office_id" type="text" {...register("office_id")} />
          {errors.office_id && <p className="text-xs text-red-600">{errors.office_id.message}</p>}
        </div>
        <div className="space-y-1">
          <Label htmlFor="placement_status">Placement Status</Label>
          <Input id="placement_status" type="text" {...register("placement_status")} />
        </div>
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
