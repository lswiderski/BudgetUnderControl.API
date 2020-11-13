using BudgetUnderControl.Common.Contracts.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Services.EmailService
{
    public interface IEmailService
    {
        Task<EmailMessage> CreateRegistrationEmailAsync(Guid userId, string subject, string body);

        void SendEmail(EmailMessage mail);
    }
}
