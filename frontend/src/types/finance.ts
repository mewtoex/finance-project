export interface Transaction {
  id: number;
  descricao: string;
  valor: number;
  categoria: string;
  data: string;
}

export interface DashboardSummary {
  totalGasto: number;
  quantidadeTransacoes: number;
  grafico: { name: string; value: number }[];
}