using BudgetUnderControl.ApiInfrastructure.Services.EmailService.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService emailService;
        private readonly GeneralSettings settings;
        public NotificationService(IEmailService emailService, GeneralSettings settings)
        {
            this.emailService = emailService;
            this.settings = settings;
        }

        public async Task SendRegisterNotificationAsync(UserDTO user, string code)
        {
            var subject = "Welcome in Budget Under Control";
           
            var link = $"{ settings.FrontEndHostBaseURL }activate?code={code}";
            var body = $"Welcome in BUC app. To Activate your account please use this code: {code} or open this link in a browser: {link}";
            var notificationArgs = new UserActivationNotificationArgs
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ActivationToken = code
            };
            var email = await emailService.CreateRegistrationEmailAsync(notificationArgs, subject, body);
            emailService.SendEmail(email);
        }
    }
}
