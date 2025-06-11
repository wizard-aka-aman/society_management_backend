using System;

namespace Society_Management_System.Model
{
    public class Bills
    {
        public int BillsId { get; set; }
        public Flats Flats { get; set; }
        public string Type { get; set; }
        public int Amount { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? GeneratedDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaidDate { get; set; }
        
    }
}
 
