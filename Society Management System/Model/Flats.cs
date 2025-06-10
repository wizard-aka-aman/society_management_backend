namespace Society_Management_System.Model
{
    public class Flats
    {
        public int FlatsId { get; set; } 
        public string Block { get; set; }
        public int FlatNumber { get; set; }
        public Users? Users { get; set; }
        public int SocietyId { get; set; }
        public int FloorNumber { get; set; }
    }
}
