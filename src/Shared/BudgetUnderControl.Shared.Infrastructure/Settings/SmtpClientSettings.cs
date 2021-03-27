using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BudgetUnderControl.Shared.Infrastructure.Settings
{
    public class SmtpClientSettings
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
    }
}
