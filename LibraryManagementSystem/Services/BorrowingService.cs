using LibraryManagementSystem.Models.Abstract;
using LibraryManagementSystem.Models.Other;
using LibraryManagementSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Services
{
    public class BorrowingService
    {
        private List<LibraryManagementSystem.Models.Other.Transaction> _transactions;
        private NotificationService _notificationService;

        public BorrowingService()
        {
            _transactions = new List<LibraryManagementSystem.Models.Other.Transaction>();
            _notificationService = new NotificationService();
        }

        public bool BorrowItem(Member member, BorrowableItem item)
        {
            // Validation checks
            if (!member.CanBorrowMore())
            {
                Console.WriteLine("Cannot borrow: Limit reached or outstanding fines exceed $10");
                return false;
            }

            if (!item.CanBeBorrowed())
            {
                Console.WriteLine($"Item '{item.Title}' is not available for borrowing");
                return false;
            }

            // Create transaction
            var transaction = new LibraryManagementSystem.Models.Other.Transaction
            {
                TransactionId = GenerateTransactionId(),
                MemberId = member.UserId,
                ItemId = item.ItemId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(item.GetBorrowingPeriodInDays()),
                Status = TransactionStatus.Active
            };

            // Update item and member
            item.Status = ItemStatus.Borrowed;
            item.CurrentBorrowerId = member.UserId;
            member.BorrowedItemIds.Add(item.ItemId);

            _transactions.Add(transaction);

            Console.WriteLine($"\n✓ Successfully borrowed '{item.Title}'");
            Console.WriteLine($"Due Date: {transaction.DueDate:yyyy-MM-dd}");

            return true;
        }

        public bool ReturnItem(Member member, BorrowableItem item, FineService fineService)
        {
            var transaction = _transactions.FirstOrDefault(t =>
                t.ItemId == item.ItemId &&
                t.MemberId == member.UserId &&
                t.Status == TransactionStatus.Active);

            if (transaction == null)
            {
                Console.WriteLine("No active borrowing record found");
                return false;
            }

            // Calculate fine if overdue
            transaction.ReturnDate = DateTime.Now;
            if (transaction.ReturnDate > transaction.DueDate)
            {
                int daysOverdue = (transaction.ReturnDate.Value - transaction.DueDate).Days;
                decimal fine = fineService.CalculateFine(item, daysOverdue);

                transaction.FineAmount = fine;
                member.TotalFines += fine;

                Console.WriteLine($"\n⚠ Item returned {daysOverdue} days late");
                Console.WriteLine($"Fine: ${fine:F2}");
            }

            // Update status
            transaction.Status = TransactionStatus.Completed;
            item.Status = ItemStatus.Available;
            item.CurrentBorrowerId = null;
            member.BorrowedItemIds.Remove(item.ItemId);

            Console.WriteLine($"✓ Successfully returned '{item.Title}'");

            // Notify if someone is waiting
            _notificationService.NotifyNextInQueue(item);

            return true;
        }

        public bool RenewItem(Member member, BorrowableItem item)
        {
            var transaction = _transactions.FirstOrDefault(t =>
                t.ItemId == item.ItemId &&
                t.MemberId == member.UserId &&
                t.Status == TransactionStatus.Active);

            if (transaction == null)
            {
                Console.WriteLine("No active borrowing record found");
                return false;
            }

            if (transaction.RenewalCount >= 2)
            {
                Console.WriteLine("Maximum renewals (2) reached");
                return false;
            }

            if (DateTime.Now > transaction.DueDate)
            {
                Console.WriteLine("Cannot renew overdue items. Please return first.");
                return false;
            }

            transaction.DueDate = transaction.DueDate.AddDays(item.GetBorrowingPeriodInDays());
            transaction.RenewalCount++;

            Console.WriteLine($"✓ Item renewed. New due date: {transaction.DueDate:yyyy-MM-dd}");
            return true;
        }

        public List<LibraryManagementSystem.Models.Other.Transaction> GetMemberTransactions(string memberId)
        {
            return _transactions.Where(t => t.MemberId == memberId).ToList();
        }

        public List<LibraryManagementSystem.Models.Other.Transaction> GetOverdueTransactions()
        {
            return _transactions.Where(t =>
                t.Status == TransactionStatus.Active &&
                DateTime.Now > t.DueDate).ToList();
        }

        private string GenerateTransactionId()
        {
            return $"TXN{DateTime.Now:yyyyMMddHHmmss}{_transactions.Count + 1:D4}";
        }
    }
}