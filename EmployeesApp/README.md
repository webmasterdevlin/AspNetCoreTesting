# Writing Automated Tests in ASP.NET Core

## Installing MS SQL Server Database using Docker

- Download or clone this repo https://github.com/webmasterdevlin/docker-compose-database
- Install docker client for your OS
- Skip account creation
- Skip survey
- Run the docker compose file of the MS SQL Server database. You might need an admin privilege to run the command below.
```pwsh
docker-compose up -d
```
- Install Azure Data Studio or update your Azure Data Studio to the latest version
- Open Azure Data Studio and connect to the database server
- server: localhost,1433
- sql login
- username: sa
- password: Pass123!
- Trust Server Certificate: True
- Name: MSSQLServer
- Click Connect
- Right click on the databases folder below MSSQLServer instance and select "New Database (Preview)"
- Create two databases with "MvcTestDb" and "TestDb" names

## EF Core Tools Installation

- dotnet tool install --global dotnet-ef

## Database Migration
- dotnet ef migrations add InitialCreate
- dotnet ef database update

## Set up unit testing project
- creat a new xunit project and name it EmployeesApp.Tests
- delete all cs files in the project
- add reference to EmployeesApp
- add package FluentAssertions
- create a directory name Validation and AccountNumberValidationTests.cs file inside of it
- add package moq
- create a directory name Controller and EmployeesControllerTests.cs file inside of it

## Set up integration testing project
- create a new xunit project and name it EmployeesApp.IntegrationTests
- delete all cs files in the project
- add reference to EmployeesApp
- add package Microsoft.AspNetCore.Mvc.Testing
- add package Microsoft.EntityFrameworkCore.InMemory
- create TestingWebAppFactory.cs file in the root project
- add package FluentAssertions
- create EmployeesControllerIntegrationTests.cs file in the root project