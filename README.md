<div align="center">

# 🖥️ Store Management System

![C#](https://img.shields.io/badge/C%23-Language-239120?logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-Framework-512BD4?logo=dotnet&logoColor=white)
![WinForms](https://img.shields.io/badge/WinForms-Desktop_UI-0078D4?logo=windows&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-Database-005C84?logo=mysql&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-Infrastructure-2496ED?logo=docker&logoColor=white)

<picture>
    <source srcset="readme_assets/demo.gif" type="image/gif">
    <img src="readme_assets/demo.png" alt="Store Management System demo" width="700">
</picture>

</div>

---

## 📌 Project Overview

**Store Management System** is a desktop application for managing retail store operations with role-based access for administrators and cashiers. Features full CRUD operations for users and inventory, password hashing, and audit logging of all operations.

---

## ✨ Features

### 👤 Administrator
- Manage users — add, update, delete, and view all accounts
- Manage full product inventory with categories, pricing, and stock
- View audit logs of all operations
- Manage suppliers and purchases

### 🛒 Cashier
- Browse and search product inventory
- Process sales and generate receipts
- View and filter products by category, price, and availability

### 🔒 Security
- BCrypt password hashing for all user accounts
- Role-based access control — administrators and cashiers have separate panels

---

## 🛠️ Tech Stack

| Technology | Usage |
|------------|-------|
| C# / .NET | Core application language and framework |
| WinForms | Desktop GUI |
| MySQL | Relational database for users and inventory |
| Docker | MySQL containerization |
| BCrypt | Password hashing |

---

## 🚀 Setup & Run

### Prerequisites
- .NET 7+ SDK
- Docker and Docker Compose

### 1. Clone the repository
```bash
git clone https://github.com/pero-grubac/Store-Management-System.git
cd Store-Management-System
```

### 2. Start the database
```bash
docker compose up -d
```

MySQL will automatically initialize with the schema from `Model/DDL.sql`.

### 3. Configure connection string

Copy `Util/appsettings.example.json` to `Util/appsettings.json` and update if needed:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost; Port=3306; Database=prodavnica; User Id=root; Password=root"
  }
}
```



### Default credentials

| Role | Username | Password |
|------|----------|----------|
| Administrator | admin | admin123 |

---

## 📂 Project Structure

```
Store-Management-System/
├── Model/              # Database models and DDL.sql
├── Prodavnica/         # Main project
│   ├── Database/       # Repository pattern and DAO implementations
│   ├── Forms/          # WinForms UI screens
│   ├── Language/       # Localization files
│   ├── Resources/      # Images and assets
│   └── Util/           # DBUtil, appsettings.json
├── readme_assets/      # README images and GIF
├── docker-compose.yml
└── README.md
```