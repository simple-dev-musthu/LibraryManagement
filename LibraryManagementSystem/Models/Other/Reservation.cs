using System;

namespace LibraryManagementSystem.Models.Other
{
    public class Reservation
    {
        public string ReservationId { get; set; }
        public string MemberId { get; set; }
        public string ItemId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int QueuePosition { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }

    public enum ReservationStatus
    {
        Active,
        Notified,
        Fulfilled,
        Cancelled,
        Expired
    }
}