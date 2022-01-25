using BudgetUnderControl.Modules.Notifications.Core.DTO.Email;

namespace BudgetUnderControl.Modules.Notifications.Core.Services.EmailService
{
    public class EmailBuilder : IEmailBuilder
    {
        private EmailMessage message;

        public EmailBuilder()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.message = new EmailMessage();
        }

        ITo IFrom.SentBy(EmailParticipant from)
        {
            message.Sender = from;
            return this;
        }

        IAfterTo ITo.To(EmailParticipant to)
        {
            message.Recipients.Add(to);
            return this;
        }

        IContent ITitle.Entitled(string subject)
        {
            message.Subject = subject;
            return this;
        }

        IAfterCc ICc.WithCopySentTo(EmailParticipant cc)
        {
            message.CC.Add(cc);
            return this;
        }

        IAfterTo IAfterTo.And(EmailParticipant to)
        {
            message.Recipients.Add(to);
            return this;
        }

        IAfterCc IAfterCc.And(EmailParticipant cc)
        {
            message.CC.Add(cc);
            return this;
        }

        EmailMessage IContent.WithHtmlContent(string body)
        {
            message.IsBodyHtml = true;
            message.Body = body;
            return message;
        }

        EmailMessage IContent.WithPlainTextContent(string body)
        {
            message.Body = body;
            return message;
        }

    }
}
