namespace Society_Management_System.Model
{
    public class Recurring
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReccuringId { get; set; }
        public Bills? Bills { get; set; }
    }
}
