# 🏦 Finance API (.NET 8 Core)

API RESTful focada em performance e arquitetura limpa.

## 🎯 Funcionalidades
* **CRUD de Despesas:** Criar, Ler, Atualizar e Deletar.
* **Resumo Mensal:** Endpoint `/resumo` que calcula totais e agrupa por categoria.
* **Segurança:** Middleware de API Key para proteção em rede local.

## 🧩 Arquitetura (Clean Architecture)
* **Controllers:** Apenas recebem requisições HTTP.
* **Services:** Regras de negócio e cálculos (Desacoplado via Interfaces).
* **Data:** Repositório EF Core e PostgreSQL.
* **Tests:** Testes de unidade em memória (InMemoryDatabase).

## 🔌 Endpoints Principais
| Verbo | Rota | Função |
| :--- | :--- | :--- |
| `POST` | `/api/gasto` | Registrar gasto. |
| `GET` | `/api/gasto/recentes` | Últimos 5 registros. |
| `GET` | `/api/gasto/resumo` | Dashboard do mês. |

## 🐳 Docker
Build manual da imagem:
```bash
docker build -t tobijf35/finance-api:latest .