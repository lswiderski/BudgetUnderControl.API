using System;
using System.Net.Mail;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Notifications.Core.DTO.Email;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using MoreLinq;
using Core.Services.EmailService.Contracts;
using Microsoft.Extensions.Logging;

namespace BudgetUnderControl.Modules.Notifications.Core.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBuilder _emailBuilder;
        private readonly ILogger<EmailService> _logger;
       
        private readonly EmailModuleSettings _settings;
        public EmailService(IEmailBuilder emailBuilder, EmailModuleSettings settings, ILogger<EmailService> logger)
        {
            this._emailBuilder = emailBuilder;
            this._settings = settings;
            _logger = logger;
        }


        public void SendEmail(EmailMessage mail)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Port = _settings.SmtpClient.Port;
                client.Host = _settings.SmtpClient.Host;
                client.DeliveryMethod = _settings.SmtpClient.DeliveryMethod;
                client.Credentials = new System.Net.NetworkCredential(_settings.SmtpClient.User, _settings.SmtpClient.Password);

                var recipients = new MailAddressCollection();

                mail.Recipients.ForEach(x => { recipients.Add(new MailAddress(x.Address, x.DisplayName)); });

                foreach (var to in recipients)
                {
                    var mailMessage = new MailMessage(new MailAddress(mail.Sender.Address, mail.Sender.DisplayName), to);

                    mailMessage.Body = mail.Body;
                    mailMessage.IsBodyHtml = mail.IsBodyHtml;
                    mailMessage.Subject = mail.Subject;

                    try
                    {
                        client.Send(mailMessage);
                    }
                    catch (Exception e)
                    {
                        _logger.Log(LogLevel.Error,e, "smtp client error");
                    }
                   
                }
            }
        }

        public async Task<EmailMessage> CreateRegistrationEmailAsync(UserActivationNotificationArgs args, string subject, string body)
        {
            var participant = new EmailParticipant { Address = args.Email, DisplayName = $"{args.FirstName} {args.LastName}" };

            return await this.CreateRegistrationEmailAsync(participant, subject, body, false);
        }

        private async Task<EmailMessage> CreateRegistrationEmailAsync(EmailParticipant emailParticipant, string subject, string body, bool isHtml)
        {
            var content = this._emailBuilder
                .SentBy(new EmailParticipant { Address = _settings.SmtpClient.SenderAddress, DisplayName = _settings.SmtpClient.SenderName })
                .To(emailParticipant)
                .Entitled(subject);

            if (isHtml)
            {
                return content.WithHtmlContent(body);
            }
            else
            {
                return content.WithPlainTextContent(body);
            }

        }
    }
}
