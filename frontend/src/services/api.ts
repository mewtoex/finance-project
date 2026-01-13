import type { Transaction, DashboardSummary } from "@/types/finance";

const API_URL = "/api";
const API_KEY = import.meta.env.VITE_API_KEY;

const HEADERS = {
  "Content-Type": "application/json",
  "x-api-key": API_KEY || ""
};

export const FinanceService = {
  getRecentTransactions: async (): Promise<Transaction[]> => {
    const response = await fetch(`${API_URL}/gasto/recentes`, { headers: HEADERS });
    if (!response.ok) throw new Error("Erro ao buscar transações");
    return response.json();
  },

  getSummary: async (start?: Date, end?: Date): Promise<DashboardSummary> => {
    const params = new URLSearchParams();
    if (start) params.append('startDate', start.toISOString());
    if (end) params.append('endDate', end.toISOString());

    const response = await fetch(`${API_URL}/gasto/summary?${params.toString()}`, { headers: HEADERS });

    if (!response.ok) throw new Error('Erro ao buscar resumo');
    return response.json();
  },

  createTransaction: async (gasto: any): Promise<void> => {
    const payload = {
      ...gasto,
      valor: Number(gasto.valor),
      data: new Date(gasto.data).toISOString()
    };

    const response = await fetch(`${API_URL}/gasto`, {
      method: "POST",
      headers: HEADERS,
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || "Erro ao criar");
    }
  },

  updateTransaction: async (id: number, gasto: any): Promise<void> => {
    const payload = {
      ...gasto,
      valor: Number(gasto.valor),
      data: new Date(gasto.data).toISOString()
    };

    const response = await fetch(`${API_URL}/gasto/${id}`, {
      method: "PUT",
      headers: HEADERS,
      body: JSON.stringify(payload),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || "Erro ao atualizar");
    }
  },

  deleteTransaction: async (id: number): Promise<void> => {
    const response = await fetch(`${API_URL}/gasto/${id}`, {
      method: "DELETE",
      headers: HEADERS,
    });

    if (!response.ok) throw new Error("Erro ao excluir");
  }
};