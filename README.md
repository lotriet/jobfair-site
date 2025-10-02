# .NET Micro API

A modern .NET Web API demonstration featuring:

- ✅ **Async/Await** patterns for non-blocking operations
- ✅ **SQLite Database** - Self-contained, no server required!
- ✅ **Retry Policy** using Polly for resilience
- ✅ **REST API** endpoints with full CRUD operations

## 🎯 **Perfect for Showcases!**

### ✨ **Why SQLite?**

- **Zero setup** - No SQL Server installation needed
- **Self-contained** - Creates `demo.db` file automatically
- **Portable** - Works on any machine with .NET
- **Showcase-friendly** - Perfect for presentations and portfolios

### 🌐 **Interactive Browser Demo**

Open `../interactive-demo.html` in any browser for an immediate visual showcase!

## 🚀 Quick Start

### 1. Start the API

```powershell
cd DotNetMicroDemo
dotnet run
```

### 2. Open Swagger UI

Navigate to: `https://localhost:7001`

### 3. Explore the Features

**Async/Await + Retry Policy:**

```bash
# GET all products (shows async database call with retry)
curl https://localhost:7001/api/products

# GET specific product (shows async query with retry)
curl https://localhost:7001/api/products/1

# POST new product (shows async insert with retry)
curl -X POST https://localhost:7001/api/products \
  -H "Content-Type: application/json" \
  -d '{"name":"Demo Product","price":99.99,"description":"Created via API"}'
```

## 🔧 Key Features Highlighted

### Async/Await Pattern

```csharp
public async Task<IEnumerable<Product>> GetAllProductsAsync()
{
    return await _retryPolicy.ExecuteAsync(async () =>
    {
        return await _context.Products.ToListAsync();
    });
}
```

### Retry Policy with Polly

```csharp
_retryPolicy = Policy
    .Handle<Microsoft.Data.Sqlite.SqliteException>()
    .Or<TimeoutException>()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
    );
```

### REST Controller

```csharp
[HttpGet]
public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
{
    var products = await _productService.GetAllProductsAsync();
    return Ok(products);
}
```

## 📊 Demo Endpoints

| Method | Endpoint             | Description                        |
| ------ | -------------------- | ---------------------------------- |
| GET    | `/api/products`      | Get all products (async + retry)   |
| GET    | `/api/products/{id}` | Get product by ID (async + retry)  |
| POST   | `/api/products`      | Create new product (async + retry) |
| PUT    | `/api/products/{id}` | Update product (async + retry)     |
| DELETE | `/api/products/{id}` | Delete product (async + retry)     |
| GET    | `/health`            | Health check endpoint              |

## 🎯 Application Overview

1. **Program.cs** - Dependency injection and async setup
2. **ProductsController.cs** - RESTful endpoints with async/await
3. **ProductService.cs** - Business logic with retry policy
4. **Interactive API** - Swagger UI with live testing

## 🛠️ Technical Stack

- **.NET 8** - Latest LTS framework
- **Entity Framework Core** - Database ORM
- **SQLite** - Self-contained database (demo.db file)
- **Polly** - Retry policy library
- **Swagger/OpenAPI** - API documentation
- **Structured Logging** - Request/response logging

## 💡 Production Ready Features

- Dependency injection
- Exception handling
- Structured logging
- API documentation
- Database migrations
- Retry resilience patterns
