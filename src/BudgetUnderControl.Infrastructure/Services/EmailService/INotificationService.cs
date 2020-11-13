using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Services.EmailService
{
    public interface INotificationService
    {
       Task SendRegisterNotificationAsync(Guid userId);
    }
}
