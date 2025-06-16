namespace Society_Management_System.Model.Dto_s
{
    public class BookingsDto
    {
        public string Name { get; set; }
        public string Facility { get; set; }
        //public DateTime BookingDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        public string? Reason { get; set; }
    }
}
