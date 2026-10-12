# BookingSystem.Api

An ASP.NET Core Web API booking-system project, structured to separate API endpoints, application logic and data access. The repository also includes a dedicated test project.

## Repository structure

- `BookingSystem.Api/` — application source code
- `BookingSystem.Test/` — automated tests
- `BookingSystem.Api.sln` — Visual Studio solution

## Technologies

- C# and ASP.NET Core Web API
- .NET testing tools (see the test project for exact frameworks)

## Getting started

Install a compatible .NET SDK and restore the solution:

```bash
dotnet restore BookingSystem.Api.sln
dotnet build BookingSystem.Api.sln
dotnet test BookingSystem.Api.sln
```

Review the API project's configuration and connection settings before starting it. Do not commit local credentials or secrets.

## Purpose

This project demonstrates API organization, separation of responsibilities, and automated testing as part of my .NET development portfolio.

[Back to GitHub profile](https://github.com/nourromeo)
