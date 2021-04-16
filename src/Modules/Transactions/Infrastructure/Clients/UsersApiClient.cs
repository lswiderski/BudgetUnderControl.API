using BudgetUnderControl.Modules.Transactions.Application.Clients.Users;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Clients.Requests;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using BudgetUnderControl.Shared.Abstractions.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Clients
{
    internal sealed class UsersApiClient : IUsersApiClient
    {
        private readonly IModuleClient _client;

        public UsersApiClient(IModuleClient client)
        {
            _client = client;
        }

        public Task<UserIdentityContext> GetCurrentUserContextAsync()
         => _client.SendAsync<UserIdentityContext>("users/get",
             new GetUserContext
             {
             });
    }
}
