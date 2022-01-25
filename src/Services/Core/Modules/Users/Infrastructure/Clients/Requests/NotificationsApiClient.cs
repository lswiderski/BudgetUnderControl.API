using System;
using System.Threading.Tasks;
using BudgetUnderControl.Shared.Abstractions.Modules;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Clients.Requests
{

    internal sealed class NotificationsApiClient : INotificationsApiClient
    {
        private readonly IModuleClient _client;

        public NotificationsApiClient(IModuleClient client)
        {
            _client = client;
        }

        public Task CreateActivateUserNotificationAsync(CreateActivateUserNotification command)
            => _client.SendAsync("notification/createActivateUserNotification",command);
    }
}