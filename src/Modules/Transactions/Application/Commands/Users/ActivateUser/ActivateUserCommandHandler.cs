using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.ActivateUser
{
    internal class ActivateUserCommandHandler : ICommandHandler<ActivateUserCommand, bool>
    {
        private readonly IUserService userService;
        private readonly IUserIdentityContext identityContext;

        internal ActivateUserCommandHandler(IUserService userService, IUserIdentityContext identityContext)
        {
            this.userService = userService;
            this.identityContext = identityContext;
        }

        public async Task<bool> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await this.userService.ActivateUserAsync(identityContext.ExternalId, request.Code);

            return isSuccess;
        }
    }
}
