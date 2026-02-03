using System;
using System.Collections.Generic;
using LibraryManagementSystem.Models.Abstract;

namespace LibraryManagementSystem.Models.Users
{
    public class Admin : User
    {
        public Admin(string userId, string name, string email, string phone)
            : base(userId, name, email, phone)
        {
        }

        public override List<string> GetPermissions()
        {
            return new List<string>
            {
                "FullAccess",
                "ManageLibrarians",
                "SystemConfiguration",
                "ViewAnalytics",
                "DataBackup"
            };
        }

        public override string GetUserRole()
        {
            return "Administrator";
        }

        public override void DisplayDashboard()
        {
            Console.WriteLine($"\n=== Administrator Dashboard ===");
            DisplayInfo();
            Console.WriteLine("\n🔐 Full system access granted");
        }
    }
}