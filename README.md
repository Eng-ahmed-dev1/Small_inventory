# 🛒 Inventory Management System

<div align="center">

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-Windows%20Presentation%20Foundation-0078D4?style=for-the-badge&logo=windows&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![XAML](https://img.shields.io/badge/XAML-0C54C2?style=for-the-badge&logo=xaml&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)

A modern desktop application for managing inventory, products, and customer shopping carts built with WPF and Entity Framework Core.

[Features](#features) • [Installation](#installation) • [Usage](#usage) • [Database](#database) • [Screenshots](#screenshots)

</div>

---

## 📋 Table of Contents

- [About](#about)
- [Features](#features)
- [Technologies](#technologies)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)

---

## 🎯 About

The **Inventory Management System** is a comprehensive desktop application designed to streamline inventory operations, product management, and customer shopping experiences. Built using WPF (Windows Presentation Foundation) and Entity Framework Core, this system provides an intuitive interface for both administrators and customers.

### Key Highlights

- ✅ Role-based authentication (Admin/Customer)
- ✅ Real-time inventory tracking
- ✅ Shopping cart functionality
- ✅ Product management (CRUD operations)
- ✅ Automatic price calculation
- ✅ Modern and responsive UI

---

## ✨ Features

### 🔐 Authentication System
- Secure login with username and password
- Role-based access control
- User-specific dashboards

### 👨‍💼 Admin Dashboard
- **Product Management**
  - Add new products with name, description, and price
  - Real-time inventory updates
  - Product validation

### 🛍️ Customer Dashboard
- **Shopping Experience**
  - Browse available products
  - Add items to cart with quantity selection
  - View cart contents in real-time
  - Automatic stock validation
  - Dynamic price calculation

### 📊 Data Management
- Entity Framework Core for database operations
- SQL Server integration
- Automatic cart creation per session
- Stock quantity tracking

---

## 🛠️ Technologies

| Technology | Purpose |
|------------|---------|
| ![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet) | Framework |
| ![WPF](https://img.shields.io/badge/WPF-Desktop%20UI-0078D4?style=flat-square&logo=windows) | User Interface |
| ![C#](https://img.shields.io/badge/C%23-Programming-239120?style=flat-square&logo=c-sharp) | Language |
| ![EF Core](https://img.shields.io/badge/EF%20Core-ORM-512BD4?style=flat-square&logo=nuget) | Data Access |
| ![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-CC2927?style=flat-square&logo=microsoft-sql-server) | Database |
| ![XAML](https://img.shields.io/badge/XAML-UI%20Markup-0C54C2?style=flat-square) | UI Design |

---

## 📦 Prerequisites

Before running this application, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (Community, Professional, or Enterprise)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or higher)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (Optional)

### Required NuGet Packages

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
```

---

## 🚀 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/inventory-management-system.git
cd inventory-management-system
```

### 2. Open in Visual Studio

- Open `Inventory.sln` in Visual Studio 2022
- Restore NuGet packages (Visual Studio will do this automatically)

### 3. Configure Database Connection

Update the connection string in `InventoryDB.cs`:

```csharp
optionsBuilder.UseSqlServer("Data Source=YOUR_SERVER_NAME;Initial Catalog=Inventory;Integrated Security=True;Trust Server Certificate=True");
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name.

---

## 🗄️ Database Setup

### Method 1: Using Package Manager Console

```powershell
# Create initial migration
Add-Migration InitialCreate

# Update database
Update-Database
```

### Method 2: Using .NET CLI

```bash
# Create migration
dotnet ef migrations add InitialCreate

# Apply migration
dotnet ef database update
```

### Database Schema

#### Users Table
| Column | Type | Constraints |
|--------|------|-------------|
| User_id | int | Primary Key, Identity |
| UserName | nvarchar(250) | Required |
| Password | nvarchar(250) | Required |
| Role | nvarchar(250) | Admin/Customer |

#### Product Table
| Column | Type | Constraints |
|--------|------|-------------|
| Pro_id | int | Primary Key, Identity |
| Name | nvarchar(max) | Required |
| Description | nvarchar(max) | Required |
| Price | decimal(18,2) | Required |
| InStockQuantity | int | Default: 0 |

#### Cart Table
| Column | Type | Constraints |
|--------|------|-------------|
| Cart_id | int | Primary Key, Identity |
| Cust_id | int | Foreign Key → Users |
| TotalPrice | decimal(18,2) | Default: 0 |

#### Cart_item Table
| Column | Type | Constraints |
|--------|------|-------------|
| Cart_item_id | int | Primary Key, Identity |
| Cart_ID | int | Foreign Key → Cart |
| Pro_ID | int | Foreign Key → Product |
| Quantity | int | Nullable |

### Seed Data (Optional)

```sql
-- Insert Admin User
INSERT INTO Users (UserName, Password, Role) 
VALUES ('admin', 'admin123', 'Admin');

-- Insert Customer User
INSERT INTO Users (UserName, Password, Role) 
VALUES ('customer', 'customer123', 'Customer');

-- Insert Sample Products
INSERT INTO Product (Name, Description, Price, InStockQuantity)
VALUES 
('Laptop', 'High-performance laptop', 1200.00, 50),
('Mouse', 'Wireless mouse', 25.00, 200),
('Keyboard', 'Mechanical keyboard', 80.00, 150);
```

---

## 💻 Usage

### Running the Application

1. Press `F5` in Visual Studio or click the **Start** button
2. The Login window will appear

### Login Credentials

**Admin Account:**
- Username: `admin`
- Password: `admin123`
- Access: Product management

**Customer Account:**
- Username: `customer`
- Password: `customer123`
- Access: Shopping cart functionality

### Admin Workflow

1. Login with admin credentials
2. Add products:
   - Enter product name
   - Enter description
   - Enter price (decimal format)
3. Click "Add Product"
4. Product is saved to inventory

### Customer Workflow

1. Login with customer credentials
2. Browse products from dropdown
3. Enter desired quantity
4. Click "Add To Cart"
5. View cart items in the data grid
6. Total price updates automatically

---

## 📁 Project Structure

```
Inventory/
├── Model/
│   ├── Cart.cs              # Cart entity model
│   ├── Cart_item.cs         # Cart item entity model
│   ├── Product.cs           # Product entity model
│   ├── Users.cs             # User entity model
│   └── InventoryDB.cs       # DbContext configuration
├── Views/
│   ├── Login.xaml           # Login window UI
│   ├── Login.xaml.cs        # Login logic
│   ├── AdminDashBoard.xaml  # Admin dashboard UI
│   ├── AdminDashBoard.xaml.cs # Admin logic
│   ├── CustomerDashBoard.xaml # Customer dashboard UI
│   └── CustomerDashBoard.xaml.cs # Customer logic
├── App.xaml                 # Application resources
├── App.xaml.cs              # Application startup
└── Inventory.sln            # Solution file
```

---

## 🔧 Key Features Explained

### 1. **Automatic Cart Management**
Each customer login creates a new cart session:
```csharp
var newCart = new Cart { Cust_id = user.User_id };
db.Cart.Add(newCart);
db.SaveChanges();
```

### 2. **Stock Validation**
Prevents over-ordering:
```csharp
if (product.InStockQuantity < quantity) {
    MessageBox.Show("Not enough stock!");
    return;
}
```

### 3. **Dynamic Price Calculation**
Total price updates automatically:
```csharp
cart.TotalPrice = cart.CartItems.Sum(ci => 
    (decimal)(ci.Quantity * ci.Product.Price)
);
```

### 4. **Role-Based Navigation**
Users are directed to appropriate dashboards:
```csharp
if (user.Role == "Admin") {
    new AdminDashBoard().Show();
} else if (user.Role == "Customer") {
    new CustomerDashBoard(userId, cartId).Show();
}
```

---

## 🎨 UI Highlights

- **Modern Design**: Clean and intuitive interface
- **Data Grids**: Real-time cart item display
- **Validation**: User-friendly error messages
- **Responsive**: Adapts to window resizing
- **Professional Styling**: Consistent color scheme and typography

---

## 🐛 Known Issues

- Cart persists across sessions (by design)
- No product deletion feature in current version
- Stock quantity is global (not session-specific)

---

## 🚧 Future Enhancements

- [ ] Order history tracking
- [ ] Product search and filtering
- [ ] User profile management
- [ ] Invoice generation
- [ ] Email notifications
- [ ] Product images
- [ ] Multi-currency support
- [ ] Reporting and analytics

---

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Coding Standards

- Follow C# naming conventions
- Comment complex logic
- Write meaningful commit messages
- Test thoroughly before submitting

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

```
MIT License

Copyright (c) 2025 Your Name

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction...
```

---

## 👥 Authors

- **Eng-ahmed-dev1 ** - *Initial work* - [Eng-ahmed-dev1](https://github.com/Eng-ahmed-dev1)

---

##  Acknowledgments

- Microsoft for .NET and Entity Framework Core
- WPF Community for design inspiration
- Stack Overflow community for solutions

---

## 📧 Contact

For questions or support, please reach out:

- GitHub: [@Eng-ahmed-dev1](https://github.com/Eng-ahmed-dev1 )

---

<div align="center">

### ⭐ Star this repository if you find it helpful!

Made with ❤️ using .NET and WPF

</div>
