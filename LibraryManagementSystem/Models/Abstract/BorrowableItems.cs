using System;

namespace LibraryManagementSystem.Models.Abstract
{
    public abstract class BorrowableItem
    {
        public string ItemId { get; set; }  // ← Make sure this line exists
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Publisher { get; set; }
        public ItemStatus Status { get; set; }
        public string CurrentBorrowerId { get; set; }

        protected BorrowableItem(string itemId, string title, string author)
        {
            ItemId = itemId;
            Title = title;
            Author = author;
            Status = ItemStatus.Available;
        }

        // Abstract methods
        public abstract int GetBorrowingPeriodInDays();
        public abstract decimal GetLateFeePerDay();
        public abstract string GetItemType();

        // Virtual methods
        public virtual bool CanBeBorrowed()
        {
            return Status == ItemStatus.Available;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Type: {GetItemType()}");
        }
    }

    public enum ItemStatus
    {
        Available,
        Borrowed,
        Reserved,
        UnderMaintenance,
        Lost
    }
}