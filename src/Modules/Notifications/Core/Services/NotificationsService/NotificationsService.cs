using System.Threading.Tasks;
using BudgetUnderControl.Modules.Notifications.Core.Services.EmailService;
using BudgetUnderControl.Shared.Infrastructure.Settings;
using Core.Services.EmailService.Contracts;

namespace BudgetUnderControl.Modules.Notifications.Core.Services.NotificationsService
{
    public class NotificationsService : INotificationsService
    {
        private readonly IEmailService emailService;
        private readonly EmailModuleSettings settings;
        public NotificationsService(IEmailService emailService, EmailModuleSettings settings)
        {
            this.emailService = emailService;
            this.settings = settings;
        }

        public async Task SendRegisterNotificationAsync(string emailAddress, string firstName, string lastName, string code)
        {
            var subject = "Welcome in Budget Under Control";
           
            var link = $"{ settings.FrontEndHostBaseURL }activate?code={code}";
            var body = $"Welcome in BUC app. To Activate your account please use this code: {code} or open this link in a browser: {link}";
            var notificationArgs = new UserActivationNotificationArgs
            {
                Email = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                ActivationToken = code
            };
            var email = await emailService.CreateRegistrationEmailAsync(notificationArgs, subject, body);
            //emailService.SendEmail(email);
        }
    }
}
