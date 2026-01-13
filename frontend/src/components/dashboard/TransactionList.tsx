import { useState, useEffect } from "react";
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogFooter } from "@/components/ui/dialog";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import type { Transaction } from "@/types/finance";

interface Props {
  isOpen: boolean;
  onClose: () => void;
  onSave: (data: any) => Promise<void>; 
  transaction: Transaction | null; 
}

export function TransactionModal({ isOpen, onClose, onSave, transaction }: Props) {
  const [formData, setFormData] = useState({
    descricao: "",
    valor: "",
    categoria: "",
    data: new Date().toISOString().split('T')[0] 
  });
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    if (isOpen) {
      if (transaction) {
        setFormData({
          descricao: transaction.descricao,
          valor: transaction.valor.toString(),
          categoria: transaction.categoria || "",
          data: transaction.data.split('T')[0]
        });
      } else {
        setFormData({
          descricao: "",
          valor: "",
          categoria: "",
          data: new Date().toISOString().split('T')[0]
        });
      }
    }
  }, [isOpen, transaction]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    try {
      await onSave({
        ...formData,
        valor: Number(formData.valor)
      });
      onClose();
    } catch (error) {
      console.error(error);
    } finally {
      setSaving(false);
    }
  };

  return (
    <Dialog open={isOpen} onOpenChange={onClose}>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>{transaction ? "Editar Transação" : "Nova Transação"}</DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit} className="grid gap-4 py-4">
          
          <div className="grid gap-2">
            <Label htmlFor="descricao">Descrição</Label>
            <Input 
              id="descricao" 
              value={formData.descricao} 
              onChange={(e) => setFormData({...formData, descricao: e.target.value})} 
              placeholder="Ex: Compras no mercado"
              required 
            />
          </div>

          <div className="grid grid-cols-2 gap-4">
            <div className="grid gap-2">
              <Label htmlFor="valor">Valor (R$)</Label>
              <Input 
                id="valor" 
                type="number" 
                step="0.01" 
                value={formData.valor} 
                onChange={(e) => setFormData({...formData, valor: e.target.value})} 
                placeholder="0,00"
                required 
              />
            </div>
            <div className="grid gap-2">
              <Label htmlFor="data">Data</Label>
              <Input 
                id="data" 
                type="date" 
                value={formData.data} 
                onChange={(e) => setFormData({...formData, data: e.target.value})} 
                required 
              />
            </div>
          </div>

          <div className="grid gap-2">
            <Label htmlFor="categoria">Categoria</Label>
            <Input 
              id="categoria" 
              value={formData.categoria} 
              onChange={(e) => setFormData({...formData, categoria: e.target.value})} 
              placeholder="Ex: Alimentação, Lazer, Casa..." 
              required
            />
          </div>

          <DialogFooter>
            <Button type="button" variant="outline" onClick={onClose}>Cancelar</Button>
            <Button type="submit" disabled={saving}>
              {saving ? "Salvando..." : (transaction ? "Salvar Alterações" : "Criar Gasto")}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}