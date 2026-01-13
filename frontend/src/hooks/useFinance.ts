import { useQuery } from "@tanstack/react-query";
import { FinanceService } from "@/services/api";
import type { DateRange } from "react-day-picker";

export function useFinance(dateRange?: DateRange) {
  
  const summaryQuery = useQuery({
    queryKey: ['finance-summary', dateRange], 
    queryFn: () => FinanceService.getSummary(dateRange?.from, dateRange?.to),
    staleTime: 1000 * 60 * 5, 
  });

  const transactionsQuery = useQuery({
    queryKey: ['finance-transactions'],
    queryFn: FinanceService.getRecentTransactions, 
    staleTime: 1000 * 60 * 1, 
  });

  return {
    resumo: summaryQuery.data,      
    recentes: transactionsQuery.data || [], 
    loading: summaryQuery.isLoading || transactionsQuery.isLoading,
    error: summaryQuery.isError || transactionsQuery.isError,
  };
}