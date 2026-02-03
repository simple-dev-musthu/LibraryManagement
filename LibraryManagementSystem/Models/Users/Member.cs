using System;
using System.Collections.Generic;
using LibraryManagementSystem.Models.Abstract;

namespace LibraryManagementSystem.Models.Users
{
    public class Member : User  // ← Changed from "Users" to "User"
    {
        public MembershipType MembershipType { get; set; }
        public List<string> BorrowedItemIds { get; set; }
        public decimal TotalFines { get; set; }
        public int MaxBorrowLimit { get; set; }

        public Member(string userId, string name, string email, string phone, MembershipType membershipType)
            : base(userId, name, email, phone)
        {
            MembershipType = membershipType;
            BorrowedItemIds = new List<string>();
            TotalFines = 0;
            SetBorrowLimit();
        }

        private void SetBorrowLimit()
        {
            MaxBorrowLimit = MembershipType switch
            {
                MembershipType.Student => 3,
                MembershipType.Regular => 5,
                MembershipType.Premium => 10,
                _ => 3
            };
        }

        public override List<string> GetPermissions()
        {
            return new List<string>
            {
                "BorrowItems",
                "ReturnItems",
                "ReserveItems",
                "ViewHistory"
            };
        }

        public override string GetUserRole()
        {
            return "Member";
        }

        public override void DisplayDashboard()
        {
            Console.WriteLine($"\n=== Member Dashboard ===");
            DisplayInfo();
            Console.WriteLine($"Membership: {MembershipType}");
            Console.WriteLine($"Books Borrowed: {BorrowedItemIds.Count}/{MaxBorrowLimit}");
            Console.WriteLine($"Outstanding Fines: ${TotalFines}");
        }

        public bool CanBorrowMore()
        {
            return BorrowedItemIds.Count < MaxBorrowLimit && TotalFines < 10;
        }
    }

    public enum MembershipType
    {
        Student,
        Regular,
        Premium
    }
}