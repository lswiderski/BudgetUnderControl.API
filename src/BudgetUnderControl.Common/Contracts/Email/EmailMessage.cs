using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts.Email
{
    public class EmailMessage
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public EmailParticipant Sender { get; set; }

        public ICollection<EmailParticipant> Recipients { get; set; }

        public ICollection<EmailParticipant> CC { get; set; }

        public bool IsBodyHtml { get; set; }

        public EmailMessage()
        {
            this.Recipients = new List<EmailParticipant>();
            this.CC = new List<EmailParticipant>();
        }
    }
}
