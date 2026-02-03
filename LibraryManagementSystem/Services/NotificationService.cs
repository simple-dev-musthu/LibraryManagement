using LibraryManagementSystem.Models.Abstract;
using System;

namespace LibraryManagementSystem.Services
{
    public class NotificationService
    {
        public void NotifyNextInQueue(BorrowableItem item)
        {
            // In a real system, this would send email/SMS
            Console.WriteLine($"\n📧 Notification: '{item.Title}' is now available for the next person in queue");
        }

        public void NotifyOverdue(string memberName, string itemTitle, int daysOverdue)
        {
            Console.WriteLine($"\n⚠ OVERDUE NOTICE to {memberName}:");
            Console.WriteLine($"'{itemTitle}' is {daysOverdue} days overdue");
        }

        public void NotifyDueSoon(string memberName, string itemTitle, DateTime dueDate)
        {
            Console.WriteLine($"\n📅 REMINDER to {memberName}:");
            Console.WriteLine($"'{itemTitle}' is due on {dueDate:yyyy-MM-dd}");
        }
    }
}