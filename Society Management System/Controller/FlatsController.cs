using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Model.ComplaintsRepo;
using Society_Management_System.Model;
using Society_Management_System.Model.FlatsRepo;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlatsController : Controller
    {

        private readonly SocietyContext _societyContext;
        private readonly IFlatsRepository _flatsRepository;

        public FlatsController(SocietyContext societyContext, IFlatsRepository flatsRepository)
        {
            _societyContext = societyContext;
            _flatsRepository =  flatsRepository;
        }
        [HttpPost("AddFlats")]
        public async Task<IActionResult> AddFlats([FromBody] FlatsDto flat)
        {
            var flat1 =await _flatsRepository.AddFlats(flat);
            if (!flat1)
            {
                return BadRequest("Flat Number already Exist");
            }
            return Ok(flat1);
        }

        [HttpGet("GetAllFlats/{id}")]
        public async Task<IActionResult> GetAllFlats(int id)
        {
            var complaints = _flatsRepository.GetAllFlats(id);
            return Ok(complaints);
        }
 

        [HttpPut("UpdateFlats/{id}")]
        public async Task<IActionResult> UpdateFlats([FromBody] FlatsDto flat, int id)
        {
            var complaint = await _flatsRepository.UpdateFlats(flat, id);
            return Ok(complaint);
        }

        [HttpDelete("DeleteFlats/{id}")]
        public async Task<IActionResult> DeleteFlats(int id)
        {
            var Flat =await _flatsRepository.DeleteFlats(id);
            return Ok(Flat);
        }
    }
}
