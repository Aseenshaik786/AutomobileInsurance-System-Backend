// Services/EmailService.cs
using AutomobileInsuranceSystem.Interfaces;
using System.Net;
using System.Net.Mail;

namespace AutomobileInsuranceSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config) { _config = config; }

        public async Task SendEmailAsync(string to, string subject, string body, byte[]? attachment = null, string? attachmentName = null)
        {
            // Simple SMTP send - configure appsettings with SMTP details
            var smtpSection = _config.GetSection("Smtp");
            var host = smtpSection["Host"];
            var port = int.Parse(smtpSection["Port"] ?? "587");
            var user = smtpSection["User"];
            var pass = smtpSection["Pass"];
            var from = smtpSection["From"] ?? user;

            using var client = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = true
            };

            var message = new MailMessage(from, to, subject, body);
            if (attachment != null && attachmentName != null)
            {
                var ms = new MemoryStream(attachment);
                message.Attachments.Add(new Attachment(ms, attachmentName));
            }

            await client.SendMailAsync(message);
        }
    }
}
