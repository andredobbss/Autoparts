<img src="https://github.com/andredobbss/Autoparts/blob/master/img/img_autoparts.jpg"/>

# Autoparts API — Vertical Slice Architecture

**Autoparts API**, um projeto desenvolvido em .NET Core 9 adotando Vertical Slice Architecture para máxima coesão, separação de responsabilidades e escalabilidade. Este projeto serve como base para aplicações modernas com **DDD, CQRS, MediatR, FluentValidation, Identity, Entity Framework Core, SQL Server**.

---
## 📘 Documentação

### [📄 Acessar os requisitos do projeto (PDF)](docs/HST.pdf)
---

## 🚀 Tecnologias Utilizadas

<!-- .NET 9 Web API -->
![.NET 9](https://img.shields.io/badge/.NET%209-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Web API](https://img.shields.io/badge/Web%20API-178600?style=for-the-badge&logo=dotnet&logoColor=white)

<!-- Vertical Slice Architecture -->
![Vertical Slice Architecture](https://img.shields.io/badge/Vertical%20Slice%20Architecture-0A66C2?style=for-the-badge&logo=layers&logoColor=white)

<!-- CQRS + MediatR -->
![CQRS](https://img.shields.io/badge/CQRS-0088CC?style=for-the-badge&logo=data&logoColor=white)
![MediatR](https://img.shields.io/badge/MediatR-E10098?style=for-the-badge&logo=messenger&logoColor=white)

<!-- DDD -->
![DDD](https://img.shields.io/badge/DDD%20(Domain--Driven%20Design)-02569B?style=for-the-badge&logo=domain&logoColor=white)

<!-- FluentValidation -->
![FluentValidation](https://img.shields.io/badge/FluentValidation-2D9CDB?style=for-the-badge&logo=checkmarx&logoColor=white)

<!-- EF Core + SQL Server -->
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=ef&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)

<!-- ASP.NET Core Identity -->
![AspNetCore Identity](https://img.shields.io/badge/ASP.NET%20Core%20Identity-512BD4?style=for-the-badge&logo=identityserver&logoColor=white)

<!-- Temporal Tables -->
![Temporal Tables](https://img.shields.io/badge/Temporal%20Tables-0066CC?style=for-the-badge&logo=clockify&logoColor=white)

<!-- JWT Auth -->
![JWT Bearer](https://img.shields.io/badge/JWT%20Bearer-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white)

<!-- FastReport -->
![FastReport](https://img.shields.io/badge/FastReport-A50034?style=for-the-badge&logo=fastapi&logoColor=white)

<!-- Z.PagedList -->
![Z.PagedList](https://img.shields.io/badge/Z.PagedList-4CAF50?style=for-the-badge&logo=buffer&logoColor=white)

<!-- Minimal APIs -->
![Minimal APIs](https://img.shields.io/badge/Minimal%20APIs-512BD4?style=for-the-badge&logo=api&logoColor=white)

<!-- Swagger -->
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black)


---

## 📁 Estrutura do Projeto (Vertical Slice)

A arquitetura é organizada por features, não por camadas. Cada funcionalidade contém tudo o que é necessário para existir isoladamente.

![Estrutura](img/Estrutura.png)

---

## 🛢️ Diagrama Entidade-Relacionamento (Conceitual)

 ![MER](img/Autoparts_Conceptual.png)
 
---

## 🛡 Validações e Regras de Negócio

- Validações no domínio, não em DTOs
- FluentValidation aplicado direto nas entidades
- Middlewares transformam exceções em respostas JSON padrão

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
