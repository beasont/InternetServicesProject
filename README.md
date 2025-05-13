# ComputerStore API – Internet Services Project

A clean‑architecture ASP.NET Core Web API that powers a small **Computer Store** back‑office.  
This is my project for the *Internet Services* course at UACS.

---
**What I Used:**

* .NET 9.0 / C#
* ASP.NET Core minimal hosting
* Entity Framework Core 9 + SQL Server
* AutoMapper
* xUnit + FluentAssertions + Microsoft.AspNetCore.Mvc.Testing
* Swagger / OpenAPI (enabled by default)

---

## Prerequisites

| Tool | Minimum version |
|------|-----------------|
| **.NET SDK** | 9.0 |
| **SQL Server** | 2017 Express or later (TCP 5443) |
| **dotnet‑ef CLI** | latest |

> The Web API listens on **http://localhost:5443** (see `Program.cs`).

---

## Database setup

1. Create the database **once** using the supplied script:

   ```bash
   sqlcmd -S localhost,5443 -i db-backup/computerstoredb.sql
   ```

   The script creates the database **ComputerStoreDb** and seeds sample
   *Categories* and *Products*.

   > If you prefer, restore `db-backup/computerstoredb.bak` instead.

2. Ensure `ComputerStore.WebApi/appsettings.json` points to the database:

   ```jsonc
   {
     "ConnectionStrings": {
       "Default": "Server=localhost,5443;Database=ComputerStoreDb;Trusted_Connection=True;Encrypt=False"
     }
   }
   ```

---

## Running the API

```bash
git clone https://github.com/beasont/InternetServicesProject.git
cd InternetServicesProject

dotnet restore
dotnet build
dotnet run --project ComputerStore.WebApi
```

Open **http://localhost:5443** – the Swagger UI appears immediately.

---

## Endpoint reference

> All examples assume the server is running on **http://localhost:5443**.

### Categories

| Method | URL | Description |
|--------|-----|-------------|
| `GET` | `/api/categories` | List all categories |
| `GET` | `/api/categories/{id}` | Get category by id |
| `POST` | `/api/categories` | Create category |
| `PUT` | `/api/categories/{id}` | Update category |
| `DELETE` | `/api/categories/{id}` | Delete category |

```bash
# List categories
curl http://localhost:5443/api/categories

# Create
curl -X POST http://localhost:5443/api/categories      -H "Content-Type: application/json"      -d '{ "name": "Monitors", "description": "LCD / LED monitors" }'
```

### Products

| Method | URL | Description |
|--------|-----|-------------|
| `GET` | `/api/products` | List products |
| `GET` | `/api/products/{id}` | Get product |
| `POST` | `/api/products` | Create product |
| `PUT` | `/api/products/{id}` | Update product |
| `DELETE` | `/api/products/{id}` | Delete product |

```bash
# Add a product
curl -X POST http://localhost:5443/api/products      -H "Content-Type: application/json"      -d '{
           "name": "Dell XPS 13",
           "description": "13‑inch ultrabook",
           "price": 1299.99,
           "quantity": 5,
           "categoryId": 1
         }'
```

### Stock import

Bulk‑import categories + products in one shot.

| Method | URL |
|--------|-----|
| `POST` | `/api/stock/import` |

```bash
curl -X POST http://localhost:5443/api/stock/import      -H "Content-Type: application/json"      -d '[
           {
             "categories": [ { "name": "Accessories", "description": "" } ],
             "products":   [ { "name": "Logitech G305", "description": "Wireless mouse" } ],
             "price":      49.90,
             "quantity":   25
           }
         ]'
```

### Basket discount

Calculates discount and final total for a customer basket.

| Method | URL |
|--------|-----|
| `POST` | `/api/basket/discount` |

```bash
curl -X POST http://localhost:5443/api/basket/discount      -H "Content-Type: application/json"      -d '[
           { "productId": 1, "quantity": 2 },
           { "productId": 3, "quantity": 1 }
         ]'
# ⇒ { "total": 2599.98, "discount": 259.998, "finalTotal": 2339.982 }
```

---

## Running tests
```bash
dotnet test
```

This executes **unit tests** (`ComputerStore.Tests`) and **integration tests**  
(`ComputerStore.IntegrationTests`) using the in‑memory TestServer.

---
