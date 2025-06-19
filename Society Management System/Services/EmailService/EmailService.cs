using Society_Management_System.Model;
using Society_Management_System.Model.Dto_s; 
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
namespace Society_Management_System.Services.EmailService
{
    public class EmailService : IEmailService
    {

        private readonly IConfiguration _config;
        private readonly SocietyContext _societyContext;

        public EmailService(IConfiguration config, SocietyContext todoContext)
        {
            _config = config;
            _societyContext = todoContext;
        }
        static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            char[] code = new char[length];

            for (int i = 0; i < length; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }

            return new string(code);
        }
        public void SendEmail(EmailDto request)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.ReplyTo.Add(MailboxAddress.Parse(_config["EmailUserName"]));

            email.Subject = "Verify your email address";
            //string otp = _todoContext.SemiRegisters.Where(e => e.Email == request.Email).Select(e => e.Otp).FirstOrDefault();
            email.Body = new TextPart(TextFormat.Html) { Text = $@"<!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Event Registration Code</title>
                    <style>
                        body {{
                              font-family: Arial, sans-serif;
                            background-color: #121212;
                            margin: 0;
                            padding: 0;
                            text-align: center;
                            color: #e0e0e0;
                        }}

                        .container {{
                           max-width: 600px;
                            margin: 30px auto;
                            background-color: #1e1e1e;
                            padding: 20px;
                            border-radius: 8px;
                            box-shadow: 0 0 10px rgba(255, 255, 255, 0.1);
                            color: #ffffff;
                            text-align: center;
                        }}

                        h1 {{
                            color: #fff;
                        }}

                        p {{
                           color: #c0c0c0;
                        }}

                        .code {{
                            display: inline-block;
                            font-size: 24px;
                            font-weight: bold;
                            color: #ffffff;
                            padding: 10px;
                            background-color: #333333;
                            border-radius: 4px;
                            margin-top: 20px;
                        }}

                        .note {{
                              color: #c0c0c0;
                              margin-top: 20px;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <h1> Verification Code</h1>
                        <p>Thank you for registering for TaskMate. Your 6 digit verification code is:</p>
                        <div class=""code""></div>
                        <p class=""note"">Please use this code to complete your registration process.</p>
                    </div>
                </body>
                </html>" };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }

        public void JobSendEmail(EmailDto request)
        {

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.ReplyTo.Add(MailboxAddress.Parse(_config["EmailUserName"]));

            email.Subject = "New bill has been added";
            
            email.Body = new TextPart(TextFormat.Html) { Text = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Bill Notification</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f6f8;
            margin: 0;
            padding: 0;
            text-align: center;
            color: #333333;
        }}

        .container {{
            max-width: 600px;
            margin: 40px auto;
            background-color: #ffffff;
            padding: 30px 25px;
            border-radius: 10px;
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.08);
            text-align: center;
        }}

        h1 {{
            color: #2c3e50;
            font-size: 28px;
            margin-bottom: 20px;
        }}

        p {{
            color: #555555;
            font-size: 16px;
            line-height: 1.6;
        }}

        .code {{
            display: inline-block;
            font-size: 18px;
            font-weight: 500;
            color: #1a1a1a;
            padding: 18px;
            background-color: #f0f4f8;
            border-left: 5px solid #3498db;
            border-radius: 6px;
            margin-top: 25px;
            text-align: left;
            box-shadow: inset 0 0 3px rgba(0, 0, 0, 0.05);
        }}

        .note {{
            color: #666666;
            margin-top: 25px;
            font-size: 15px;
            font-style: italic;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Hello  {request.Name},</h1>
        <p>Your new bill has been added. Please find the details below and make the payment before the due date.</p>
        <div class=""code"">
            Bill Type: {request.Type}<br>
            Amount Due: ₹{request.Amount}<br>
            Due Date: {request.DueDate}
        </div> 
        <p class=""note"">Please ensure timely payment to avoid any late charges.</p>
    </div>
</body>
</html>
 " };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
            try
            {
                smtp.Send(email);
            }
            catch (Exception e)
            {
                Console.WriteLine("Execption : ", e);
            }
            smtp.Disconnect(true);

        }
    }
}
