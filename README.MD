# eApplication

eApplication is a simple to-do list application that implements various advanced software development techniques and patterns, including Domain-Driven Design (DDD), Test-Driven Development (TDD), Command Query Responsibility Segregation (CQRS), MediatR, and FluentMigration.

## Introduction

This project serves as an example of how to build a simple to-do list application using best practices in software architecture and design. It leverages the following technologies and patterns:

- **DDD (Domain-Driven Design)**: Structuring the core logic of the application around the business domain.
- **TDD (Test-Driven Development)**: Writing tests before writing the actual code to ensure quality and reliability.
- **CQRS (Command Query Responsibility Segregation)**: Separating the write and read operations to optimize performance and scalability.
- **MediatR**: A simple mediator pattern implementation for .NET.
- **FluentMigration**: A migration framework for managing database schema changes.

## Project Structure

The project is structured as follows:
- `Application`: Contains the application logic, including commands, queries, and services.
- `Domain`: Contains the domain models and business logic.
- `Infrastructure`: Contains the implementation details, such as data access and migrations.
- `Web.Api`: Contains the web API controllers and configurations.
- `Test`: Contains the tests.

## Key Concepts

### Domain-Driven Design (DDD)
The core business logic is structured around the domain, with clear boundaries and entities that represent the business concepts.

### Test-Driven Development (TDD)
Tests are written before the actual code to ensure that the application meets the requirements and is free of defects.

### Command Query Responsibility Segregation (CQRS)
The application separates the write and read operations, optimizing performance and scalability.

### MediatR
MediatR is used to implement the mediator pattern, facilitating communication between components and reducing dependencies.

### FluentMigration
FluentMigration is used to manage database schema changes, ensuring that the database is always in sync with the application.
