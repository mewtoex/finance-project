# 💰 Finance Ecosystem (Self-Hosted & Automated)

![CI/CD Status](https://img.shields.io/github/actions/workflow/status/seu-usuario/finance-project/deploy.yml?label=Build%20%26%20Deploy&logo=github)
![.NET Version](https://img.shields.io/badge/.NET-8.0-purple?logo=dotnet)
![Docker](https://img.shields.io/badge/Container-Docker-blue?logo=docker)
![Architecture](https://img.shields.io/badge/Architecture-Microservices%20Concept-orange)

Um ecossistema financeiro pessoal completo, automatizado e auto-hospedado (Home Lab). O projeto permite o gerenciamento de despesas via chat (Telegram), utilizando uma arquitetura moderna com **ChatOps**, **CI/CD** e **Clean Architecture**.

---

## 🏗️ Arquitetura da Solução

O sistema foi desenhado para ser **invisível e eficiente**. Não há necessidade de abrir apps complexos; tudo acontece onde o usuário já está (Telegram).

### Fluxo de Dados
1. **Inputs:**
   - **ChatOps (Telegram):** Envio de mensagens de texto ou *fotos de comprovantes*.
   - **Web Dashboard:** Interface visual para análise de gráficos e cadastro manual.
2. **Orquestração & IA (n8n + Groq):**
   - **OCR & Processamento:** O n8n recebe a imagem, extrai o texto e envia para a **Groq IA (Llama 3)**.
   - **Interpretação:** A IA identifica Data, Valor, Estabelecimento e define a **Categoria** automaticamente.
   - **Webhook:** Envia o JSON estruturado para o Backend.
3. **Core (API .NET):**
   - Valida regras de negócio e DTOs.
   - Persiste no **PostgreSQL** (Tabela `Gastos`).
4. **Feedback:** O usuário recebe a confirmação no Telegram ou visualiza a atualização em tempo real no Dashboard.
---

## 🔄 DevOps & CI/CD (Automação Total)

O projeto segue a filosofia **GitOps**. Nenhuma atualização é feita manualmente no servidor.

1.  **Development:** Código é commitado na branch `main`.
2.  **Continuous Integration (GitHub Actions):**
    * O pipeline é acionado automaticamente.
    * Roda os **Testes Unitários (xUnit)**. Se falhar, o processo para.
    * Se passar, constrói a imagem Docker.
    * Publica a imagem no Docker Hub com a tag `:latest`.
3.  **Continuous Deployment (Watchtower):**
    * O serviço **Watchtower** (rodando no CasaOS) monitora o Docker Hub.
    * Ao detectar uma nova versão `:latest`, ele baixa a imagem e recria o container da API automaticamente em produção.

---

## 🛠️ Tecnologias Utilizadas

### Backend & Dados
* **C# .NET 8:** Performance e tipagem forte.
* **Entity Framework Core:** ORM para manipulação de dados.
* **PostgreSQL:** Banco de dados relacional robusto.
* **xUnit & Moq:** Testes unitários e mocks.
* **Swagger:** Documentação viva da API.

### Frontend (Dashboard)
* **React 18 (Vite):** Performance e build rápido.
* **TypeScript:** Segurança de tipos.
* **Tailwind CSS + Shadcn/UI:** Interface moderna e responsiva.
* **Recharts:** Visualização de dados.

### Inteligência & Automação
* **n8n:** Orquestrador de fluxos (Telegram <-> API).
* **Groq Cloud API:** LLM de ultra-baixa latência para categorização e extração de dados (OCR).
* **Docker & CasaOS:** Infraestrutura de Home Lab.
---

## 🚀 Como Executar Localmente

### Pré-requisitos
* Docker e Docker Compose instalados.
* Node.js (para rodar o frontend localmente, se desejar).

### Passo a Passo
1.  Clone o repositório:
    ```bash
    git clone [https://github.com/mewtoex/finance-project.git](https://github.com/mewtoex/finance-project.git)
    ```

2.  Configure as variáveis de ambiente (crie um arquivo `.env` ou ajuste no `docker-compose.yml`):
    ```env
    POSTGRES_USER=admin
    POSTGRES_PASSWORD=admin
    ```

3.  Suba o ambiente:
    ```bash
    docker-compose up -d
    ```

4.  Acesse a documentação da API:
    * URL: `http://localhost:5001/swagger`

---

## 📂 Estrutura do Projeto

```bash
finance-project/
├── .github/workflows/    # 🤖 Pipelines CI/CD
├── automation/           # ⚡ Fluxos do n8n (JSON)
├── backend/              # 🧠 API .NET 8
│   ├── FinanceApi/
│   └── FinanceApi.Tests/
├── frontend/             # 🎨 Dashboard React + Vite
├── docker-compose.yml    # 📦 Orquestração
└── README.md             # 📄 Documentação Geral
---
Desenvolvido por **Whanderson Andrade**.