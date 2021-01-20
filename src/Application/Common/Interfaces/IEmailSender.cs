using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace DrWhistle.Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(MailMessage message);

        Task SendAsync(MailMessage message, TimeSpan timeout);
    }
}