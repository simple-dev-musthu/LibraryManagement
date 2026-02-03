using LibraryManagementSystem.Models.Items;
using LibraryManagementSystem.Models.Abstract;
using LibraryManagementSystem.Models.Users;
using LibraryManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Core
{
    public class Library
    {
        // Composition - Library HAS-A these components
        private List<BorrowableItem> _items;
        private List<User> _users;
        private BorrowingService _borrowingService;
        private ReservationService _reservationService;
        private FineService _fineService;
        private SearchService _searchService;
        private NotificationService _notificationService;
        private RecommendationService _recommendationService;

        public string LibraryName { get; set; }
        public string Address { get; set; }

        public Library(string name, string address)
        {
            LibraryName = name;
            Address = address;

            // Initialize collections
            _items = new List<BorrowableItem>();
            _users = new List<User>();

            // Initialize services
            _borrowingService = new BorrowingService();
            _reservationService = new ReservationService();
            _fineService = new FineService();
            _searchService = new SearchService();
            _notificationService = new NotificationService();
            _recommendationService = new RecommendationService();

            Console.WriteLine($"✓ Library System '{name}' initialized");
        }

        #region Item Management

        public void AddItem(BorrowableItem item)
        {
            if (_items.Any(i => i.ItemId == item.ItemId))
            {
                Console.WriteLine($"Item with ID {item.ItemId} already exists");
                return;
            }

            _items.Add(item);
            Console.WriteLine($"✓ Added '{item.Title}' to library");
        }

        public void RemoveItem(string itemId)
        {
            var item = _items.FirstOrDefault(i => i.ItemId == itemId);

            if (item == null)
            {
                Console.WriteLine("Item not found");
                return;
            }

            if (item.Status == ItemStatus.Borrowed)
            {
                Console.WriteLine("Cannot remove borrowed item");
                return;
            }

            _items.Remove(item);
            Console.WriteLine($"✓ Removed '{item.Title}' from library");
        }

        public BorrowableItem GetItemById(string itemId)
        {
            return _items.FirstOrDefault(i => i.ItemId == itemId);
        }

        public List<BorrowableItem> GetAllItems()
        {
            return _items;
        }

        public void DisplayAllItems()
        {
            Console.WriteLine($"\n=== All Items in {LibraryName} ===");
            Console.WriteLine($"Total Items: {_items.Count}");

            foreach (var item in _items)
            {
                Console.WriteLine($"\n[{item.ItemId}] {item.Title}");
                Console.WriteLine($"Author: {item.Author}");
                Console.WriteLine($"Type: {item.GetItemType()}");
                Console.WriteLine($"Status: {item.Status}");
            }
        }

        #endregion

        #region User Management

        public void RegisterUser(User user)
        {
            if (_users.Any(u => u.UserId == user.UserId))
            {
                Console.WriteLine($"User with ID {user.UserId} already exists");
                return;
            }

            _users.Add(user);
            Console.WriteLine($"✓ Registered {user.GetUserRole()}: {user.Name}");
        }

        public void RemoveUser(string userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                Console.WriteLine("User not found");
                return;
            }

            if (user is Member member && member.BorrowedItemIds.Count > 0)
            {
                Console.WriteLine("Cannot remove member with borrowed items");
                return;
            }

            _users.Remove(user);
            Console.WriteLine($"✓ Removed user: {user.Name}");
        }

        public User GetUserById(string userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        public Member GetMemberById(string memberId)
        {
            return _users.FirstOrDefault(u => u.UserId == memberId) as Member;
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public void DisplayAllMembers()
        {
            var members = _users.OfType<Member>().ToList();

            Console.WriteLine($"\n=== All Members ===");
            Console.WriteLine($"Total Members: {members.Count}");

            foreach (var member in members)
            {
                Console.WriteLine($"\n[{member.UserId}] {member.Name}");
                Console.WriteLine($"Membership: {member.MembershipType}");
                Console.WriteLine($"Books Borrowed: {member.BorrowedItemIds.Count}");
                Console.WriteLine($"Fines: ${member.TotalFines:F2}");
            }
        }

        #endregion

        #region Borrowing Operations

        public void BorrowItem(string memberId, string itemId)
        {
            var member = GetMemberById(memberId);
            var item = GetItemById(itemId);

            if (member == null)
            {
                Console.WriteLine("Member not found");
                return;
            }

            if (item == null)
            {
                Console.WriteLine("Item not found");
                return;
            }

            _borrowingService.BorrowItem(member, item);
        }

        public void ReturnItem(string memberId, string itemId)
        {
            var member = GetMemberById(memberId);
            var item = GetItemById(itemId);

            if (member == null)
            {
                Console.WriteLine("Member not found");
                return;
            }

            if (item == null)
            {
                Console.WriteLine("Item not found");
                return;
            }

            _borrowingService.ReturnItem(member, item, _fineService);
        }

        public void RenewItem(string memberId, string itemId)
        {
            var member = GetMemberById(memberId);
            var item = GetItemById(itemId);

            if (member == null || item == null)
            {
                Console.WriteLine("Member or Item not found");
                return;
            }

            _borrowingService.RenewItem(member, item);
        }

        public void DisplayMemberBorrowedItems(string memberId)
        {
            var member = GetMemberById(memberId);

            if (member == null)
            {
                Console.WriteLine("Member not found");
                return;
            }

            Console.WriteLine($"\n=== Borrowed Items - {member.Name} ===");

            if (member.BorrowedItemIds.Count == 0)
            {
                Console.WriteLine("No items currently borrowed");
                return;
            }

            foreach (var itemId in member.BorrowedItemIds)
            {
                var item = GetItemById(itemId);
                if (item != null)
                {
                    Console.WriteLine($"• {item.Title} by {item.Author}");
                }
            }
        }

        #endregion

        #region Reservation Operations

        public void ReserveItem(string memberId, string itemId)
        {
            var member = GetMemberById(memberId);
            var item = GetItemById(itemId);

            if (member == null || item == null)
            {
                Console.WriteLine("Member or Item not found");
                return;
            }

            _reservationService.ReserveItem(member, item);
        }

        public void DisplayMemberReservations(string memberId)
        {
            var reservations = _reservationService.GetMemberReservations(memberId);

            Console.WriteLine($"\n=== Your Reservations ===");

            if (reservations.Count == 0)
            {
                Console.WriteLine("No active reservations");
                return;
            }

            foreach (var res in reservations)
            {
                var item = GetItemById(res.ItemId);
                Console.WriteLine($"• {item?.Title} - Queue Position: {res.QueuePosition}");
            }
        }

        #endregion

        #region Search Operations

        public void SearchByTitle(string title)
        {
            var results = _searchService.SearchByTitle(_items, title);
            _searchService.DisplaySearchResults(results);
        }

        public void SearchByAuthor(string author)
        {
            var results = _searchService.SearchByAuthor(_items, author);
            _searchService.DisplaySearchResults(results);
        }

        public void DisplayAvailableItems()
        {
            var available = _searchService.GetAvailableItems(_items);
            Console.WriteLine($"\n=== Available Items ({available.Count}) ===");

            foreach (var item in available)
            {
                Console.WriteLine($"[{item.ItemId}] {item.Title} by {item.Author}");
            }
        }

        #endregion

        #region Fine Operations

        public void PayFine(string memberId, decimal amount)
        {
            var member = GetMemberById(memberId);

            if (member == null)
            {
                Console.WriteLine("Member not found");
                return;
            }

            _fineService.PayFine(member, amount);
        }

        public void DisplayMemberFines(string memberId)
        {
            var member = GetMemberById(memberId);

            if (member == null)
            {
                Console.WriteLine("Member not found");
                return;
            }

            _fineService.DisplayFineDetails(member);
        }

        #endregion

        #region Reports

        public void DisplayOverdueItems()
        {
            var overdueTransactions = _borrowingService.GetOverdueTransactions();

            Console.WriteLine($"\n=== Overdue Items Report ===");
            Console.WriteLine($"Total Overdue: {overdueTransactions.Count}");

            foreach (var transaction in overdueTransactions)
            {
                var member = GetMemberById(transaction.MemberId);
                var item = GetItemById(transaction.ItemId);

                Console.WriteLine($"\nMember: {member?.Name}");
                Console.WriteLine($"Item: {item?.Title}");
                Console.WriteLine($"Days Overdue: {transaction.GetDaysOverdue()}");
            }
        }

        public void GetRecommendations(string memberId)
        {
            var transactions = _borrowingService.GetMemberTransactions(memberId);
            var recommendations = _recommendationService.GetRecommendations(memberId, transactions, _items);
            _recommendationService.DisplayRecommendations(recommendations);
        }

        public void DisplayLibraryStatistics()
        {
            var totalItems = _items.Count;
            var availableItems = _items.Count(i => i.Status == ItemStatus.Available);
            var borrowedItems = _items.Count(i => i.Status == ItemStatus.Borrowed);
            var totalMembers = _users.OfType<Member>().Count();
            var overdueCount = _borrowingService.GetOverdueTransactions().Count;

            Console.WriteLine($"\n╔══════════════════════════════════════╗");
            Console.WriteLine($"║   {LibraryName} - Statistics      ║");
            Console.WriteLine($"╚══════════════════════════════════════╝");
            Console.WriteLine($"Total Items: {totalItems}");
            Console.WriteLine($"  └─ Available: {availableItems}");
            Console.WriteLine($"  └─ Borrowed: {borrowedItems}");
            Console.WriteLine($"Total Members: {totalMembers}");
            Console.WriteLine($"Overdue Items: {overdueCount}");
        }

        #endregion
    }
}