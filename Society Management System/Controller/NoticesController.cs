using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Model.FlatsRepo;
using Society_Management_System.Model;
using Society_Management_System.Model.NoticesRepo;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NoticesController : Controller
    {
         
        private readonly INoticesReopsitory  _noticesReopsitory;

        public NoticesController(  INoticesReopsitory noticesReopsitory)
        { 
            _noticesReopsitory = noticesReopsitory;
        }
        [HttpPost("AddNotices")]
        public async Task<IActionResult> AddNotices([FromBody] NoticesDto flat)
        {
            var flat1 = await _noticesReopsitory.AddNotices(flat);
            if (!flat1)
            {
                return BadRequest("Flat Number already Exist");
            }
            return Ok(flat1);
        }

        [HttpGet("GetAllNotices/{id}")]
        public async Task<IActionResult> GetAllNotices(int id)
        {
            var complaints =  await _noticesReopsitory.GetAllNotices(id);
            return Ok(complaints);
        }


        [HttpPut("UpdateNotices/{id}")]
        public async Task<IActionResult> UpdateNotices([FromBody] NoticesDto flat, int id)
        {
            var complaint = await _noticesReopsitory.UpdateNotices(flat, id);
            return Ok(complaint);
        }

        [HttpDelete("DeleteNotices/{id}")]
        public async Task<IActionResult> DeleteNotices(int id)
        {
            var Flat = await _noticesReopsitory.DeleteNotices(id);
            return Ok(Flat);
        }
    }
}
