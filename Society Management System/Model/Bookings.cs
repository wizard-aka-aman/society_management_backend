namespace Society_Management_System.Model
{
    public class Bookings
    {
        public int BookingsId { get; set; } 
        public Flats Flats { get; set; }
        public string Facility { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApprovedBy { get; set; }


    }
} 
