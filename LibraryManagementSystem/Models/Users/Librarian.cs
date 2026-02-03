using System;
using System.Collections.Generic;
using LibraryManagementSystem.Models.Abstract;

namespace LibraryManagementSystem.Models.Users
{
    public class Librarian : User
    {
        public string EmployeeId { get; set; }
        public DateTime HireDate { get; set; }

        public Librarian(string userId, string name, string email, string phone)
            : base(userId, name, email, phone)
        {
            EmployeeId = userId;
            HireDate = DateTime.Now;
        }

        public override List<string> GetPermissions()
        {
            return new List<string>
            {
                "AddItems",
                "RemoveItems",
                "RegisterMembers",
                "ViewAllTransactions",
                "ManageFines",
                "GenerateReports"
            };
        }

        public override string GetUserRole()
        {
            return "Librarian";
        }

        public override void DisplayDashboard()
        {
            Console.WriteLine($"\n=== Librarian Dashboard ===");
            DisplayInfo();
            Console.WriteLine($"Employee ID: {EmployeeId}");
            Console.WriteLine($"Hire Date: {HireDate:yyyy-MM-dd}");
            Console.WriteLine("\nPermissions:");
            foreach (var permission in GetPermissions())
            {
                Console.WriteLine($"  • {permission}");
            }
        }
    }
}