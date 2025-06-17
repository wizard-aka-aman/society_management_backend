 
namespace Society_Management_System.Model
{
    public class Society
    {
        public int SocietyId { get; set; }
        public string Name { get; set; }

        public DateTime CreatedWhen { get; set; }
        
        public string? Admin { get; set; }
    }
}
