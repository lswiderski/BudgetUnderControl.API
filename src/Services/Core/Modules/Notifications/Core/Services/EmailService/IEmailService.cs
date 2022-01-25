using System.Threading.Tasks;
using BudgetUnderControl.Modules.Notifications.Core.DTO.Email;
using Core.Services.EmailService.Contracts;

namespace BudgetUnderControl.Modules.Notifications.Core.Services.EmailService
{
    public interface IEmailService
    {
        Task<EmailMessage> CreateRegistrationEmailAsync(UserActivationNotificationArgs args, string subject, string body);

        void SendEmail(EmailMessage mail);
    }
}
