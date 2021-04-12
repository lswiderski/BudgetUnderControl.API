using BudgetUnderControl.ApiInfrastructure.Services.EmailService.Contracts;
using BudgetUnderControl.Common.Contracts.Email;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBuilder emailBuilder;
       
        private readonly EmailModuleSettings settings;
        public EmailService(IEmailBuilder emailBuilder, EmailModuleSettings settings)
        {
            this.emailBuilder = emailBuilder;
            this.settings = settings;
        }


        public void SendEmail(EmailMessage mail)
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.Port = settings.SmtpClient.Port;
                client.Host = settings.SmtpClient.Host;
                client.DeliveryMethod = settings.SmtpClient.DeliveryMethod;
                client.Credentials = new System.Net.NetworkCredential(settings.SmtpClient.User, settings.SmtpClient.Password);

                var recipients = new MailAddressCollection();

                mail.Recipients.ForEach(x => { recipients.Add(new MailAddress(x.Address, x.DisplayName)); });

                foreach (var to in recipients)
                {
                    var mailMessage = new MailMessage(new MailAddress(mail.Sender.Address, mail.Sender.DisplayName), to);

                    mailMessage.Body = mail.Body;
                    mailMessage.IsBodyHtml = mail.IsBodyHtml;
                    mailMessage.Subject = mail.Subject;

                    client.Send(mailMessage);
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
            var content = this.emailBuilder
                .SentBy(new EmailParticipant { Address = settings.SmtpClient.SenderAddress, DisplayName = settings.SmtpClient.SenderName })
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
