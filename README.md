
# MathHelper Project

## Overview

MathHelper is an educational application specifically designed for generating and practicing mathematical exercises. The app aims to provide math problems of various levels and types to support learners' development and motivation.

## Key Features

- **Math Exercise Generation**: Users can choose the type of exercises (addition, subtraction, multiplication, division, algebra, etc.) and the difficulty level.
- **Time Tracking**: The application measures the time taken to solve exercises.
- **Result Saving**: The application records the exercises, user ID, solution time, and exercise type in a database.
- **User Authentication**: Secure login and user data management using ASP.NET Core Identity.


## Technology Stack

- **Backend**: ASP.NET Core 8.0
  - Web API
  - Entity Framework Core (with SQL Server)
  - ASP.NET Core Identity
  - JWT-based authentication (Microsoft.AspNetCore.Authentication.JwtBearer)
- **Frontend**: React.js
  - Redux for state management
  - HTTPOnly cookies for secure authentication
- **Packages and Libraries**:
  - DotNetEnv
  - Scrutor (simplifying dependency injection)
  - Swashbuckle (OpenAPI/Swagger documentation)
- **Containerization**: Docker
- **Testing**:
  - Unit Testing
  - Integration Testing

## Using JWT Tokens

The application uses JWT tokens for authentication and authorization:

- **JWT Tokens**:
  - Generated using ASP.NET Core Identity.
  - Contains information for user identification and permissions.
  - Configurable lifespan, requiring re-authentication after expiration.

## Environment Variables (.env File)

Environment variables required for the application:

- **ConnectionStrings__DefaultConnection**: Database connection string. Example:
  ```
  Server=db;Database=YOURDATABASE;User Id=sa;Password=YOURPASSWORD;Encrypt=false;
  ```
  - `Server`: Database server name or address (e.g., `db` in a containerized setup).
  - `Database`: Name of the database.
  - `User Id`: Database username.
  - `Password`: Database password.
  - `Encrypt`: Encryption setting (use `false` in development).

- **SA_PASSWORD**: SQL Server administrator (SA) password. Example: `Macko1234`. Must be at least 8 characters long, including numbers and special characters.

- **ACCEPT_EULA**: SQL Server license acceptance. Value: `Y` (mandatory).

- **CERT_PASSWORD**: SSL certificate password. Example: `MYPassword`. Set during certificate creation.

- **AES_KEY**: AES encryption key. Example: `1234567893333444` (16 characters).

- **AES_IV**: AES initialization vector. Example: `1223334450123457` (16 characters).

- **CERT_PATH**: Path to the SSL certificate file. Example:
  ```
  Backend\MathHelper\MathHelperr\ssl\localhost.pfx
  ```

**SSL Certificate Notes**:
- Store SSL certificates securely and do not share them publicly.
- Avoid storing certificate passwords and AES keys in version control.
- Use a self-signed certificate in development, but a validated certificate in production.

## Project Structure

```
MathHelper/
├── Contracts/
├── Controller/
├── Data/
├── Migrations/
├── Model/
│   └── Db/
│       ├── AlgebraResult.cs
│       └── ExerciseResult.cs
├── Service/
│   ├── Authentication/
│   ├── Encription/
│   ├── Groq/
│   ├── LevelChecker/
│   ├── LevelProvider/
│   └── Math/
│       ├── AbstractImplement/
│       ├── Algebra/
│       ├── Division/
│       ├── Factory/
│       ├── IMathInterfaces/
│       ├── Multiplication/
│       └── RemainDivision/
├── Repository/
├── ssl/
├── Utility/
└── Program.cs
```

## Installation and Running

### Prerequisites

- Node.js
- .NET SDK 8.0
- Docker
- SQL Server

### Steps

1. **Backend Installation**
   ```bash
   cd Backend
   dotnet restore
   dotnet build
   dotnet run
   ```
2. **Frontend Installation**
   ```bash
   cd Frontend
   npm install
   npm start
   ```
3. **Database Configuration**
   Provide the SQL Server database details in the `appsettings.json` file.
4. **Using Docker**
   ```bash
   docker-compose up --build
   ```

## Usage

- Register a new account or log in with an existing one.
- Select the desired math operation and level.
- Start solving problems.
- View results and statistics in your profile.

## Development Guidelines

- **Clean Code**: Follow clean code principles.
- **SOLID Principles**: Ensure maintainability and extensibility of the application.
- **Factory Pattern**: Simplify adding new levels and exercises by leveraging the factory pattern.

## Future Developments

- Multiplayer mode.
- Gamification elements (e.g., points, badges).
- Mobile application development.
- Machine learning-based exercise level recommendations.

## Contribution

If you'd like to contribute to the project, please open an issue or submit a pull request. For questions, feel free to reach out.

---

**Created by:** Maci

