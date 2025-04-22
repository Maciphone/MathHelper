<a id="readme-top"></a>
<div align="center">
<h1 align="center">Math Helper</h1>
  <img src="images/mathLogo.jpg" alt="MathHelper Logo" width="400" height="400" />
</div>

## Table of Contents

- [Overview](#overview)
- [Key Features](#key-features)
- [Built With](#built-with)
- [Environment Variables (.env File)](#environment-variables-env-file)
- [Project Structure](#project-structure)
- [Installation and Running](#installation-and-running)
- [Usage](#usage)
- [Development Guidelines](#development-guidelines)
- [Future Developments](#future-developments)
- [Contribution](#contribution)
- [Acknowledgments](#acknowledgments)

## Overview

MathHelper is an educational application specifically designed for generating and practicing mathematical exercises. The app aims to provide math problems of various levels and types to support learners' development and motivation.

## Key Features

- **Math Exercise Generation**: Users can choose the type of exercises (addition, subtraction, multiplication, division, algebra, etc.) and the difficulty level.
- **Time Tracking**: The application measures the time taken to solve exercises.
- **Result Saving**: The application records the exercises, user ID, solution time, and exercise type in a database.
- **User Authentication**: Secure login and user data management using ASP.NET Core Identity.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Built With

This section lists the major frameworks and libraries used to bootstrap the project.

- [![.NET Core][dotnet-core-shield]][dotnet-core-url]
- [![SQL Server][sql-shield]][sql-url]
- [![React][react-shield]][react-url]
- [![Redux][redux-shield]][redux-url]
- [![Docker][docker-shield]][docker-url]
- [![Swagger][swagger-shield]][swagger-url]

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Environment Variables (.env File)

```dotenv
ConnectionStrings__DefaultConnection=Server=db;Database=YOURDATABASE;User Id=sa;Password=YOURPASSWORD;Encrypt=false;
SA_PASSWORD=Macko1234
ACCEPT_EULA=Y
CERT_PASSWORD=MYPassword
AES_KEY=1234567893333444
AES_IV=1223334450123457
CERT_PATH=Backend/MathHelper/ssl/localhost.pfx
```

**SSL Certificate Notes**:

- Store certificates securely and never commit passwords or keys to version control.
- Use self-signed certificates for development and validated ones in production.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Project Structure

```
MathHelper/
├── Backend/
│   ├── Contracts/
│   ├── Controller/
│   ├── Data/
│   ├── Migrations/
│   ├── Model/
│   ├── Service/
│   ├── Repository/
│   ├── ssl/
│   └── Program.cs
├── Frontend/
│   ├── public/
│   └── src/
├── docker-compose.yml
└── README.md
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Installation and Running

### Prerequisites

- **Node.js**
- **.NET SDK 8.0**
- **Docker & Docker Compose**
- **SQL Server**

### Backend

```bash
cd Backend/MathHelper
dotnet restore
dotnet build
dotnet run
```

### Frontend

```bash
cd Frontend/mathHelper
npm install
npm start
```

### Using Docker

```bash
docker-compose up --build
```

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Usage

1. Register a new account or log in with an existing one.
2. Select the desired math operation and difficulty level.
3. Start solving problems.
4. View results and statistics in your profile.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Development Guidelines

- **Clean Code**: Follow clean code principles.
- **SOLID Principles**: Ensure maintainability and extensibility.
- **Factory Pattern**: Simplify adding new levels and exercises.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Future Developments

- Multiplayer mode
- Gamification elements (points, badges)
- Mobile application development
- ML-based exercise level recommendations

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Contribution

Contributions are welcome! Please open an issue or submit a pull request.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Acknowledgments

- [Choose an Open Source License](https://choosealicense.com)
- [Img Shields](https://shields.io)
- [GitHub Pages](https://pages.github.com)

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- MARKDOWN LINKS & IMAGES -->

[dotnet-core-shield]: https://img.shields.io/badge/.NET%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white
[dotnet-core-url]: https://dotnet.microsoft.com/
[sql-shield]: https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white
[sql-url]: https://www.microsoft.com/sql-server
[react-shield]: https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB
[react-url]: https://reactjs.org/
[redux-shield]: https://img.shields.io/badge/Redux-593D88?style=for-the-badge&logo=redux&logoColor=white
[redux-url]: https://redux.js.org/
[docker-shield]: https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white
[docker-url]: https://www.docker.com/
[swagger-shield]: https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black
[swagger-url]: https://swagger.io/
