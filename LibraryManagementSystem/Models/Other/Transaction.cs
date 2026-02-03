using System;

namespace LibraryManagementSystem.Models.Other
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string MemberId { get; set; }
        public string ItemId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal FineAmount { get; set; }
        public int RenewalCount { get; set; }
        public TransactionStatus Status { get; set; }

        public Transaction()
        {
            RenewalCount = 0;
            FineAmount = 0;
        }

        public int GetDaysOverdue()
        {
            if (ReturnDate.HasValue)
            {
                return Math.Max(0, (ReturnDate.Value - DueDate).Days);
            }
            else if (DateTime.Now > DueDate)
            {
                return (DateTime.Now - DueDate).Days;
            }
            return 0;
        }
    }

    public enum TransactionStatus
    {
        Active,
        Completed,
        Cancelled
    }
}