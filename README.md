# 🏗️ Construction Project Tracker - Multi-Tenant SaaS Backend

A professional multi-tenant SaaS backend for construction project management with JWT authentication, built using **.NET 9** and **PostgreSQL**.

---

## 🎉 Day 2 Complete: JWT Authentication ✅

### Implemented Features
- ✅ Company registration with admin user creation
- ✅ User login with JWT token generation (7-day expiration)
- ✅ Secure password hashing (ASP.NET Identity PBKDF2)
- ✅ Multi-tenant isolation via TenantId in JWT claims
- ✅ Role-based authorization (Admin, Manager, Worker, Viewer)
- ✅ Protected API endpoints with [Authorize] attribute
- ✅ Swagger UI with Bearer token authentication

---

## 🚀 Quick Start

### API is Running
**Base URL:** http://localhost:5103  
**Swagger:** http://localhost:5103/swagger

### Test in 30 Seconds

1. **Open Swagger:** http://localhost:5103/swagger
2. **Register:** POST /api/auth/register
   ```json
   {
     "companyName": "Test Co",
     "subdomain": "testco",
     "firstName": "John",
     "lastName": "Doe",
     "email": "john@testco.com",
     "password": "Test1234!@#$"
   }
   ```
3. **Copy the token** from response
4. **Click "Authorize"** button (🔓)
5. **Enter:** `Bearer YOUR_TOKEN`
6. **Test protected endpoints!** ✅

---

## 📡 API Endpoints

| Endpoint | Method | Auth | Description |
|----------|--------|------|-------------|
| `/api/auth/register` | POST | ❌ | Register company + admin |
| `/api/auth/login` | POST | ❌ | Login and get JWT |
| `/api/auth/me` | GET | ✅ | Get current user |
| `/api/projects` | GET | ✅ | Get projects (test) |
| `/api/projects/admin` | GET | ✅ Admin | Admin only |
| `/api/health` | GET | ❌ | Health check |

---

## 🏗️ Architecture

```
API Layer (Controllers)
    ↓
Application Layer (DTOs, Interfaces, Validation)
    ↓
Infrastructure Layer (Services, Repositories, EF Core)
    ↓
Domain Layer (Entities, Enums)
    ↓
PostgreSQL Database
```

---

## 🔐 Security

- **Password Hashing:** ASP.NET Identity PasswordHasher (PBKDF2)
- **JWT Signing:** HMAC-SHA256
- **Token Expiration:** 7 days
- **Password Policy:** 8+ chars, uppercase, lowercase, digit, special char
- **Multi-Tenancy:** TenantId claim enforces data isolation

---

## 📂 Project Structure

```
├── ConstructionTracker.API/          # Web API
│   ├── Controllers/
│   │   ├── AuthController.cs         # Auth endpoints
│   │   └── ProjectsController.cs     # Protected test
│   └── Program.cs
├── ConstructionTracker.Application/  # Business logic
│   └── Common/Models/Auth/           # DTOs
├── ConstructionTracker.Infrastructure/ # Data access
│   └── Services/
│       ├── AuthService.cs            # Registration/login
│       └── JwtService.cs             # Token generation
└── ConstructionTracker.Domain/       # Entities
    └── Entities/
        ├── Tenant.cs
        └── User.cs
```

---

## 📚 Documentation

- **QUICK_START.md** - Quick reference guide
- **DAY2_IMPLEMENTATION_SUMMARY.md** - Complete overview
- **DAY2_JWT_AUTHENTICATION_COMPLETE.md** - Detailed technical guide
- **Auth.http** - HTTP test requests

---

## 🧪 Testing Examples

### curl
```bash
# Register
curl -X POST http://localhost:5103/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"companyName":"Test Co","subdomain":"testco","firstName":"John","lastName":"Doe","email":"john@testco.com","password":"Test1234!@#$"}'

# Login
curl -X POST http://localhost:5103/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"john@testco.com","password":"Test1234!@#$"}'

# Protected endpoint
curl http://localhost:5103/api/projects \
  -H "Authorization: Bearer YOUR_TOKEN"
```

---

## 🗄️ Database

**PostgreSQL** with Entity Framework Core

### Tables
- **Tenants** - Companies (multi-tenancy)
- **Users** - User accounts with roles

### Migrations
```bash
# Create migration
dotnet ef migrations add MigrationName --project ConstructionTracker.Infrastructure --startup-project ConstructionTracker.API

# Update database
dotnet ef database update --project ConstructionTracker.Infrastructure --startup-project ConstructionTracker.API
```

---

## ⚙️ Configuration

Update `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=construction_tracker_dev;Username=postgres;Password=YOUR_PASSWORD"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-min-32-characters",
    "Issuer": "ConstructionTracker.API",
    "Audience": "ConstructionTracker.Client",
    "ExpirationInDays": 7
  }
}
```

⚠️ **Change SecretKey in production!**

---

## 🎯 Next Steps (Day 3+)

### Coming Soon
- [ ] Project CRUD operations
- [ ] Task management
- [ ] Team assignments
- [ ] File uploads
- [ ] Real-time notifications (SignalR)
- [ ] Reporting and analytics

---

## 🐛 Common Issues

**401 Unauthorized**
→ Check `Authorization: Bearer {token}` header format

**Email/subdomain exists**
→ Each must be unique, choose a different value

**Password validation fails**
→ Must have 8+ chars, uppercase, lowercase, digit, special char

---

## 🛠️ Development

```bash
# Build
dotnet build

# Run
dotnet run --project ConstructionTracker.API

# Watch mode (auto-restart)
dotnet watch --project ConstructionTracker.API
```

---

## 📦 Tech Stack

- **.NET 9** - Web API framework
- **PostgreSQL** - Database
- **Entity Framework Core 9** - ORM
- **JWT Bearer** - Authentication
- **ASP.NET Identity** - Password hashing
- **Swagger/OpenAPI** - API documentation
- **FluentValidation** - Input validation
- **MediatR** - CQRS pattern
- **Dapper** - Raw SQL queries

---

## ✅ Implementation Checklist

- [x] Multi-tenant architecture
- [x] JWT authentication
- [x] Company registration
- [x] User login
- [x] Protected endpoints
- [x] Role-based authorization
- [x] Password security
- [x] Swagger documentation
- [x] Clean architecture
- [x] Async operations

---

## 🎉 Status

**Day 2: JWT Authentication - COMPLETE!** ✅

The authentication system is production-ready with secure password hashing, JWT tokens, multi-tenant support, and comprehensive documentation.

**Ready for Day 3!** 🚀

---

**Built with ❤️ using .NET 9, PostgreSQL, and Entity Framework Core**
