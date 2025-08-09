# Employee Management Task

## Overview
This project is a **.NET MVC web application** for managing employees and their dynamic custom properties.  
It allows you to:
- Create employees with both basic and custom properties.
- Edit employee data while only saving properties with actual values.
- Retrieve and display all employees with their filled properties only.

## Features
- **Dynamic Custom Properties**: Add properties of different types (Text, Integer, Date, Dropdown) from the system without code changes.
- **Create / Edit Employees** with validation for required fields.
- **Client-side & Server-side Validation**.
- **Reusable Service Layer** to keep controller code clean.
- **Dropdown options** loaded dynamically from property definitions.

## Technologies Used
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **Bootstrap 5**
- **jQuery Validation**

## Project Structure
```
/Controllers      -> MVC controllers (EmployeeController, etc.)
/Models           -> Domain models & ViewModels
/Services         -> Business logic and data handling
/Repositories     -> Data access layer
/Views            -> Razor views for Create, Edit, Index
```

## How to Run Locally
1. Clone the repository:
   ```bash
   git clone https://github.com/Reem-A-Hikal/Task.git
   ```
2. Navigate to the project folder:
   ```bash
   cd Task
   ```
3. Update `appsettings.json` with your SQL Server connection string.
4. Apply migrations and update the database:
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```

## Testing
You can test by:
- Adding a few property definitions in the database (e.g., `Department`, `Joining Date`, `Skill Level`).
- Creating a new employee and filling some of these properties.
- Editing the employee and verifying that only filled properties are saved.
