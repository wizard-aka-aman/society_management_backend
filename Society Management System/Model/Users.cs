namespace Society_Management_System.Model
{
    public class Users
    {
        public int UsersId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public int? SocietyId { get; set; }

    }
}
