## 📌 Descrição
A ideia desse projeto é aperfeiçoar meu conhecimento, sendo esse o primeiro projeto com padronizações, bons estudos a eu mesmo.

Sistema CRUD completo para gerenciamento de "Mercado" e "Clientes". Permite cadastrar, listar, editar e desativar (soft delete) veículos, com autenticação de usuários, validação de dados e arquitetura 

---

## 🏗️ Arquitetura

O diagrama abaixo ilustra a separação física dos projetos dentro da solução, seguindo os princípios da Clean Architecture.


- **Controllers** 
- **Services** 
- **Repositories** 
- **Models / Entities** 
- **DTOs** 
- **Data / Context** 

## 🛠 Funcionalidades Desejadas

### 🔐 Autenticação
- Login com JWT (`POST /api/auth/login`)
- Permissões por perfil: Admin ou Funcionário
- Proteção das rotas com `[Authorize]`

---

## ✅ Validações com FluentValidation

- **Name** obrigatória.
- **CEP** deve ser existente.
- **PhoneNumber** e **Number** não podem estar vazios.

---

## 📝 Logs de Erros

- Uso de `ILogger<T>` para registrar ações e falhas.
- Middleware global para capturar e exibir erros padrão da aplicação.

---

## 🗑️ Soft Delete

- O campo `Ativo` indica se o veículo está disponível.
- O método de "exclusão" apenas atualiza o campo para `false`.
- Listagens consideram apenas registros com `Ativo == true`.

---

## 🧪 Tecnologias Utilizadas

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- FluentValidation
- JWT Bearer Authentication
- Swagger (para testes)
- AutoMapper (opcional)
