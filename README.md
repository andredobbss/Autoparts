<img src="https://github.com/andredobbss/Autoparts/blob/master/img/img_autoparts.jpg"/>

# Autoparts API — Vertical Slice Architecture

**Autoparts API**, um projeto desenvolvido em .NET Core 9 adotando Vertical Slice Architecture para máxima coesão, separação de responsabilidades e escalabilidade. Este projeto serve como base para aplicações modernas com **DDD, CQRS, MediatR, FluentValidation, Identity, Entity Framework Core, SQL Server**.

---
## 📘 Documentação

### [📄 Acessar os requisitos do projeto (PDF)](docs/HST.pdf)
---

## 🚀 Tecnologias Utilizadas

- .NET 9 Web API
- Vertical Slice Architecture
- CQRS + MediatR
- DDD (Domain-Driven Design)
- FluentValidation (Validações no Domínio)
- Entity Framework Core (SQL Server)
- AspNetCore Identity
- Temporal Tables (Controle automático de histórico)
- Authentication JWT Bearer
- Fast Report (Geração de relatório)
- Z.PagedList
- xUnit + Bogus + NSubstitute (Testes)
- Minimals APIs
- Swagger (Documentação da API)

---

## 📁 Estrutura do Projeto (Vertical Slice)

A arquitetura é organizada por features, não por camadas. Cada funcionalidade contém tudo o que é necessário para existir isoladamente.

<img src="https://github.com/andredobbss/Autoparts/blob/master/img/Estrutura.png"/>

---

## 🛢️ Diagrama Entidade-Relacionamento (Conceitual)

<img src="https://github.com/andredobbss/Autoparts/blob/master/img/Autoparts_Conceptual.png"/>

---

## 🛡 Validações e Regras de Negócio

- Validações no domínio, não em DTOs
- FluentValidation aplicado direto nas entidades
- Erros geram DomainValidationException
- Middlewares transformam exceções em respostas JSON padrão

---
## 🧪 Testes Automatizados

O projeto contém testes de:

- Entidades do domínio (Categories, Clients, Manufactures, Products...)
- Validação de regras com FluentValidation
- Serviços usando NSubstitute
- Geração de dados fake com Bogus

Exemplo de stack:

- xUnit
- Bogus
- NSubstitute

---
## ▶️ Como Rodar o Projeto
### 1. Clone o repositório

```cmd
git clone https://github.com/andredobbss/autoparts
cd autoparts
```

### 2. Configure a connection string
#### 2.1 Crie uma variável de ambiente com o nome `"DEFAULT_CONNECTION_AUTOPARTS"` com o valor da string de conexão
#### 2.2 Crie um `appsettings.Development.json` com:

```cmd
{
  "ConnectionStrings": {
  "DefaultConnection": "Server=SERVER_NAME;Database=Autoparts;User ID=sa;Password=*******; Trusted_Connection=False;TrustServerCertificate=True"
  }
}
```
### 3. Execute as migrations

```cmd
dotnet ef database update -s Autoparts.Api
```
### 4. Execute a API

```cmd
dotnet run --project Autoparts.Api
```
---

## 🌐 Endpoints (exemplo minimal API)

```csharp
group.MapGet("/", async (ISender mediator) =>
{
  var result = await mediator.Send(new GetAllUsersQuery());
  return Results.Ok(result);
});
```

## 📝 Licença
Este projeto é distribuído sob a licença MIT.
