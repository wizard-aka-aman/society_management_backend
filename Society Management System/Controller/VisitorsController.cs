using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Model.NoticesRepo;
using Society_Management_System.Model.VisitorsRepo;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VisitorsController : Controller
    {
        private readonly IVisitorsRepository  _visitorsRepository;

        public VisitorsController(IVisitorsRepository visitorsRepository)
        {
            _visitorsRepository = visitorsRepository;
        }

        [HttpPost("AddVisitors")]
        public async Task<IActionResult> AddVisitors([FromBody] VisitorsDto visitor)
        {
            var visitor1 = await _visitorsRepository.AddVisitors(visitor);
            if (!visitor1)
            {
                return BadRequest("Flat Number already Exist");
            }
            return Ok(visitor1);
        }

        [HttpGet("GetAllVisitors/{id}")]
        public async Task<IActionResult> GetAllVisitors(int id)
        {
            var complaints = await _visitorsRepository.GetAllVisitors(id);
            return Ok(complaints);
        }
        [HttpGet("GetOneVisitors/{id}")]
        public async Task<IActionResult> GetOneVisitors(int id)
        {
            var complaints = await _visitorsRepository.GetOneVisitors(id);
            return Ok(complaints);
        }


        [HttpPut("UpdateVisitors/{id}")]
        public async Task<IActionResult> UpdateVisitors([FromBody] VisitorsDto visitor, int id)
        {
            var complaint = await _visitorsRepository.UpdateVisitors(visitor, id);
            return Ok(complaint);
        }

        [HttpDelete("DeleteVisitors/{id}")]
        public async Task<IActionResult> DeleteVisitors(int id)
        {
            var Flat = await _visitorsRepository.DeleteVisitors(id);
            return Ok(Flat);
        }
    }
}
