using LibraryManagementSystem.Models.Abstract;
using System;

namespace LibraryManagementSystem.Models.Items
{
    public class EBook : BorrowableItem
    {
        public decimal FileSize { get; set; } // in MB
        public string Format { get; set; } // PDF, EPUB, MOBI
        public string DownloadLink { get; set; }

        public EBook(string itemId, string title, string author, decimal fileSize)
            : base(itemId, title, author)
        {
            FileSize = fileSize;
            Format = "PDF";
        }

        public override int GetBorrowingPeriodInDays()
        {
            return 21; // 3 weeks for ebooks
        }

        public override decimal GetLateFeePerDay()
        {
            return 0.25m; // Lower fee for digital items
        }

        public override string GetItemType()
        {
            return "E-Book";
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"File Size: {FileSize} MB");
            Console.WriteLine($"Format: {Format}");
        }
    }
}
