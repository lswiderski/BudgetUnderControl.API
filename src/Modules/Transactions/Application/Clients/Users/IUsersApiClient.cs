using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Clients.Users
{
    public interface IUsersApiClient
    {
        Task<UserIdentityContext> GetCurrentUserContextAsync();
    }
}
