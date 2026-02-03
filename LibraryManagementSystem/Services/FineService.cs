using LibraryManagementSystem.Models.Abstract;
using LibraryManagementSystem.Models.Users;
using System;

namespace LibraryManagementSystem.Services
{
    public class FineService
    {
        public decimal CalculateFine(BorrowableItem item, int daysOverdue)
        {
            if (daysOverdue <= 0)
                return 0;

            decimal dailyFine = item.GetLateFeePerDay();
            decimal totalFine = dailyFine * daysOverdue;

            // Maximum fine cap
            decimal maxFine = 50.00m;
            return Math.Min(totalFine, maxFine);
        }

        public bool PayFine(Member member, decimal amount)
        {
            if (amount <= 0 || amount > member.TotalFines)
            {
                Console.WriteLine("Invalid payment amount");
                return false;
            }

            member.TotalFines -= amount;
            Console.WriteLine($"✓ Payment of ${amount:F2} received");
            Console.WriteLine($"Remaining balance: ${member.TotalFines:F2}");
            return true;
        }

        public void DisplayFineDetails(Member member)
        {
            Console.WriteLine($"\n=== Fine Details for {member.Name} ===");
            Console.WriteLine($"Total Outstanding: ${member.TotalFines:F2}");

            if (member.TotalFines >= 10)
            {
                Console.WriteLine("⚠ Borrowing suspended until fines are below $10");
            }
        }
    }
}