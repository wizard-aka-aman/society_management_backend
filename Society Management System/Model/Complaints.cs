namespace Society_Management_System.Model
{
    public class Complaints
    {
        public int ComplaintsId { get; set; }
        public Flats Flats { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateResolved { get; set; }
        public string? FeedBack { get; set; }
    }
}