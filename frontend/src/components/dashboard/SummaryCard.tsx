import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card";
import { ArrowDownCircle, ArrowUpCircle, Wallet, DollarSign } from "lucide-react"; 

interface SummaryCardProps {
  total: number;
  label: string;
  type: "income" | "expense" | "balance" | "number"; 
  percentage?: number; 
}

export function SummaryCard({ total, label, type, percentage }: SummaryCardProps) {
  const config = {
    income: {
      color: "text-emerald-500",
      icon: ArrowUpCircle,
      trendColor: "text-emerald-600",
      description: "em relação ao mês passado"
    },
    expense: {
      color: "text-red-500", 
      icon: ArrowDownCircle,
      trendColor: "text-red-600",
      description: "em relação ao mês passado"
    },
    balance: {
      color: "text-blue-500", 
      icon: Wallet,
      trendColor: "text-blue-600",
      description: "saldo atual acumulado"
    },
    number: {
      color: "text-muted-foreground",
      icon: DollarSign,
      trendColor: "text-gray-500",
      description: "registros totais"
    }
  };

  const activeConfig = config[type];
  const Icon = activeConfig.icon;

  const valorFormatado = type === "number" 
    ? total.toString() 
    : new Intl.NumberFormat("pt-BR", { style: "currency", currency: "BRL" }).format(total);

  return (
    <Card>
      <CardHeader className="flex flex-row items-center justify-between space-y-0 pb-2">
        <CardTitle className="text-sm font-medium">{label}</CardTitle>
        <Icon className={`h-4 w-4 ${activeConfig.color}`} />
      </CardHeader>
      <CardContent>
        <div className="text-2xl font-bold">{valorFormatado}</div>
        
        {percentage !== undefined && (
          <p className="text-xs text-muted-foreground flex items-center mt-1">
            <span className={`font-bold mr-1 ${percentage >= 0 ? 'text-emerald-600' : 'text-red-600'}`}>
              {percentage > 0 ? "+" : ""}{percentage}%
            </span>
            {activeConfig.description}
          </p>
        )}
      </CardContent>
    </Card>
  );
}