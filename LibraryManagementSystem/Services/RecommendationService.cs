using LibraryManagementSystem.Models.Abstract;
using LibraryManagementSystem.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Services
{
    public class RecommendationService
    {
        public List<BorrowableItem> GetRecommendations(
            string memberId,
            List<LibraryManagementSystem.Models.Other.Transaction> transactions,
            List<BorrowableItem> allItems)
        {
            // Get member's borrowing history
            var borrowedItems = transactions
                .Where(t => t.MemberId == memberId)
                .Select(t => t.ItemId)
                .ToList();

            if (borrowedItems.Count == 0)
            {
                // Return popular items for new members
                return allItems.Take(5).ToList();
            }

            // Simple recommendation: items by same authors
            var borrowedAuthors = allItems
                .Where(i => borrowedItems.Contains(i.ItemId))
                .Select(i => i.Author)
                .Distinct()
                .ToList();

            var recommendations = allItems
                .Where(i => borrowedAuthors.Contains(i.Author) &&
                           !borrowedItems.Contains(i.ItemId) &&
                           i.Status == ItemStatus.Available)
                .Take(5)
                .ToList();

            return recommendations;
        }

        public void DisplayRecommendations(List<BorrowableItem> recommendations)
        {
            Console.WriteLine("\n=== Recommended for You ===");
            foreach (var item in recommendations)
            {
                Console.WriteLine($"• {item.Title} by {item.Author}");
            }
        }
    }
}