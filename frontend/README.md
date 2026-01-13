# 📊 Finance Dashboard - Interface

Módulo de visualização do Smart Finance Control, focado em UX limpa e gráficos em tempo real.

## 🚀 Tecnologias
- **React + TypeScript** (Vite)
- **Lucide React**: Ícones modernos.
- **Recharts**: Gráficos dinâmicos de categorias.
- **Axios**: Comunicação eficiente com a API .NET.

## 🛠️ Execução Local

1. **Instalar dependências**:
   ```bash
   npm install
Configuração de Proxy: O projeto utiliza proxy para evitar problemas de CORS. Ajuste o target no arquivo vite.config.ts para a porta da sua API (ex: http://localhost:5048).

Rodar Projeto:

Bash

npm run dev
📋 Funcionalidades Implementadas
Dashboard Resumo: Total gasto e contagem de transações com filtro de data.

Gráfico de Rosca: Distribuição percentual por categoria de gasto.

Lista de Recentes: Tabela com os últimos gastos cadastrados via API ou Automação.