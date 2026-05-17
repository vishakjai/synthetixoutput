import { brand } from "@/lib/brand"
import { Card, CardContent } from "@/components/ui/card"
import ContractorPlacementForm from "./ContractorPlacementForm"

export const metadata = { title: "New Contractor Placement | MagicBox Next" }
export const dynamic = "force-dynamic"

export default function Page() {
  return (
    <main className={`min-h-screen ${brand.pageBackground}`}> 
      <div className="container mx-auto px-8 pt-8 pb-4 max-w-4xl">
        <h1 className="text-2xl font-bold tracking-tight text-slate-900">New Contractor Placement</h1>
      </div>
      <div className="container mx-auto px-8 max-w-4xl pb-12">
        <Card className="shadow-md">
          <CardContent className="p-8">
            <ContractorPlacementForm />
          </CardContent>
        </Card>
      </div>
    </main>
  )
}