using LibraryManagementSystem.Models.Abstract;
using System;

namespace LibraryManagementSystem.Models.Items
{
    public class AudioBook : BorrowableItem
    {
        public int DurationMinutes { get; set; }
        public string Narrator { get; set; }
        public string Format { get; set; } // MP3, AAC

        public AudioBook(string itemId, string title, string author, int durationMinutes)
            : base(itemId, title, author)
        {
            DurationMinutes = durationMinutes;
            Format = "MP3";
        }

        public override int GetBorrowingPeriodInDays()
        {
            return 14; // 2 weeks
        }

        public override decimal GetLateFeePerDay()
        {
            return 0.30m;
        }

        public override string GetItemType()
        {
            return "Audio Book";
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Duration: {DurationMinutes} minutes ({DurationMinutes / 60.0:F1} hours)");
            Console.WriteLine($"Format: {Format}");
            if (!string.IsNullOrEmpty(Narrator))
            {
                Console.WriteLine($"Narrator: {Narrator}");
            }
        }
    }
}