namespace Society_Management_System.Model
{
    public class Bookings
    {
        public int BookingsId { get; set; } 
        public Flats Flats { get; set; }
        public string Facility { get; set; }
        //public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApprovedBy { get; set; }

        public string? Reason { get; set; }


    }
} 
