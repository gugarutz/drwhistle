using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DrWhistle.Application.Common.Models;
using Microsoft.Extensions.Options;
using InternalEmailSender = DrWhistle.Application.Common.Interfaces.IEmailSender;

namespace DrWhistle.WebUI.Services
{
    public class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        private readonly InternalEmailSender inernalSender;
        private readonly EmailSettings emailSettings;

        public EmailSender(InternalEmailSender inernalSender, IOptionsSnapshot<EmailSettings> emailSettings)
        {
            this.inernalSender = inernalSender;
            this.emailSettings = emailSettings?.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MailMessage();

            message.To.Add(email);
            message.Subject = subject;
            message.From = new MailAddress(emailSettings?.FromAddress);
            message.SubjectEncoding = Encoding.UTF8;

            message.IsBodyHtml = true;

            message.Body = htmlMessage;

            await inernalSender.SendAsync(message);
        }
    }
}