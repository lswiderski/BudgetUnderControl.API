using BudgetUnderControl.ApiInfrastructure.Services.EmailService.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public interface INotificationService
    {
       Task SendRegisterNotificationAsync(UserDTO user,string code);
    }
}
