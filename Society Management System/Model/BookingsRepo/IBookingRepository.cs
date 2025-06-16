using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.BookingsRepo
{
    public interface IBookingRepository
    {
        List<Bookings> GetMyBookings(string name);
        List<Bookings> GetAllBookings(int id);

        Task<bool> AddBookings(BookingsDto bookings);

        Task<Bookings> UpdateBookings(BookingsDto bookings, int id);

        Task<bool> DeleteBookings(int id);
        Dictionary<string,int>  MyTotalNumberBookings(string name);
        Dictionary<string,int>  AdminTotalNumberBookings(int id); 
    }
}
