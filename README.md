
API backend para um sistema de e-commerce, projetada com foco em **arquitetura limpa, segurança, escalabilidade e boas práticas de engenharia**.

---

## 🚀 Stack

* .NET 10 / ASP.NET Core
* Entity Framework Core (PostgreSQL)
* JWT Authentication
* FluentValidation
* AutoMapper
* BCrypt
* Logging com ILogger

---

## 🧱 Arquitetura

O projeto segue uma abordagem inspirada em **Clean Architecture**, separando responsabilidades:

```id="arq1"
API → camada de entrada (controllers, pipeline HTTP)
Application → regras de aplicação, serviços e DTOs
Domain → regras de negócio e entidades
Infrastructure → acesso a dados e integrações externas
```

### 🎯 Decisão

Essa separação foi adotada para:

* reduzir acoplamento
* facilitar testes
* permitir evolução do sistema sem impacto global

---

## 🔐 Autenticação e Autorização

* Autenticação baseada em **JWT**
* Senhas protegidas com **BCrypt**
* Autorização com **Roles (Admin / User)**

### Fluxo

```id="flow"
Login → geração de token → acesso protegido via header Authorization
```

---

## ✅ Validação

Validação implementada em duas camadas:

### Entrada (Application)

* FluentValidation
* Garante integridade dos dados recebidos

### Domínio (Domain)

* Regras críticas dentro das entidades
* Evita estados inválidos no sistema

👉 Essa abordagem garante **defesa em profundidade**

---

## ⚠️ Tratamento de erros

Middleware global responsável por:

* padronização de respostas
* isolamento de exceções
* segurança (não expor detalhes internos)

---

## 📊 Logging

* Implementação com ILogger
* Logs estruturados para:

  * autenticação
  * falhas
  * eventos relevantes

---

## 📦 Funcionalidades atuais

* Cadastro de usuários
* Autenticação com JWT
* CRUD de produtos
* Controle de acesso por roles
* Validação robusta
* Tratamento global de erros

---

## 🔜 Roadmap técnico

### 🔐 Segurança

* Refresh Token com rotação
* Revogação de tokens
* Policies (controle granular de acesso)

---

### ⚡ Performance

* Paginação eficiente
* Queries otimizadas (EF Core)
* Cache distribuído (Redis)

---

### 🧪 Qualidade

* Testes unitários
* Testes de integração
* Cobertura de código

---

### 📊 Observabilidade

* Logging estruturado (Serilog)
* CorrelationId por request
* Monitoramento

---

### 📈 Analytics

* Integração com eventos de uso
* Tracking de comportamento
* Dashboard de métricas

---

### 🌐 Frontend

* Aplicação em Next.js e Typescript
* Integração completa com API
* Visualização de dados analíticos

---

## ▶️ Execução

dotnet restore
dotnet run

---

## 🧠 Objetivo do projeto

Evoluir para um sistema completo com:

* arquitetura escalável
* backend resiliente
* integração com analytics

