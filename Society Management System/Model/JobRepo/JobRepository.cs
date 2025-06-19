using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Services.EmailService;

namespace Society_Management_System.Model.JobRepo
{
    public class JobRepository : IJobRepository
    {
        private readonly SocietyContext _societyContext;
        private readonly IEmailService _emailService;
        public JobRepository(SocietyContext societyContext, IEmailService emailService)
        {
            _societyContext = societyContext;
            _emailService = emailService;
        }
        public async Task<ActionResult> CreateScheduleJob(EmailItemDto item)
        {
            if (item.Name == String.Empty || item.Type == String.Empty || item.NotifyBefore == 0
                 || item.DueDate == String.Empty || item.Email == String.Empty || item.Amount == 0)
            {
                return new BadRequestObjectResult("Fill the Full form First");
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
                return new BadRequestObjectResult("Previous Date Or Time Can't be Applied");
            }

            var diff = DateTimeCompleted - DateTime.Now;
            var NotifyTime = new TimeSpan(item.NotifyBefore, 0, 0, 0);

            if (diff < NotifyTime)
            {
                return new BadRequestObjectResult("Email cannot be sent as Completed time is less than Notify time");
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
            return new OkObjectResult("Ok");
        }
    }
}
