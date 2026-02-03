using LibraryManagementSystem.Models.Other;
using LibraryManagementSystem.Models.Abstract;
using LibraryManagementSystem.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Services
{
    public class ReservationService
    {
        private List<Reservation> _reservations;

        public ReservationService()
        {
            _reservations = new List<Reservation>();
        }

        public bool ReserveItem(Member member, BorrowableItem item)
        {
            // Check if already reserved by this member
            if (_reservations.Any(r => r.ItemId == item.ItemId && r.MemberId == member.UserId && r.Status == ReservationStatus.Active))
            {
                Console.WriteLine("You already have a reservation for this item");
                return false;
            }

            // Check queue length
            int queuePosition = _reservations.Count(r => r.ItemId == item.ItemId && r.Status == ReservationStatus.Active) + 1;

            var reservation = new Reservation
            {
                ReservationId = GenerateReservationId(),
                MemberId = member.UserId,
                ItemId = item.ItemId,
                ReservationDate = DateTime.Now,
                QueuePosition = queuePosition,
                Status = ReservationStatus.Active
            };

            _reservations.Add(reservation);
            item.Status = ItemStatus.Reserved;

            Console.WriteLine($"✓ Item reserved. Queue position: {queuePosition}");
            return true;
        }

        public bool CancelReservation(string reservationId)
        {
            var reservation = _reservations.FirstOrDefault(r => r.ReservationId == reservationId);

            if (reservation == null)
            {
                Console.WriteLine("Reservation not found");
                return false;
            }

            reservation.Status = ReservationStatus.Cancelled;

            // Update queue positions
            UpdateQueuePositions(reservation.ItemId);

            Console.WriteLine("✓ Reservation cancelled");
            return true;
        }

        public List<Reservation> GetMemberReservations(string memberId)
        {
            return _reservations.Where(r => r.MemberId == memberId && r.Status == ReservationStatus.Active).ToList();
        }

        public Reservation GetNextInQueue(string itemId)
        {
            return _reservations
                .Where(r => r.ItemId == itemId && r.Status == ReservationStatus.Active)
                .OrderBy(r => r.QueuePosition)
                .FirstOrDefault();
        }

        private void UpdateQueuePositions(string itemId)
        {
            var activeReservations = _reservations
                .Where(r => r.ItemId == itemId && r.Status == ReservationStatus.Active)
                .OrderBy(r => r.QueuePosition)
                .ToList();

            for (int i = 0; i < activeReservations.Count; i++)
            {
                activeReservations[i].QueuePosition = i + 1;
            }
        }

        private string GenerateReservationId()
        {
            return $"RES{DateTime.Now:yyyyMMddHHmmss}{_reservations.Count + 1:D4}";
        }
    }
}