# 🏗️ Construction Project Tracker

A multi-tenant SaaS backend for construction project management built with **.NET 9** and **Clean Architecture**.

## 📋 Project Concept

A comprehensive construction project management system designed to help construction companies manage their projects, teams, tasks, and resources efficiently. The platform provides multi-tenant architecture allowing multiple companies to use the system while keeping their data completely isolated.

### Key Features

- **Multi-Tenant Architecture** - Each company operates in an isolated environment with their own data
- **JWT Authentication** - Secure token-based authentication with role-based access control
- **Project Management** - Create, track, and manage construction projects
- **Team Management** - Assign team members with different roles (Admin, Manager, Worker, Viewer)
- **Task Tracking** - Break down projects into manageable tasks with status tracking
- **Secure & Scalable** - Built with security best practices and designed to scale

### Technology Stack

- **.NET 9** - Modern web API framework
- **Entity Framework Core 9** - Object-relational mapping
- **SQLite/PostgreSQL** - Flexible database options
- **JWT Bearer Authentication** - Industry-standard token authentication
- **Swagger/OpenAPI** - Interactive API documentation
- **Clean Architecture** - Maintainable and testable code structure

### Architecture Layers

```
┌─────────────────────────────────────┐
│     API Layer (Controllers)         │  ← HTTP Endpoints
├─────────────────────────────────────┤
│  Application Layer (Business Logic) │  ← Use Cases, DTOs, Interfaces
├─────────────────────────────────────┤
│ Infrastructure Layer (Data Access)  │  ← Repositories, Services, EF Core
├─────────────────────────────────────┤
│    Domain Layer (Core Entities)     │  ← Business Entities, Rules
└─────────────────────────────────────┘
```

### Getting Started

```bash
# Clone the repository
git clone https://github.com/Chalana-98/Construction.WebAPI.git

# Navigate to project
cd Construction.WebAPI

# Build the solution
dotnet build

# Run the API
dotnet run --project ConstructionTracker.API

# Access Swagger UI
http://localhost:5103/swagger
```

---

**Built with ❤️ using .NET 9 and Clean Architecture principles**
