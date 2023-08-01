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