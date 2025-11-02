# Performance Evaluation System - Backend

A comprehensive REST API backend system for managing employee performance evaluations, goals, and activities. Built with ASP.NET Core 8.0, featuring Azure AD authentication, MySQL database integration, and a clean architecture pattern.

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Architecture](#architecture)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Configuration](#configuration)
- [Database](#database)
- [Authentication & Authorization](#authentication--authorization)
- [Logging](#logging)
- [CI/CD](#cicd)
- [Project Structure](#project-structure)

## 🎯 Overview

This backend system provides a robust API for managing performance evaluations in an organization. It enables administrators and employees to track goals, monitor activities, and conduct annual performance reviews with a comprehensive workflow management system.

### Key Capabilities

- **Employee Management**: Maintain employee records with department assignments
- **Performance Evaluations**: Create and manage annual performance evaluations
- **Goal Tracking**: Define, track, and manage employee goals with different categories and statuses
- **Activity Management**: Associate activities with goals to track progress
- **Role-Based Access Control**: Secure API endpoints with Azure AD authentication and role-based authorization
- **Soft Delete**: Preserve data integrity with soft delete functionality
- **Pagination**: Efficient data retrieval with paginated responses

## ✨ Features

- ✅ RESTful API architecture
- ✅ Azure AD integration for authentication
- ✅ Entity Framework Core with MySQL
- ✅ Repository pattern for data access
- ✅ Service layer for business logic
- ✅ DTOs for request/response mapping
- ✅ Global exception handling middleware
- ✅ CORS configuration
- ✅ Swagger/OpenAPI documentation
- ✅ Structured logging with Serilog
- ✅ Soft delete implementation
- ✅ Audit fields (CreatedBy, UpdatedBy, DeletedBy, etc.)
- ✅ Azure Pipelines CI/CD integration

## 🛠 Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: MySQL (via Pomelo.EntityFrameworkCore.MySql)
- **ORM**: Entity Framework Core 8.0.13
- **Authentication**: Microsoft Identity Web 3.9.2 (Azure AD)
- **Logging**: Serilog.AspNetCore 9.0.0 with MySQL sink
- **Documentation**: Swashbuckle.AspNetCore 8.1.2
- **CI/CD**: Azure DevOps Pipelines

## 🏗 Architecture

The project follows a clean architecture with clear separation of concerns:

```
├── Controllers/          # API endpoints
├── Services/             # Business logic layer
├── Repositories/         # Data access layer
├── Models/               # Domain models and DTOs
├── Interfaces/           # Service and repository contracts
├── Data/                 # DbContext and database configuration
├── Middleware/           # Custom middleware (Exception, CORS)
├── Extensions/           # Extension methods for configuration
├── Utils/                # Helpers, constants, enums, mappers
└── Configurations/       # Dependency injection setup
```

### Design Patterns

- **Repository Pattern**: Abstracts data access logic
- **Service Pattern**: Encapsulates business logic
- **DTO Pattern**: Separates API contracts from domain models
- **Dependency Injection**: Loosely coupled components
- **Middleware Pattern**: Cross-cutting concerns (logging, exceptions)

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- MySQL Server 8.0 or higher
- Azure AD App Registration (for authentication)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/yourusername/performance_evaluation_system_BE.git
cd performance_evaluation_system_BE
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Configure `appsettings.json`:
   - Set up MySQL connection string
   - Configure Azure AD settings
   - Configure Serilog settings

4. Run database migrations (if using EF migrations):
```bash
dotnet ef database update
```

5. Build and run the project:
```bash
dotnet build
dotnet run --project PACE/PACE.csproj
```

The API will be available at `https://localhost:5001` (or configured port).

6. Access Swagger documentation:
Navigate to `https://localhost:5001/swagger` to explore the API endpoints.

## 📡 API Endpoints

### Employees
- `GET /api/Employees` - Get paginated list of employees (Admin only)
- `GET /api/Employees/{id}` - Get employee by ID
- `POST /api/Employees` - Create new employee
- `PUT /api/Employees/{id}` - Update employee
- `DELETE /api/Employees/{id}` - Soft delete employee

### Evaluations
- `GET /api/Evaluations` - Get paginated list of evaluations (Admin only)
- `GET /api/Evaluations/withgoals/{employeeid}` - Get evaluation with goals by employee ID and year
- `POST /api/Evaluations/all` - Create evaluations for multiple employees (Admin only)
- `PUT /api/Evaluations/idemployee/{employeeid}` - Update evaluation (Admin only)

### Goals
- `GET /api/Goals/{id}` - Get goal by ID
- `POST /api/Goals` - Create new goal
- `PUT /api/Goals/{id}` - Update goal
- `DELETE /api/Goals/{id}` - Soft delete goal

### Activities
- `GET /api/Activities/{id}` - Get activity by ID
- `POST /api/Activities` - Create new activity
- `PUT /api/Activities/{id}` - Update activity
- `DELETE /api/Activities/{id}` - Soft delete activity

**Note**: All endpoints require authentication. Some endpoints have role-based restrictions (Admin/User).

## ⚙️ Configuration

### appsettings.json

Key configuration sections:

```json
{
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "ClientId": "your-client-id",
    "TenantId": "your-tenant-id"
  },
  "ConnectionStrings": {
    "AppConnection": "server=...;user=...;database=...;port=...;password=...;"
  },
  "Serilog": {
    // Logging configuration
  }
}
```

### Environment Variables

For production, use environment variables or Azure Key Vault to store sensitive configuration.

## 🗄 Database

### Entity Relationships

```
Employee (1) ──→ (N) Evaluation
Evaluation (1) ──→ (N) Goal
Goal (1) ──→ (N) Activity
```

### Key Entities

- **Employees**: Employee information with department assignment
- **Evaluations**: Annual performance evaluations with feedback
- **Goals**: Employee goals with categories, types, and status tracking
- **Activities**: Activities associated with goals to track progress

### Soft Delete

All entities implement soft delete functionality with:
- `IsActive` flag
- `DeletedBy` and `DeletedOn` audit fields
- Automatic filtering of deleted records

## 🔐 Authentication & Authorization

The API uses Azure AD for authentication:

- **Azure AD Integration**: Microsoft Identity Web
- **Roles**: 
  - `Admin`: Full access to all endpoints
  - `User`: Limited access to employee-specific data
- **Token Validation**: Automatic token validation via middleware
- **Authorization**: Role-based authorization on controllers

### Required Setup

1. Register application in Azure AD
2. Configure redirect URIs
3. Set up API permissions
4. Configure app roles (Admin, User)

## 📝 Logging

Structured logging implemented with Serilog:

- **Sinks**: Console and MySQL
- **Log Levels**: Configurable per namespace
- **Enrichment**: Machine name, thread ID, context
- **Storage**: Logs stored in MySQL database

Configure log levels in `appsettings.json` under the `Serilog` section.

## 🔄 CI/CD

Azure DevOps Pipeline configured for automated deployment:

- **Trigger**: Push to `main` branch
- **Build**: .NET build and publish
- **Deploy**: Automatic deployment to Azure Web App (Linux)
- **Runtime**: .NET Core 8.0

Pipeline file: `azure-pipelines.yml`

## 📁 Project Structure

```
PACE/
├── Controllers/          # API Controllers
│   ├── ActivitiesController.cs
│   ├── EmployeesController.cs
│   ├── EvaluationsController.cs
│   ├── GoalsController.cs
│   └── ErrorsController.cs
│
├── Services/             # Business Logic
│   ├── ActivityService.cs
│   ├── EmployeeService.cs
│   ├── EvaluationService.cs
│   ├── GoalService.cs
│   └── UserService.cs
│
├── Repositories/         # Data Access
│   ├── ActivityRepository.cs
│   ├── EmployeeRepository.cs
│   ├── EvaluationRepository.cs
│   └── GoalRepository.cs
│
├── Models/               # Domain & DTOs
│   ├── ActivityModels/
│   ├── EmployeeModels/
│   ├── EvaluationModels/
│   ├── GoalModels/
│   └── CommonModels/
│
├── Data/
│   └── PaceDbContext.cs
│
├── Middleware/
│   ├── ExceptionMiddleware.cs
│   └── CorsMiddleware.cs
│
├── Utils/
│   ├── Constants/
│   ├── Enums/
│   ├── Helpers/
│   └── Mappers/
│
└── Configurations/
    ├── DependencyInjection.cs
    └── ServiceConfiguration.cs
```

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is private and owned by **CGC**. All rights reserved.

## 👥 Team

Developed by **CGC**

---

⭐ If this project has been useful to you, consider giving it a star on GitHub.

