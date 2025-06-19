using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Society_Management_System.Model;
using Society_Management_System.Model.BillsRepo;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Model.JobRepo;
using Society_Management_System.Services.EmailService;

namespace Society_Management_System.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {

        private readonly IEmailService _emailService;
        private readonly SocietyContext _societyContext;
        private readonly IJobRepository _jobRepository;
        private readonly IBillsRepository _billsRepository;


        public JobController(IEmailService emailService, SocietyContext societyContext, IJobRepository jobRepository, IBillsRepository billsRepository)
        {
            _emailService = emailService;
            _societyContext = societyContext;
            _jobRepository = jobRepository;
            _billsRepository = billsRepository;

        }

        [HttpPost]
        [Route("CreateBackgroundJob")]
        public ActionResult CreateBackgroundJob()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Turant : CreateBackgroundJob"));
            return Ok();
        }

        [HttpPost]
        [Route("CreateScheduleJob")]
        public async Task<ActionResult> CreateScheduleJob([FromBody] EmailItemDto item)
        {
            return await _jobRepository.CreateScheduleJob(item);


        }
        [HttpDelete("DeleteJobEmail/{id}")]
        public async Task<IActionResult> DeleteJobEmail(int id)
        {
            Alarms alarm = await _societyContext.Alarms.FirstOrDefaultAsync(e => e.Id == id);
            if (alarm == null)
            {
                return NotFound();
            }
            BackgroundJob.Delete(alarm.Jobid.ToString());
            var allAlarm = _societyContext.Alarms.Where(e => e.Jobid == alarm.Jobid).ToList();
            _societyContext.Alarms.RemoveRange(allAlarm);
            await _societyContext.SaveChangesAsync();
            return Ok();

        }
        //[HttpPost]
        //[Route("CreateContinuationJob")]
        //public ActionResult CreateContinuationJob()
        //{
        //    var seheduleDateTime = DateTime.UtcNow.AddSeconds(5);
        //    var dateTimeOffset = new DateTimeOffset(seheduleDateTime);
        //    var jobId = BackgroundJob.Schedule(() => Console.WriteLine("5 second delay") , dateTimeOffset);

        //    var jobId2 = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continue job 1 Triggered"));

        //    var jobId3 = BackgroundJob.ContinueJobWith(jobId2, () => Console.WriteLine("Continue job 2 Triggered")); 

        //    var jobId4 = BackgroundJob.ContinueJobWith(jobId3, () => Console.WriteLine("Continue job 3 Triggered"));


        //    return Ok();
        //}

        [HttpGet]
        [Route("CreateRecurringJob/{Billid}/{NotifyBefore}")]
        public async Task<ActionResult> CreateRecurringJobAsync(int Billid, int NotifyBefore)
        {
            Recurring alreadyBillExistInRecurring = await _societyContext.Recurring.Include(e => e.Bills).ThenInclude(e => e.Flats).ThenInclude(e => e.Users).FirstOrDefaultAsync(e => e.Bills.BillsId == Billid);
            Bills bill;
            if (alreadyBillExistInRecurring != null)
            {
                bill = alreadyBillExistInRecurring.Bills;
            }
            else
            {
                bill = await _societyContext.Bills.Include(e => e.Flats).ThenInclude(e => e.Users).FirstOrDefaultAsync(e => e.BillsId == Billid);
            }
            BillsDto billdto = new BillsDto()
            {
                Amount = bill.Amount,
                NotifyBefore = NotifyBefore,
                DueDate = DateTime.Now,
                Name = bill.Flats.Users.Name,
                Type = bill.Type,
            };

            await _societyContext.SaveChangesAsync();
            Recurring recurring = new Recurring
            {
                Bills = bill,
                Name = bill.Flats.Users.Name,
                ReccuringId = bill.BillsId.ToString(),
            };
            _societyContext.Add(recurring);
            await _societyContext.SaveChangesAsync();
            RecurringJob.AddOrUpdate(recurring.ReccuringId, () => _billsRepository.AddBill(billdto),
            //"0 * * * *");
            "0 0 1 * * ");

            return Ok();
        }

        [HttpGet]
        [Route("StopRecurringJob/{billId}")]
        public async Task<ActionResult> StopRecurringJobAsync(int billId)
        {
            try
            {
                var recurring = await _societyContext.Recurring
                    .Include(e => e.Bills)
                    .FirstOrDefaultAsync(e => e.Bills != null && e.Bills.BillsId == billId);

                if (recurring == null)
                    return NotFound("Recurring job not found.");

                RecurringJob.RemoveIfExists(recurring.ReccuringId.ToString()); // Convert to string if needed
                _societyContext.Recurring.Remove(recurring);
                await _societyContext.SaveChangesAsync();

                return Ok(new { message = "Recurring job stopped." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error occurred: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("GetAllRecurring/{societyId}")]
        public async Task<ActionResult> GetAllRecurring(int societyId)
        {
            var allRecurring = _societyContext.Recurring.Include(e => e.Bills).ThenInclude(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Bills.Flats.SocietyId == societyId);
            return Ok(allRecurring);
        }
    }
}
