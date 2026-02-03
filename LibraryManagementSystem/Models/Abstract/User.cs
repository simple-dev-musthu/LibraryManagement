using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Models.Abstract
{
    public abstract class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        protected User(string userId, string name, string email, string phone)
        {
            UserId = userId;
            Name = name;
            Email = email;
            Phone = phone;
            RegistrationDate = DateTime.Now;
            IsActive = true;
        }

        // Abstract methods - each user type implements differently
        public abstract List<string> GetPermissions();
        public abstract string GetUserRole();
        public abstract void DisplayDashboard();

        // Virtual method - can be overridden if needed
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"User ID: {UserId}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Role: {GetUserRole()}");
        }
    }
}