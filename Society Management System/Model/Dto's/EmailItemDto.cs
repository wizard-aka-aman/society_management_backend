namespace Society_Management_System.Model.Dto_s
{
    public class EmailItemDto
    {
        public string Email { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string DueDate { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public int NotifyBefore { get; set; }
        public int? BillId { get; set; }
        
    }
}
