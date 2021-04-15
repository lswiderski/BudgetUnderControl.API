using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Users.GetUserIdentity
{
    public class GetUserIdentityQuery : QueryBase<IUserIdentityContext>
    {
    }
}
