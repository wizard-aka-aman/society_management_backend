namespace Society_Management_System.Model
{
    public class Visitors
    {
        public int VisitorsId { get; set; }
        public string Name { get; set; }

        public string Purpose { get; set; }
        
        public Flats? Flats { get; set; }

        public DateTime VisitDateTime { get; set; }
        public int SocietyId { get; set; }


    }
}
 