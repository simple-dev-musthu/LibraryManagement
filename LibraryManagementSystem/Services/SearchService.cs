using LibraryManagementSystem.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Services
{
    public class SearchService
    {
        public List<BorrowableItem> SearchByTitle(List<BorrowableItem> items, string searchTerm)
        {
            return items.Where(i => i.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<BorrowableItem> SearchByAuthor(List<BorrowableItem> items, string author)
        {
            return items.Where(i => i.Author.Contains(author, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<BorrowableItem> FilterByStatus(List<BorrowableItem> items, ItemStatus status)
        {
            return items.Where(i => i.Status == status).ToList();
        }

        public List<BorrowableItem> GetAvailableItems(List<BorrowableItem> items)
        {
            return items.Where(i => i.Status == ItemStatus.Available).ToList();
        }

        public void DisplaySearchResults(List<BorrowableItem> results)
        {
            if (results.Count == 0)
            {
                Console.WriteLine("No items found matching your search");
                return;
            }

            Console.WriteLine($"\n=== Search Results ({results.Count} items) ===");
            foreach (var item in results)
            {
                Console.WriteLine($"\nID: {item.ItemId}");
                Console.WriteLine($"Title: {item.Title}");
                Console.WriteLine($"Author: {item.Author}");
                Console.WriteLine($"Type: {item.GetItemType()}");
                Console.WriteLine($"Status: {item.Status}");
                Console.WriteLine("---");
            }
        }
    }
}