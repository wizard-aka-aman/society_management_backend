using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Model.ComplaintsRepo;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Model;
using Society_Management_System.Model.BookingsRepo;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingsController : Controller
    {
        private readonly SocietyContext _societyContext;
        private readonly IBookingRepository _bookingRepository;

        public BookingsController(SocietyContext societyContext, IBookingRepository bookingRepository)
        {
            _societyContext = societyContext;
            _bookingRepository = bookingRepository;
        }
        [HttpGet("GetAllBookings/{id}")]
        public IActionResult GetAllBookings(int id)
        {
            if (id == 0)
            {
                return NotFound("id is 0");
            }
            return Ok(_bookingRepository.GetAllBookings(id));
        }

        [HttpGet("GetMyBookings/{name}")]
        public IActionResult GetMyBookings(string name)
        {
            var complaints = _bookingRepository.GetMyBookings(name);
            return Ok(complaints);
        }

        [HttpPost("AddBookings")]
        public async Task<IActionResult> AddBookings([FromBody] BookingsDto booking)
        {
            var complaint = await _bookingRepository.AddBookings(booking);
            return Ok(complaint);
        }

        [HttpPut("UpdateBookings/{id}")]
        public async Task<IActionResult> UpdateBookings([FromBody] BookingsDto booking, int id)
        {
            var complaint = await _bookingRepository.UpdateBookings(booking, id);
            return Ok(complaint);
        }
        [HttpDelete("DeleteBookings/{id}")]
        public async Task<IActionResult> DeleteBookings(int id)
        {
            var complaints =await _bookingRepository.DeleteBookings(id);
            return Ok(complaints);
        }

        [HttpGet("MyTotalNumberBookings/{name}")]
        public IActionResult MyTotalNumberBookings(string name)
        {
            var complaints = _bookingRepository.MyTotalNumberBookings(name);
            return Ok(complaints);
        }
        [HttpGet("AdminTotalNumberBookings/{id}")]
        public IActionResult AdminTotalNumberBookings(int id)
        {
            var complaints = _bookingRepository.AdminTotalNumberBookings(id);
            return Ok(complaints);
        }
    }
}
