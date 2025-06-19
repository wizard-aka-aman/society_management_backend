using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.JobRepo
{
    public interface IJobRepository
    {
        Task<ActionResult> CreateScheduleJob(EmailItemDto item);
    }
}
