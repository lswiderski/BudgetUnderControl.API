using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Services.EmailService
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService emailService;
        public NotificationService(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public async Task SendRegisterNotificationAsync(Guid userId)
        {
            var body = "Welcome in BUC app.";
            var subject = "Welcome in Budget Under Control";
            var email = await emailService.CreateRegistrationEmailAsync(userId, subject, body);
            emailService.SendEmail(email);
        }
    }
}
