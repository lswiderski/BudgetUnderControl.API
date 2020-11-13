using BudgetUnderControl.Common.Contracts.Email;
using BudgetUnderControl.CommonInfrastructure.Interfaces.Email;
using BudgetUnderControl.CommonInfrastructure.Settings;
using BudgetUnderControl.Domain.Repositiories;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBuilder emailBuilder;
        private readonly IUserRepository userRepository;
        private readonly GeneralSettings settings;
        public EmailService(IEmailBuilder emailBuilder, IUserRepository userRepository, GeneralSettings settings)
        {
            this.emailBuilder = emailBuilder;
            this.userRepository = userRepository;
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

        public async Task<EmailMessage> CreateRegistrationEmailAsync(Guid userId, string subject, string body)
        {
            var user = await this.userRepository.GetAsync(userId);

            var participant = new EmailParticipant { Address = user.Email, DisplayName = $"{user.FirstName} {user.LastName}" };

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
