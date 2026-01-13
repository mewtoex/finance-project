import { useState } from "react";
import { useFinance } from "@/hooks/useFinance"; 
import { FinanceService } from "@/services/api";
import { toast } from "sonner";
import { useQueryClient } from "@tanstack/react-query";
import type { Transaction } from "@/types/finance";
import type { DateRange } from "react-day-picker";
import { subDays } from "date-fns";

export function useDashboard() {
  const queryClient = useQueryClient();

  const [dateRange, setDateRange] = useState<DateRange | undefined>({
    from: subDays(new Date(), 30),
    to: new Date(),
  });

  const { resumo, recentes, loading, error } = useFinance(dateRange);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedTransaction, setSelectedTransaction] = useState<Transaction | null>(null);

  const refreshData = async () => {
    await Promise.all([
      queryClient.invalidateQueries({ queryKey: ['finance-summary'] }),
      queryClient.invalidateQueries({ queryKey: ['finance-transactions'] })
    ]);
  };

  const openCreateModal = () => {
    setSelectedTransaction(null);
    setIsModalOpen(true);
  };

  const openEditModal = (t: Transaction) => {
    setSelectedTransaction(t);
    setIsModalOpen(true);
  };

  const deleteTransaction = async (id: number) => {
    toast.promise(
      async () => {
        await FinanceService.deleteTransaction(id);
        await refreshData();
      },
      {
        loading: 'Excluindo...',
        success: 'Registro removido!',
        error: 'Erro ao excluir.',
      }
    );
  };

  const saveTransaction = async (data: any) => {
    try {
      if (selectedTransaction) {
        await FinanceService.updateTransaction(selectedTransaction.id, data);
        toast.success("Atualizado com sucesso!");
      } else {
        await FinanceService.createTransaction(data);
        toast.success("Criado com sucesso!");
      }
      await refreshData();
      setIsModalOpen(false);
    } catch (error: any) {
      console.error(error);
      toast.error(error.message || "Erro ao salvar");
    }
  };

  return {
    summary: resumo,
    transactions: recentes,
    loading,
    error,
    
    isModalOpen,
    setIsModalOpen,
    selectedTransaction,
    
    openCreateModal,
    openEditModal,
    deleteTransaction,
    saveTransaction,

    dateRange,
    setDateRange
  };
}