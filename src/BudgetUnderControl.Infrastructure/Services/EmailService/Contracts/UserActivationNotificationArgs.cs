using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.ApiInfrastructure.Services.EmailService.Contracts
{
    public class UserActivationNotificationArgs : INotificationArgs
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ActivationToken { get; set; }
    }
}
