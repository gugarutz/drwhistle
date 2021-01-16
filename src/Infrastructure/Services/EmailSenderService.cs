using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using DrWhistle.Application.Common.Interfaces;
using DrWhistle.Application.Common.Models;
using Microsoft.Extensions.Options;

namespace DrWhistle.Infrastructure.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly EmailSettings emailSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSenderService"/> class.
        /// </summary>
        /// <param name="emailOptions">The email options.</param>
        public EmailSenderService(IOptionsSnapshot<EmailSettings> emailOptions)
        {
            emailSettings = emailOptions?.Value;
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>Task.</returns>
        public async Task SendAsync(MailMessage message)
        {
            await this.SendAsync(message, TimeSpan.FromSeconds(30));
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>Task.</returns>
        public async Task SendAsync(MailMessage message, TimeSpan timeout)
        {
            switch (emailSettings.DeliveryMethod)
            {
                case EmailDeliveryMethod.Network:
                case EmailDeliveryMethod.SpecifiedPickupDirectory:
                    await SendSmtpAsync(message, timeout);
                    break;

                default:
                    throw new NotSupportedException($"Email Delivery method '{emailSettings?.DeliveryMethod}' is not supported.");
            }
        }

        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>
        /// Task.
        /// </returns>
        protected async Task SendSmtpAsync(MailMessage message, TimeSpan timeout)
        {
            SmtpClient smtp = null;
            try
            {
                smtp = CreateSmtpClient();

                using var token = new CancellationTokenSource(timeout);
                token.Token.Register(smtp.SendAsyncCancel);

                await smtp.SendMailAsync(message, token.Token);
            }
            finally
            {
                if (smtp != null)
                {
                    ReleaseSmtpClient(smtp);
                }
            }
        }

        protected SmtpClient CreateSmtpClient()
        {
            switch (emailSettings.DeliveryMethod)
            {
                case EmailDeliveryMethod.Network:
                    return CreateNetworkSmtpClient();

                case EmailDeliveryMethod.SpecifiedPickupDirectory:
                    return CreateLocalFolderSmtpClient();

                default:
                    throw new NotSupportedException($"SMTP Delivery method '{emailSettings?.DeliveryMethod}' is not supported.");
            }
        }

        protected SmtpClient CreateNetworkSmtpClient()
        {
            string host = emailSettings?.Host;

            int port = emailSettings?.Port ?? 0;

            bool enableSsl = emailSettings?.EnableSsl ?? false;

            string username = emailSettings?.Username ?? string.Empty;

            bool useDefaultCredentials = emailSettings?.UseDefaultCredentials ?? false;

            string password = emailSettings?.Password ?? string.Empty;

            SecureString passSecureString = new SecureString();

            if (!string.IsNullOrEmpty(password))
            {
                Array.ForEach(password.ToCharArray(), c => passSecureString.AppendChar(c));
            }

            var client = new SmtpClient(host, port)
            {
                UseDefaultCredentials = useDefaultCredentials,
                EnableSsl = enableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(username, passSecureString)
            };

            return client;
        }

        protected SmtpClient CreateLocalFolderSmtpClient()
        {
            var pickupFolder = Environment.ExpandEnvironmentVariables(emailSettings?.PickupDirectoryLocation?.Trim());

            pickupFolder = Path.GetFullPath(pickupFolder);

            if (!Directory.Exists(pickupFolder))
            {
                Directory.CreateDirectory(pickupFolder);
            }

            var client = new SmtpClient()
            {
                EnableSsl = emailSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                UseDefaultCredentials = emailSettings.UseDefaultCredentials,
                PickupDirectoryLocation = pickupFolder
            };

            return client;
        }

        protected virtual void ReleaseSmtpClient(SmtpClient smtp)
        {
            if (smtp != null)
            {
                smtp.Dispose();
            }
        }
    }
}