using System;
using LibraryManagementSystem.Models.Abstract;

namespace LibraryManagementSystem.Models.Items
{
    public class PhysicalBook : BorrowableItem
    {
        public string ISBN { get; set; }
        public int PageCount { get; set; }
        public string ShelfLocation { get; set; }
        public BookCondition Condition { get; set; }

        public PhysicalBook(string itemId, string title, string author, string isbn)
            : base(itemId, title, author)
        {
            ISBN = isbn;
            Condition = BookCondition.Good;
        }

        public override int GetBorrowingPeriodInDays()
        {
            return 14; // 2 weeks for physical books
        }

        public override decimal GetLateFeePerDay()
        {
            return 0.50m; // $0.50 per day
        }

        public override string GetItemType()
        {
            return "Physical Book";
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"ISBN: {ISBN}");
            Console.WriteLine($"Location: {ShelfLocation}");
            Console.WriteLine($"Condition: {Condition}");
        }
    }

    public enum BookCondition
    {
        Excellent,
        Good,
        Fair,
        Poor,
        Damaged
    }
}