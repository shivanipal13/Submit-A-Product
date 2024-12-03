# Submit-A-Product
A CRUD application for product management with ASP.NET Core Razor Pages and MS-SQL database.

This is a CRUD application built with **ASP.NET Core Razor Pages** and **MS-SQL Database**. It features a panel for admin users to manage products, including functionalities to add, edit, and delete entries directly in a grid view. The project includes various validation checks to ensure data integrity.

## Features
- **CRUD Functionality:** Add, edit, delete, and display products.
- **Admin Panel:** Manage products through a user-friendly interface.
- **Grid View:** All entries are displayed in a grid view with inline editing and deletion.
- **Validation Checks:**
  - Only 10 entries allowed; the 11th entry is blocked.
  - Mandatory fields with ASP.NET Core validation.
  - MRP cannot be less than the product price.
  - Validations for proper email format and 10-digit mobile number.

## Technologies Used
- **Frontend:** ASP.NET Core Razor Pages
- **Backend:** MS-SQL
- **Language:** C#
- **IDE:** Visual Studio 2022

## Getting Started

### Prerequisites
- Install [Visual Studio 2022](https://visualstudio.microsoft.com/) with the ASP.NET and web development workload.
- Install MS-SQL Server and Management Studio.

### Setup Instructions
### Setup Instructions
1. Clone this repository to your local system.
2. Open the project in Visual Studio and update the database connection string in `appsettings.json`.
3. Run database migrations and start the application.

