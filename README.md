# Mini Inventory System – .NET Web API

**Project Overview**

This project is a Mini Inventory Management System built using ASP.NET Core Web API and Entity Framework Core.
It manages Products, Customers, and Sales, with JWT-based authentication and secure endpoints.

**Technologies Used**

-> ASP.NET Core Web API

-> Entity Framework Core

-> SQL Server

-> JWT Authentication

-> Swagger (OpenAPI)

**Setup Instructions (How to Run the App)**

Prerequisites

Make sure you have the following installed:

-> .NET SDK (7.0 or later)

-> SQL Server

-> Visual Studio / VS Code

Clone the Repository

-> git clone <[github-repo-url](https://github.com/tamimachy/Mini_Inventory_System.git)>

-> cd Mini_Inventory_System

Configure Database

-> Update the connection string in appsettings.json:

    "ConnectionStrings": {
      "DefaultConnection": "Server=DESKTOP-O9RE5MQ\\SQLEXPRESS;Database=MiniInventoryDb;Trusted_Connection=True;TrustServerCertificate=True"
    },

Apply Database Migrations

-> Open Package Manager Console and run:

    Add-Migration InitialCreate

    Update-Database

This will create all required tables.

Run the Application

    dotnet run
  
The API will start at:

    https://localhost:7092

Access Swagger UI

Open your browser and navigate to:

    https://localhost:7092/swagger

**Authentication**

 Sample Login Credentials
 
    Username: admin
    Password: admin123

 Login Endpoint
   POST /api/auth/login
   
 After login, copy the JWT token and use it in Swagger:
     
     Authorization: Bearer <generated-token>

**All endpoints except login are secured.**

API Usage Notes

  **Product Management**
  
=>   Add, Update, Delete, and List products

=>   Product stock is automatically reduced after a successful sale

=>   Soft delete supported (if enabled)

  **Customer Management**
  
=> Add, Update, Delete, and List customers
  
  **Sales Module**
  
=> Create a new sale with multiple products

=> Validates stock availability

=> Simulates processing delay (3000ms)

=> Allows only 3 concurrent sales globally

=> Returns HTTP 429 if limit exceeded

  **Sales Report**
  
=> Get sales summary by date range

=> Returns:
    Total Sales
    Total Revenue
    Number of Transactions

**Notes**

Uses async/await for all database operations

Designed following clean architecture principles

Ready for interview evaluation

**Author**

Tamima Naznin Chy

GitHub: <[my-github-profile](https://github.com/tamimachy)>
