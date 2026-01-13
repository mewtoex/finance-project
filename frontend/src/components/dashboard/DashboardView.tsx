import { Plus } from "lucide-react";
import { Button } from "@/components/ui/button";
import { TransactionTable } from "./TransactionTable";
import { FinanceCharts } from "./Charts";
import { TransactionModal } from "./TransactionModal";
import { SummaryCard } from "./SummaryCard"; 
import { DateRangePicker } from "./DateRangePicker";
import { DashboardSkeleton } from "./DashboardSkeleton"; 
import { useDashboard } from "@/hooks/useDashboard";

export function DashboardView() {
  const { 
    summary, 
    transactions, 
    loading, 
    error,
    isModalOpen, 
    setIsModalOpen, 
    openCreateModal, 
    saveTransaction,
    deleteTransaction,
    openEditModal,
    selectedTransaction,
    dateRange,
    setDateRange
  } = useDashboard();

  if (loading) {
    return <DashboardSkeleton />;
  }

  if (error) {
    return (
      <div className="flex flex-col items-center justify-center h-screen text-red-500 gap-4">
        <p>Erro ao carregar dados do servidor.</p>
        <Button onClick={() => window.location.reload()}>Tentar Novamente</Button>
      </div>
    );
  }

  const mediaGastos = summary?.quantidadeTransacoes 
    ? (summary.totalGasto / summary.quantidadeTransacoes) 
    : 0;

  return (
    <div className="space-y-6 animate-in fade-in duration-500 p-6">
      <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">Olá, Whanderson! 👋</h2>
          <p className="text-muted-foreground">Controle suas despesas de forma simples.</p>
        </div>
        
        <div className="flex items-center gap-2">
          <DateRangePicker date={dateRange} setDate={setDateRange} />
          
          <Button onClick={openCreateModal}>
            <Plus className="mr-2 h-4 w-4" /> Novo Gasto
          </Button>
        </div>
      </div>

      <div className="grid gap-4 md:grid-cols-3">
        
        <SummaryCard 
          label="Total Gasto no Período" 
          total={summary?.totalGasto || 0} 
          type="expense" 
        />
        
        <SummaryCard 
          label="Transações Realizadas" 
          total={summary?.quantidadeTransacoes || 0} 
          type="number" 
        />

        <SummaryCard 
          label="Média por Compra" 
          total={mediaGastos} 
          type="balance" 
        />
      </div>

      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-7">
        <div className="col-span-4">
          <FinanceCharts data={summary?.grafico || []} />
        </div>
        <div className="col-span-3">
          <TransactionTable 
            data={transactions} 
            onDelete={deleteTransaction}
            onEdit={openEditModal}
          />
        </div>
      </div>

      <TransactionModal 
        isOpen={isModalOpen} 
        onClose={() => setIsModalOpen(false)} 
        onSave={saveTransaction} 
        transaction={selectedTransaction} 
      />
    </div>
  );
}