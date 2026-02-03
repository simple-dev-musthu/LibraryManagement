Library Management System

A robust, console-based Library Management System developed in C# using the .NET Framework. This application provides a streamlined terminal interface for managing book inventories, tracking member transactions, and maintaining a database of library records.

🚀 Features

- Book Inventory Management: Add, update, and remove book records from the system.
- Member Tracking: Register new members and manage their contact information.
- Borrowing & Returns: Log book issues and returns with automatic date tracking for due dates.
- Search Functionality: Quickly find books by title, author, or ISBN using optimized SQL queries.
- Secure Authentication: Role-based access for librarians and students with password protection.
- Transaction History: Maintain a digital passbook of all book movements, similar to a banking ledger.

🛠️ Technical Stack

- Language: C#
-  Framework: .NET Framework 4.6.1
- Database: Microsoft SQL Server
- IDE: Visual Studio 2017 or later

📋 Database Schema

The system relies on a SQL backend with the following core tables:

- `Books`: Stores ISBN, Title, Author, and Availability Status.
- `Members`: Stores MemberID, Name, and Contact details.
- `Circulation`: Tracks IssuedDate, ReturnDate, and associated Member/Book IDs.

⚙️ Installation & Setup

1. Clone the Repository:
bash
git clone https://github.com/yourusername/library-management-system.git




2. Database Setup:
- Open SQL Server Management Studio (SSMS).
- Execute the provided `Database.sql` script to create the tables and initial schema.


3. Configure Connection:
Open `App.config` and update the `connectionString` to point to your local SQL instance.


4. Build and Run:
- Open the `.sln` file in Visual Studio.
- Press `F5` to build and launch the console application.



🕹️ How to Use

The application uses a numeric menu-driven interface:

1. Login: Enter your credentials to access the librarian dashboard.
2. Manage Books: Choose options to add new stock or view current inventory.
3. Issue/Return: Enter the Member ID and Book ISBN to process a transaction.
4. Reports: Generate a summary of overdue books or member activity.

📝 License

Distributed under the MIT License. See `LICENSE` for more information.
