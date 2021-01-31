using BudgetUnderControl.ApiInfrastructure.Services.EmailService.Contracts;
using BudgetUnderControl.Common.Contracts.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Services.EmailService
{
    public interface INotificationService
    {
       Task SendRegisterNotificationAsync(UserDTO user,string code);
    }
}
