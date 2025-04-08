# Online Shopping Web Application

---

## Overview
This is an online shopping web application and stock management system that I built in Visual Studio, using ASP.NET MVC and C#. It is connected to a database developed in Microsoft's SQL Server Management Studio, containing tables that store data regarding user/administrative accounts, orders, and products. It also houses the various SQL statements for retrieving, adding, updating tables etc. These SQL statements are then called from the program through the use of repos.

Users are provided with a seamless experience, inluding browsing products, managing a shopping cart, and placing orders. Authenticating users allows for returning users to view previous orders. 

This application allows for administrative roles, upon authentication, administrators can manage stock, and add/remove products from the database.

---

## Features

### Customer Features
- **User Registration and Login**: Customers can register and log into their accounts.
- **Product Search**: Search for products by type or description using an autocomplete search box.
- **Shopping Cart**: Add products to the cart, edit quantities, and delete items.
- **Order Placement**: Place orders and view a confirmation page after finalizing the order.
- **Order History**: Returning customers can view their current and previous orders.

### Admin Features
- **Stock Management**: Administrators can view low-stock items, add new products, edit existing products, and delete products.
- **Order Stock**: Replenish stock for low-stock items with a single click.
- **Role-Based Access**: Admins have access to stock management features, while customers only see shopping-related features.

---

## Technologies Used
- **Backend**: ASP.NET Core MVC (C#)
- **Frontend**: Razor Views, HTML, CSS, JavaScript, jQuery, Bootstrap
- **Database**: Microsoft SQL Server
- **Authentication**: Cookie-based authentication with role-based access control
- **Dependency Injection**: Used for repository and service management
- **Libraries**:
  - Dapper for lightweight ORM
  - jQuery Validation for client-side form validation
  - Bootstrap for responsive UI design
- **Visual Studio**: IDE for development

---

## Project Structure
The project is organized as follows:<br/>
ShoppingToYou<br/> 
    ├── Controllers/            # MVC Controllers for handling requests<br/>
    ├── Database/               # Repositories and database connection logic<br/>
    ├── Models/                 # Data models for the application<br/>
    ├── Views/                  # Razor views for rendering UI<br/>
    ├── wwwroot/                # Static files (CSS, JS, images)<br/>
    ├── appsettings.json        # Configuration file for database connection<br/>
    ├── Startup.cs              # Application startup configuration<br/> 
    ├── Program.cs              # Main entry point of the application<br/>
    └── ShoppingToYou.csproj    # Project file

---

## Setup Instructions

### Prerequisites
- .NET 5.0 SDK or later
- Microsoft SQL Server
- Visual Studio 2022
- Node.js (optional, for managing frontend dependencies)

### Installation
1. Clone the repository:

   ```
   git clone https://github.com/your-username/Online-Shopping-Web-Application.git
   cd Online-Shopping-Web-Application
   ```

2. Open the solution in Visual Studio 2022

3. Configure the database connection:
    - Update the DefaultConnection string in appsettings.json to point to your SQL Server instance.

4. Run the database migrations to create the database schema:

    ```
    Update-Database
    ```

5. Restore dependencies:

    ```
    dotnet restore
    ```

6. Build the project:

    ```
    dotnet build
    ```

7. Run the application:

    ```
    dotnet run
    ```

### Usage
1. Register a new user account or log in with an exisiting account
2. Browse products and add them to your shopping cart
3. Proceed to checkout to place an order
4. Administrators can log in to access the admin panel for stock management

---

## Key Files
- **Startup.cs**: Configures services and middleware for the application.
- **SqlServerConnection.cs**: Manages the database connection.
- **StockController.cs**: Handles stock management features.
- **OrderController.cs**: Handles customer order-related features.
- **AccountController.cs**: Manages user authentication and registration.

---

## Acknowledgments
- [Bootstrap](https://getbootstrap.com) for responsive UI components.
- [Dapper](https://github.com/DapperLib/Dapper) for lightweight ORM functionality.
- [jQuery](https://jquery.com) for client-side scripting.

---

## Contributing
**Contributions are welcome!**\
Please fork the repository and submit a pull request with your changes.

---

## Contact
For any questions or feedback, feel free to reach out:
- **Email**: matty.tom@icloud.com
- **GitHub**: [Mattytomo365](https://github.com/Mattytomo365)

