using Microsoft.AspNetCore.Mvc;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Services.EmailService;

namespace Society_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpPost("SendEmail")]
        public IActionResult SendEmail([FromBody] EmailDto request)
        {
            _emailService.SendEmail(request);

            return Ok();
        }
        [HttpPost("JobSendEmail")]
        public IActionResult JobSendEmail([FromBody] EmailDto request)
        {
            _emailService.JobSendEmail(request);

            return Ok();
        }
    }
}
