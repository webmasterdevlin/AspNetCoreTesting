# Writing Automated Tests in ASP.NET Core

- Note: Create an instance of the database before running the tests
- Name the database "MvcTestDB"

## Installing MS SQL Server Database using Docker

- Download or clone this repo https://github.com/webmasterdevlin/docker-compose-database
- install docker client for your OS
- Install Azure Data Studio
- Run the docker compose file of the MS SQL Server database
- Create a database named "MvcTestDb"

## EF Core Tools Installation

- dotnet tool install --global dotnet-ef

## Database Migration
- dotnet ef migrations add InitialCreate
- dotnet ef database update
