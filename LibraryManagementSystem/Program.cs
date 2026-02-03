using System;
using LibraryManagementSystem.Core;
using LibraryManagementSystem.Models.Items;
using LibraryManagementSystem.Models.Users;
using LibraryManagementSystem.Models.Abstract;

namespace LibraryManagementSystem
{
    class Program
    {
        private static Library _library;
        private static string _currentMemberId = null; // Simulating logged-in user

        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║  Library Management System v1.0        ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            // Initialize library
            _library = new Library("City Central Library", "123 Main Street");

            // Load sample data
            LoadSampleData();

            // Main menu loop
            bool exit = false;
            while (!exit)
            {
                DisplayMainMenu();
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        MemberMenu();
                        break;
                    case "2":
                        LibrarianMenu();
                        break;
                    case "3":
                        _library.DisplayLibraryStatistics();
                        break;
                    case "0":
                        exit = true;
                        Console.WriteLine("\nThank you for using the Library Management System!");
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\n╔════════════════════════════════════════╗");
            Console.WriteLine("║           MAIN MENU                    ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.WriteLine("1. Member Portal");
            Console.WriteLine("2. Librarian Portal");
            Console.WriteLine("3. View Library Statistics");
            Console.WriteLine("0. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void MemberMenu()
        {
            // Simple login simulation
            if (_currentMemberId == null)
            {
                Console.Write("\nEnter Member ID (e.g., M001): ");
                _currentMemberId = Console.ReadLine();

                var member = _library.GetMemberById(_currentMemberId);
                if (member == null)
                {
                    Console.WriteLine("❌ Member not found!");
                    _currentMemberId = null;
                    return;
                }

                Console.WriteLine($"\n✓ Welcome, {member.Name}!");
            }

            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║        MEMBER PORTAL                   ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("1. Browse Available Items");
                Console.WriteLine("2. Search by Title");
                Console.WriteLine("3. Search by Author");
                Console.WriteLine("4. Borrow Item");
                Console.WriteLine("5. Return Item");
                Console.WriteLine("6. Renew Item");
                Console.WriteLine("7. Reserve Item");
                Console.WriteLine("8. View My Borrowed Items");
                Console.WriteLine("9. View My Reservations");
                Console.WriteLine("10. View My Fines");
                Console.WriteLine("11. Pay Fine");
                Console.WriteLine("12. Get Recommendations");
                Console.WriteLine("13. View My Dashboard");
                Console.WriteLine("0. Logout");
                Console.Write("\nEnter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _library.DisplayAvailableItems();
                        break;
                    case "2":
                        Console.Write("Enter title to search: ");
                        _library.SearchByTitle(Console.ReadLine());
                        break;
                    case "3":
                        Console.Write("Enter author to search: ");
                        _library.SearchByAuthor(Console.ReadLine());
                        break;
                    case "4":
                        Console.Write("Enter Item ID to borrow: ");
                        _library.BorrowItem(_currentMemberId, Console.ReadLine());
                        break;
                    case "5":
                        Console.Write("Enter Item ID to return: ");
                        _library.ReturnItem(_currentMemberId, Console.ReadLine());
                        break;
                    case "6":
                        Console.Write("Enter Item ID to renew: ");
                        _library.RenewItem(_currentMemberId, Console.ReadLine());
                        break;
                    case "7":
                        Console.Write("Enter Item ID to reserve: ");
                        _library.ReserveItem(_currentMemberId, Console.ReadLine());
                        break;
                    case "8":
                        _library.DisplayMemberBorrowedItems(_currentMemberId);
                        break;
                    case "9":
                        _library.DisplayMemberReservations(_currentMemberId);
                        break;
                    case "10":
                        _library.DisplayMemberFines(_currentMemberId);
                        break;
                    case "11":
                        Console.Write("Enter payment amount: $");
                        if (decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            _library.PayFine(_currentMemberId, amount);
                        }
                        break;
                    case "12":
                        _library.GetRecommendations(_currentMemberId);
                        break;
                    case "13":
                        var member = _library.GetMemberById(_currentMemberId);
                        member?.DisplayDashboard();
                        break;
                    case "0":
                        back = true;
                        _currentMemberId = null;
                        Console.WriteLine("\n✓ Logged out successfully");
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice");
                        break;
                }

                if (!back)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void LibrarianMenu()
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine("\n╔════════════════════════════════════════╗");
                Console.WriteLine("║       LIBRARIAN PORTAL                 ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.WriteLine("1. Add New Item");
                Console.WriteLine("2. Remove Item");
                Console.WriteLine("3. Register New Member");
                Console.WriteLine("4. View All Items");
                Console.WriteLine("5. View All Members");
                Console.WriteLine("6. View Overdue Items");
                Console.WriteLine("7. Search Item");
                Console.WriteLine("0. Back");
                Console.Write("\nEnter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewItem();
                        break;
                    case "2":
                        Console.Write("Enter Item ID to remove: ");
                        _library.RemoveItem(Console.ReadLine());
                        break;
                    case "3":
                        RegisterNewMember();
                        break;
                    case "4":
                        _library.DisplayAllItems();
                        break;
                    case "5":
                        _library.DisplayAllMembers();
                        break;
                    case "6":
                        _library.DisplayOverdueItems();
                        break;
                    case "7":
                        Console.Write("Enter title to search: ");
                        _library.SearchByTitle(Console.ReadLine());
                        break;
                    case "0":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice");
                        break;
                }

                if (!back)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        static void AddNewItem()
        {
            Console.WriteLine("\n--- Add New Item ---");
            Console.WriteLine("1. Physical Book");
            Console.WriteLine("2. E-Book");
            Console.WriteLine("3. Audio Book");
            Console.Write("Select type: ");

            var type = Console.ReadLine();

            Console.Write("Enter Item ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter Author: ");
            string author = Console.ReadLine();

            BorrowableItem item = type switch
            {
                "1" => new PhysicalBook(id, title, author, "ISBN-" + id),
                "2" => new EBook(id, title, author, 5.5m),
                "3" => new AudioBook(id, title, author, 480),
                _ => null
            };

            if (item != null)
            {
                _library.AddItem(item);
            }
            else
            {
                Console.WriteLine("❌ Invalid item type");
            }
        }

        static void RegisterNewMember()
        {
            Console.WriteLine("\n--- Register New Member ---");

            Console.Write("Enter Member ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            Console.WriteLine("Select Membership Type:");
            Console.WriteLine("1. Student");
            Console.WriteLine("2. Regular");
            Console.WriteLine("3. Premium");
            Console.Write("Choice: ");

            var typeChoice = Console.ReadLine();
            MembershipType membershipType = typeChoice switch
            {
                "1" => MembershipType.Student,
                "2" => MembershipType.Regular,
                "3" => MembershipType.Premium,
                _ => MembershipType.Regular
            };

            var member = new Member(id, name, email, phone, membershipType);
            _library.RegisterUser(member);
        }

        static void LoadSampleData()
        {
            Console.WriteLine("\n📚 Loading sample data...");

            // Add sample books
            _library.AddItem(new PhysicalBook("B001", "The Great Gatsby", "F. Scott Fitzgerald", "ISBN-001"));
            _library.AddItem(new PhysicalBook("B002", "1984", "George Orwell", "ISBN-002"));
            _library.AddItem(new PhysicalBook("B003", "To Kill a Mockingbird", "Harper Lee", "ISBN-003"));
            _library.AddItem(new EBook("E001", "Clean Code", "Robert Martin", 9.99m));
            _library.AddItem(new EBook("E002", "The Pragmatic Programmer", "Andrew Hunt", 12.99m));
            _library.AddItem(new AudioBook("A001", "Atomic Habits", "James Clear", 360));
            _library.AddItem(new AudioBook("A002", "Sapiens", "Yuval Noah Harari", 450));

            // Add sample members
            _library.RegisterUser(new Member("M001", "John Doe", "john@email.com", "555-0001", MembershipType.Regular));
            _library.RegisterUser(new Member("M002", "Jane Smith", "jane@email.com", "555-0002", MembershipType.Premium));
            _library.RegisterUser(new Member("M003", "Bob Johnson", "bob@email.com", "555-0003", MembershipType.Student));

            // Add sample librarian
            _library.RegisterUser(new Librarian("L001", "Alice Admin", "alice@library.com", "555-1000"));

            Console.WriteLine("✓ Sample data loaded successfully\n");
        }
    }
}