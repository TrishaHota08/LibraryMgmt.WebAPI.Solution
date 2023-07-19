# LibraryMgmt.WebAPI Project - README

This repository contains a WebAPI project built using ASP.NET Core Web API with a SQL Server database and Entity Framework Core as the ORM framework. The project follows the Repository and Unit of Work design patterns along with the Generic Repository pattern for handling database operations efficiently. It also implements JSON Web Token (JWT) for Authentication and Authorization.

## Purpose

The purpose of this WebAPI project is to provide a scalable backend solution for managing various entities and operations related to a library management system. The entities include Staff, Customer, Book, and Order, each having its own data structure and associated operations.

## Database and ORM

The project is integrated with a Microsoft SQL Server database, which serves as the storage for the various entities and their relationships. Entity Framework Core (EF Core) is utilized as the ORM framework to interact with the database and perform CRUD (Create, Read, Update, Delete) operations on the entities.

## Design Patterns

### Repository Pattern

The Repository pattern is used to separate the data access logic from the rest of the application. It provides a layer of abstraction that allows the application to work with data in a more flexible manner, without being directly tied to the database. Each entity has its own repository class, responsible for handling the specific data operations related to that entity.

### Unit of Work Pattern

The Unit of Work pattern is used to manage transactions and ensure consistency when multiple database operations are involved. It allows the application to treat multiple database operations as a single unit, providing atomicity and rollback capabilities in case of failures. The Unit of Work manages the underlying repositories and ensures that all changes are committed or rolled back together.

### Generic Repository Pattern

The Generic Repository pattern abstracts the data access logic to a generic level, reducing code duplication and promoting reusability. By creating a generic repository interface and implementing it for each entity, we can centralize few of the common data access operations such as Create and Delete.

## Authentication and Authorization

JSON Web Token (JWT) is used for both Authentication and Authorization in the WebAPI project. It provides a secure and stateless way of authenticating users and granting access to specific API endpoints based on their roles and permissions. JWT tokens are generated upon successful login and are included in subsequent API requests to validate the user's identity and access rights.

## Swagger API Documentation

The WebAPI project is equipped with Swagger, a powerful tool for API documentation to generates an interactive, user-friendly interface that lists all the available API endpoints, their parameters, and responses. 

## Data Transfer Objects (DTOs)

Data Transfer Objects (DTOs) are used to transfer only the required data between the client and the server, minimizing unnecessary data exposure and improving performance. Each entity  has corresponding DTO classes with a subset of properties that need to be exposed to clients.

## Database Tables

The WebAPI project manages the following database tables:

1. **Staff**: Contains columns for staffId(PK), staff name, staff email id, staff role, staff password

2. **Customer**: Contains columns for customerId(PK), customer name, customer ph number(Unique), customer email id

3. **Book**: Contains columns for BookId(PK),Book Title(Unique), Book Booking Status(Availability of book), Book Price

4. **Order**:  Contains columns for OrderId(PK), CustomerID(FK), StaffId(FK), Customer ID(FK),BookId(FK), Issue Date, Due Date.

## Libraries used:
For ORM framework and database connection:
1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.Tools
3. Microsoft.EntityFrameworkCore.SqlServer

For Authentication and authorization:
4.Microsoft.AspNetCore.Authentication.JwtBearer
5. System.IdentityModel.Tokens.Jwt

For API documentation:
1. Swashbuckle.AspNetCore
2. Swashbuckle.AspNetCore.Filter

## Getting Started:
1..Net Core 6
2. SQL Server 2016/2019
3. Visual Studio 2019/2022

## Steps To Setup:
To set up and run the WebAPI project, follow these steps:
1. Open the solution file(LibraryMgmt.WebAPI.Solution.sln) in Visual Studio.

2.Build the solution.

3.Once successfully built, Open the Package Manager Console tool, and set the default project to 'LibraryMgmt.WebAPI'.

4.Confirm that there is a initialsetup migration file present under the WEbAPI project's migration folder.If present, go to step 5 directly. Else run the command: 'add-migration intialSetup'. 

5.Once the add migration command is success, run the command: 'update-database'.

6.Once update database command is success, open the SQL Server Object Explorer tool in Visual Studio and check the Database 'LibDB_final' under '(localdb)\MSSQLLocalDB'.

7.Once done, run the 'LibraryMgmt.WebAPI' project using the built-in server: Kestrel.

NOTE: If running using the IIS Server, update the applicationUrl same as set for the built-in server.


## Few Assumptions made:
1. There is only single piece of each book present in the library.
2. Each customer is expected to have a unique phone number.
2. Under a single order, each customer can issue a single book.
3. Each customer can issue up to 5 books at max.
4. The due date is set as per the staff no specific duration is considered.
5. Staff id is set by default by the Database creation and no create, delete operations are allowed.
6. During placing of an order, the staff id is needed.
6. Staff details set by default:
Staff Id 	Staff Name	Staff Role 	Staff email				Password
1		Admin		Admin		admin@libraryuae.com		admin123
2		Intern	Intern	intern@libraryuae.com		intern123
3		Librarian	Librarian	librarian@libraryuae.com 	librarian123


## Conclusion

This WebAPI project provides a solid foundation for building a library management system, with a well-structured database and a design pattern-based approach to handle data access efficiently. The usage of JWT for authentication and authorization ensures secure and controlled access to API resources. 

For any questions or issues, feel free to reach out to my mail trisha.hota@gmail.com.