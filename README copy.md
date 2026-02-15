# Construction Project Tracker

A multi-tenant SaaS platform for small and mid-sized construction companies to manage projects, costs, workers, and client billing.

## Tech Stack

### Backend
- .NET 9 Web API
- Entity Framework Core 9 (for writes/commands)
- Dapper (for reads/queries)
- PostgreSQL
- JWT Authentication
- Clean Architecture + CQRS pattern

### Frontend (Coming Soon)
- React + TypeScript

## Project Structure

```
src/
├── ConstructionTracker.Domain/           # Domain Layer (Entities, Enums, Interfaces)
│   ├── Common/
│   │   ├── BaseEntity.cs                 # Base class for all entities
│   │   ├── IAuditableEntity.cs           # Audit interface
│   │   └── ITenantEntity.cs              # Multi-tenancy interface
│   ├── Entities/
│   │   ├── Tenant.cs                     # Tenant/Company entity
│   │   └── User.cs                       # User entity
│   └── Enums/
│       └── UserRole.cs                   # User roles enum
│
├── ConstructionTracker.Application/      # Application Layer (Use Cases, DTOs)
│   ├── Common/
│   │   ├── Behaviors/
│   │   │   └── ValidationBehavior.cs     # MediatR validation pipeline
│   │   ├── Exceptions/
│   │   │   ├── ForbiddenAccessException.cs
│   │   │   ├── NotFoundException.cs
│   │   │   └── TenantAccessViolationException.cs
│   │   ├── Interfaces/
│   │   │   ├── IApplicationDbContext.cs  # EF Core DbContext interface
│   │   │   ├── IDapperContext.cs         # Dapper connection factory
│   │   │   ├── IReadRepository.cs        # Generic read repository
│   │   │   ├── ITenantService.cs         # Tenant context service
│   │   │   ├── IUnitOfWork.cs            # Unit of work pattern
│   │   │   └── IWriteRepository.cs       # Generic write repository
│   │   └── Models/
│   │       └── Result.cs                 # Result pattern for operations
│   └── DependencyInjection.cs            # DI configuration
│
├── ConstructionTracker.Infrastructure/   # Infrastructure Layer (Data Access)
│   ├── Data/
│   │   ├── Configurations/
│   │   │   ├── TenantConfiguration.cs    # Tenant EF config
│   │   │   └── UserConfiguration.cs      # User EF config
│   │   ├── Migrations/                   # EF Core migrations
│   │   ├── ApplicationDbContext.cs       # EF Core DbContext
│   │   ├── ApplicationDbContextFactory.cs # Design-time factory
│   │   ├── DapperContext.cs              # Dapper connection factory
│   │   └── UnitOfWork.cs                 # Unit of work implementation
│   ├── Repositories/
│   │   ├── ReadRepository.cs             # Dapper-based read repository
│   │   └── WriteRepository.cs            # EF Core-based write repository
│   ├── Services/
│   │   └── TenantService.cs              # Tenant context service
│   └── DependencyInjection.cs            # DI configuration
│
└── ConstructionTracker.API/              # API Layer (Controllers, Middleware)
    ├── Controllers/
    │   ├── BaseApiController.cs          # Base controller
    │   └── HealthController.cs           # Health check endpoint
    ├── Middleware/
    │   ├── ExceptionHandlingMiddleware.cs # Global exception handling
    │   └── TenantMiddleware.cs           # JWT tenant extraction
    ├── appsettings.json                  # Configuration
    ├── appsettings.Development.json      # Dev configuration
    └── Program.cs                        # Application entry point
```

## Architecture Principles

### Clean Architecture
- **Domain Layer**: Core business logic and entities (no external dependencies)
- **Application Layer**: Use cases, DTOs, and interfaces (depends only on Domain)
- **Infrastructure Layer**: Data access, external services (implements Application interfaces)
- **API Layer**: HTTP endpoints, middleware (orchestrates Application)

### CQRS (Command Query Responsibility Segregation)
- **Commands** (writes): Use Entity Framework Core through `IWriteRepository<T>`
- **Queries** (reads): Use Dapper through `IReadRepository<T>` for optimized performance

### Multi-Tenancy
- Every entity includes `TenantId` for data isolation
- Tenant ID is extracted from JWT claims via middleware
- Global query filters prevent cross-tenant data access
- All repository operations require tenant context

## Getting Started

### Prerequisites
- .NET 9 SDK
- PostgreSQL 14+
- Docker (optional, for PostgreSQL)

### Setup PostgreSQL (Docker)

```bash
docker run --name construction-postgres \
  -e POSTGRES_PASSWORD=your_password_here \
  -e POSTGRES_DB=construction_tracker_dev \
  -p 5432:5432 \
  -d postgres:16
```

### Configure Connection String

Update `src/ConstructionTracker.API/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=construction_tracker_dev;Username=postgres;Password=your_password_here"
  }
}
```

### Apply Migrations

```bash
# Install EF Core tools (if not already installed)
dotnet tool install --global dotnet-ef

# Apply migrations
dotnet ef database update \
  --project src/ConstructionTracker.Infrastructure \
  --startup-project src/ConstructionTracker.API
```

### Run the Application

```bash
cd src/ConstructionTracker.API
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: https://localhost:5001/swagger

## JWT Claims

The API expects the following JWT claims for multi-tenancy:

| Claim | Description |
|-------|-------------|
| `sub` or `user_id` | User's unique identifier |
| `tenant_id` or `TenantId` | Tenant's unique identifier |

## API Endpoints

### Health Check
- `GET /api/health` - Basic health check
- `GET /api/health/detailed` - Detailed health check with database status

## Future Entities (Day 2+)

- Project
- ProjectCost
- Worker
- WorkerAttendance
- Budget
- Client
- Invoice
- SiteProgress

## License

MIT License
