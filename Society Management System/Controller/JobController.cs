using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Society_Management_System.Model;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Services.EmailService;

namespace Society_Management_System.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {

        private readonly IEmailService _emailService;
        private readonly SocietyContext _societyContext;
       

        public JobController(IEmailService emailService,SocietyContext societyContext)
        {
            _emailService = emailService; 
            _societyContext = societyContext;

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
            if (item.Name == String.Empty || item.Type == String.Empty || item.NotifyBefore == 0
                || item.DueDate == String.Empty || item.Email == String.Empty || item.Amount == 0)
            {
                return BadRequest("Fill the Full form First");
            }
            EmailDto emailDto = new EmailDto()
            {
                Email = item.Email,
                Amount = item.Amount,
                DueDate = item.DueDate,
                Type = item.Type,
                Name = item.Name,
            };
            var DateTimeCompleted = DateTime.Parse(item.DueDate);
            var DateAndTimeNow = DateTime.Now;
            if (DateTimeCompleted < DateAndTimeNow)
            {
                return BadRequest("Previous Date Or Time Can't be Applied");
            }

            var diff = DateTimeCompleted - DateTime.Now; 
            var NotifyTime = new TimeSpan(item.NotifyBefore, 0, 0, 0); 

            if (diff < NotifyTime)
            {
                return BadRequest("Email cannot be sent as Completed time is less than Notify time");
            }


            var seheduleDateTime = DateTimeCompleted - NotifyTime;
            var dateTimeOffset = new DateTimeOffset(seheduleDateTime);
            var jobId = BackgroundJob.Schedule(() => _emailService.JobSendEmail(emailDto), dateTimeOffset);
            Bills bill = await _societyContext.Bills.Include(e => e.Flats).ThenInclude(e => e.Users).FirstOrDefaultAsync(e => e.BillsId == item.BillId);
            Alarms alarm = new Alarms()
            {
                Name = item.Name, 
                Jobid = Convert.ToInt32(jobId),
                Bills = bill
            };
            _societyContext.Alarms.Add(alarm);
            await _societyContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("DeleteJobEmail/{id}")]
        public async Task<IActionResult> DeleteJobEmail(int id)
        {
            Alarms alarm = await _societyContext.Alarms.FirstOrDefaultAsync(e => e.Id == id);
            if (alarm == null) {
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

        [HttpPost]
        [Route("CreateRecurringJob")]
        public async Task<ActionResult> CreateRecurringJobAsync([FromBody] EmailItemDto item)
        {
            EmailDto emailDto = new EmailDto()
            {
                Email = item.Email,
                Amount = item.Amount,
                DueDate = item.DueDate,
                Type = item.Type,
                Name = item.Name,
            };
            
            await _societyContext.SaveChangesAsync();
            RecurringJob.AddOrUpdate("jobEmail",() => _emailService.JobSendEmail(emailDto),
                "*/10 * * * * *");
            return Ok();
        }

        [HttpPost]
        [Route("StopRecurringJob")]
        public ActionResult StopRecurringJob([FromBody] EmailItemDto item)
        {
            RecurringJob.RemoveIfExists("SendEmailJob");
            return Ok("Recurring job stopped.");
        }
    }
}
