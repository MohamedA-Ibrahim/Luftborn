# Luftborn.Server

A modern .NET 8 web application with a Clean Architecture design pattern, serving as a backend API for the Luftborn application.

## Architecture

The project follows Clean Architecture principles with the following structure:

### Core Layer
- **Domain**: Contains enterprise business rules, entities, and domain models 
- **Application**: Contains business rules, interfaces, DTOs, and CQRS handlers using MediatR

### Infrastructure Layer
- **Persistence**: Database context, migrations, and repositories
- **Services**: Implementation of application services
- **Middleware**: Custom middleware including exception handling

### Presentation Layer
- **Controllers**: API endpoints for the application

## Technologies

- .NET 8
- Entity Framework Core 8 with SQL Server
- MediatR for CQRS pattern
- FluentValidation for input validation 
- Swagger for API documentation

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server
- Node.js (for the client application)

### Setup and Run

1. Clone the repository:
   ```
   git clone [repository-url]
   cd Luftborn
   ```

2. Update the connection string in `Luftborn.Server/appsettings.json` if necessary.

3. Run the server application:
   ```
   cd Luftborn.Server
   dotnet run
   ```

4. The application will be available at:
   - API: https://localhost:7080
   - Swagger UI: https://localhost:7080/swagger

5. To run with the client application:
   ```
   dotnet run --project Luftborn.Server
   ```
   This will start both the server and client applications using the SPA proxy.

## Database Migrations

The application uses Entity Framework Core migrations to manage the database schema:

```
cd Luftborn.Server
dotnet ef database update
```

To create a new migration:

```
dotnet ef migrations add [MigrationName]
```

## Project Structure

- `Core/Domain`: Contains entities like Todo
- `Core/Application`: Contains interfaces, CQRS commands/queries
- `Infrastructure/Persistence`: Database context and repositories
- `Presentation/Controllers`: API controllers 