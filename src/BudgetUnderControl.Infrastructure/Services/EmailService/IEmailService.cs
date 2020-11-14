using BudgetUnderControl.ApiInfrastructure.Services.EmailService.Contracts;
using BudgetUnderControl.Common.Contracts.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Services.EmailService
{
    public interface IEmailService
    {
        Task<EmailMessage> CreateRegistrationEmailAsync(UserActivationNotificationArgs args, string subject, string body);

        void SendEmail(EmailMessage mail);
    }
}
