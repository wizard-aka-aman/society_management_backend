namespace Society_Management_System.Model
{
    public class Notices
    {
        public int NoticesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedAt { get; set; }

        public int SocietyId { get; set; }
    }
}
