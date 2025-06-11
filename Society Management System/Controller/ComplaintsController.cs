using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Model;
using Society_Management_System.Model.ComplaintsRepo;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ComplaintsController : Controller
    {

        
        private readonly SocietyContext _societyContext;
        private readonly IComplaintsRepository _complaintsRepository;

        public ComplaintsController(SocietyContext societyContext , IComplaintsRepository complaintsRepository)
        {
            _societyContext = societyContext;
            _complaintsRepository = complaintsRepository;
        }
        [HttpGet("GetAllComplaints/{id}")]
        public async Task<IActionResult> GetAllComplaints(int id)
        {
            if(id == 0)
            {
                return NotFound("id is 0");
            }
            return Ok(_complaintsRepository.GetAllComplaints(id));
        }

        [HttpGet("GetMyComplaints/{name}")]
        public async Task<IActionResult> GetMyComplaints(string name)
        {
            var complaints = _complaintsRepository.GetMyComplaints(name);
            return Ok(complaints);
        }

        [HttpPost("AddComplaints")]
        public async Task<IActionResult> AddComplaints([FromBody] ComplaintsDto complaints )
        {
             var complaint = await _complaintsRepository.AddComplaints(complaints);
            return Ok(complaint);
        }

        [HttpPut("UpdateComplaints/{id}")]
        public async Task<IActionResult> UpdateComplaints([FromBody] ComplaintsDto complaints , int id)
        {
            var complaint = await _complaintsRepository.UpdateComplaints(complaints , id);
            return Ok(complaint);
        }
    }
}
