using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
        void JobSendEmail(EmailDto request);
    }
}
