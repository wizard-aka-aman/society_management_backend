using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Model.NoticesRepo;
using Society_Management_System.Model.SocietyRepo;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SocietyController : Controller
    {
        private readonly ISocietyRepository _societyRepository;

        public SocietyController(ISocietyRepository societyRepository)
        {
            _societyRepository = societyRepository;
        }
        [HttpPost("AddSociety")]
        public async Task<IActionResult> AddSociety([FromBody] SocietyDto dto)
        {
            var society = await _societyRepository.AddSociety(dto);
            if (!society)
            {
                return BadRequest("Flat Number already Exist");
            }
            return Ok(society);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var society = await _societyRepository.GetAll();
            return Ok(society);
        } 
        [HttpGet("GetSocietyDetail/{id}")]
        public async Task<IActionResult> GetSocietyDetail(int id)
        {
            var society = await _societyRepository.GetSocietyDetail(id);
            return Ok(society);
        } 

        [HttpPut("UpdateSociety/{id}")]
        public async Task<IActionResult> UpdateSociety([FromBody] SocietyDto dto, int id)
        {
            var society = await _societyRepository.UpdateSociety(dto, id);
            return Ok(society);
        }
        [HttpPut("changeSocietyName/{id}")]
        public async Task<IActionResult> changeSocietyName([FromBody] SocietyDto dto, int id)
        {
            var society = await _societyRepository.changeSocietyName(dto, id);
            return Ok(society);
        } 
        [HttpPut("changeSocietyNotifyBefore/{id}")]
        public async Task<IActionResult> changeSocietyNotifyBefore([FromBody] SocietyDto dto, int id)
        {
            var society = await _societyRepository.changeSocietyNotifyBefore(dto, id);
            return Ok(society);
        }

        [HttpDelete("DeleteSociety/{id}")]
        public async Task<IActionResult> DeleteSociety(int id)
        {
            var society = await _societyRepository.DeleteSociety(id);
            return Ok(society);
        }
    }
}
