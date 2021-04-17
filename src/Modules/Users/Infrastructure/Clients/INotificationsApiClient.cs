using System;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Users.Infrastructure.Clients.Requests;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Clients
{
    public interface INotificationsApiClient
    {
        Task CreateActivateUserNotificationAsync(CreateActivateUserNotification command);
    }
}