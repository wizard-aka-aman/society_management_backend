using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.BookingsRepo
{
    public class BookingRepository : IBookingRepository
    {
        private readonly SocietyContext _societyContext;
        public BookingRepository(SocietyContext societyContext)
        {
            _societyContext = societyContext;
        }

        public async Task<bool> AddBookings(BookingsDto bookings)
        {
            Flats flat = _societyContext.Flats.Include(e => e.Users).FirstOrDefault(e => e.Users.Name == bookings.Name);
            if (bookings.StartTime >= bookings.EndTime)
            {
                return false;
            }
            if (flat == null)
            {
                return false;
            }
            Bookings booking1 = new Bookings
            {
                CreatedAt = DateTime.Now,
                Status = "Pending",
                Flats = flat,
                EndTime = bookings.EndTime,
                StartTime = bookings.StartTime,
                Facility = bookings.Facility,
                ApprovedBy = ""
            };
            _societyContext.Bookings.Add(booking1);
            await _societyContext.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteBookings(int id)
        {
            Bookings booking = await _societyContext.Bookings.FirstOrDefaultAsync(e => e.BookingsId == id);

            if (booking == null)
            {
                return false;
            }
            _societyContext.Bookings.Remove(booking);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public List<Bookings> GetAllBookings(int id)
        {
            return _societyContext.Bookings.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.SocietyId == id).OrderByDescending(e => e.CreatedAt).ToList();
        }

        public List<Bookings> GetMyBookings(string name)
        {
            return _societyContext.Bookings.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name).OrderByDescending(e => e.CreatedAt).ToList();
        }

        public async Task<Bookings> UpdateBookings(BookingsDto bookings, int id)
        {
            Bookings booking = await _societyContext.Bookings.FindAsync(id);
            booking.Status = bookings.Status;
            if (booking.Status == "Approved")
            {
                booking.ApprovalDate = DateTime.Now;
                booking.ApprovedBy = bookings.Name;
            }
            else if (booking.Status == "Rejected")
            {
                booking.Reason = bookings.Reason;
                booking.ApprovalDate = DateTime.Now;
                booking.ApprovedBy = bookings.Name;

            }
            else
            {
                booking.ApprovalDate = null;
                booking.ApprovedBy = "";
            }
            _societyContext.Bookings.Update(booking);
            await _societyContext.SaveChangesAsync();
            return booking;

        }
        public  Dictionary<string ,int>  AdminTotalNumberBookings(int id) { 
            Dictionary<string , int > dict = new Dictionary<string , int >();
            var pending = _societyContext.Bookings.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.SocietyId == id && e.Status == "Pending").Count();
            var Approved = _societyContext.Bookings.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.SocietyId == id && e.Status == "Approved").Count();
            var Rejected = _societyContext.Bookings.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.SocietyId == id && e.Status == "Rejected").Count();

            dict["Rejected"] = Rejected;
            dict["Approved"] = Approved;
            dict["Pending"] = pending;
            return dict;
        } 
        public  Dictionary<string ,int>  MyTotalNumberBookings(string name) { 
            Dictionary<string , int > dict = new Dictionary<string , int >();
            var pending = _societyContext.Bookings.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name && e.Status == "Pending").Count();
            var Approved = _societyContext.Bookings.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name && e.Status == "Approved").Count();
            var Rejected = _societyContext.Bookings.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name && e.Status == "Rejected").Count();

            dict["Rejected"] = Rejected;
            dict["Approved"] = Approved;
            dict["Pending"] = pending;
            return dict;
        }
    }
}
