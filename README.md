# Upesi Backend

Upesi Backend is a transaction processing system designed to facilitate secure and efficient financial transactions. It includes user authentication, funds transfer capabilities, and ATM withdrawals, ensuring users have adequate funds before processing transactions.

## Features

- **Secure User Authentication**: Supports user registration secure login and Single Sign-On (SSO).
- **Funds Transfer**: Check balances and transfer funds between accounts securely.
- **ATM Withdrawals**: Ensure both user balance and ATM capabilities before processing withdrawals.

## Getting Started

These instructions will help you get a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [MySQL](https://www.mysql.com/downloads/)

### Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/kamula/upesi_backend.git
   ```
2. Navigate to the project directory:
   ```bash
   cd upesi_backend
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Set up the database (ensure MySQL is running):
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```

## Tests

Run automated tests for this system with:
```bash
dotnet test
```

## Deployment

Deploy using Docker:
```bash
docker pull kamula/isaac_repo:latest
docker run -p 8080:80 kamula/isaac_repo:latest
```

## Built With

- **[ASP.NET 8](https://dotnet.microsoft.com/apps/aspnet)** - Web framework used.
- **[MySQL](https://www.mysql.com/)** - Database management.
- **[Docker](https://www.docker.com/)** - Used for deployment.


```
