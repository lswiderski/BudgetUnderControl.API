using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.GetUserIdentity
{
    internal class GetUserIdentityQueryHandler : IQueryHandler<GetUserIdentityQuery, IUserIdentityContext>
    {
        private readonly IUserIdentityContext identityContext;

        internal GetUserIdentityQueryHandler(IUserIdentityContext identityContext)
        {
            this.identityContext = identityContext;
        }

        public async Task<IUserIdentityContext> Handle(GetUserIdentityQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            return identityContext;
        }
    }
}