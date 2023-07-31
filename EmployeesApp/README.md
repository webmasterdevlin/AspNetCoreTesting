# Writing Automated Tests in ASP.NET Core

- Note: Create an instance of the database before running the tests
- Name the database "MvcTestDB"

## Installing Database using Docker

- https://github.com/webmasterdevlin/docker-compose-database
- install docker client for your OS
- Install Azure Data Studio
- docker commands for each database are located on each Readme.md file

## Commands

- dotnet tool install --global dotnet-ef
- dotnet ef migrations add InitialCreate
- dotnet ef database update
