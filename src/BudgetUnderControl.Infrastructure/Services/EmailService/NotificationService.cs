using BudgetUnderControl.ApiInfrastructure.Services.EmailService.Contracts;
using BudgetUnderControl.Common.Contracts.User;
using BudgetUnderControl.CommonInfrastructure.Settings;
using BudgetUnderControl.Domain.Repositiories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Services.EmailService
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

        public async Task SendRegisterNotificationAsync(UserDTO user)
        {
            var subject = "Welcome in Budget Under Control";
           
            var link = $"{ settings.FrontEndHostBaseURL }activate?code={user.ActivationCode}";
            var body = $"Welcome in BUC app. To Activate your account please use this code: {user.ActivationCode} or open this link in a browser: {link}";
            var notificationArgs = new UserActivationNotificationArgs
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ActivationToken = user.ActivationCode
            };
            var email = await emailService.CreateRegistrationEmailAsync(notificationArgs, subject, body);
            emailService.SendEmail(email);
        }
    }
}
