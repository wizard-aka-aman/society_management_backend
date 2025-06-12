using Microsoft.AspNetCore.Mvc; 
using Society_Management_System.Model;
using Society_Management_System.Model.BillsRepo;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BillsController : Controller
    {
        private readonly IBillsRepository _billsRepository;

        public BillsController(IBillsRepository billsRepository)
        {
            _billsRepository = billsRepository;
        }

        [HttpPost("addbills")]
        public async Task<IActionResult> AddFlats([FromBody] BillsDto bill)
        {
            var bill1 = await _billsRepository.AddBill(bill);
             
            return Ok(bill1);
        }

        [HttpGet("GetAllBills/{id}")]
        public async Task<IActionResult> GetAllBills(int id)
        {
            var bill = await _billsRepository.GetAllBills(id);
            return Ok(bill);
        }

        [HttpGet("GetMyBills/{name}")]
        public async Task<IActionResult> GetMyBills(string name)
        {
            var bill = await _billsRepository.GetMyBills(name);
            return Ok(bill);
        }

        [HttpDelete("deleteBill/{id}")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var bill = await _billsRepository.DeleteBill(id);
            return Ok(bill);
        }
        [HttpPut("updateBill/{id}")]
        public async Task<IActionResult> UpdateBill(BillsDto bill, int id)
        {
            var bill1 = await _billsRepository.UpdateBill(bill ,id);
            return Ok(bill1);
        }
    }
}
