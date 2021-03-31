
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.ResetUserActivation
{
    internal class ResetUserActivationCommandHandler : ICommandHandler<ResetUserActivationCommand>
    {
        private readonly IUserService userService;
        private readonly IUserIdentityContext identityContext;

        internal ResetUserActivationCommandHandler(IUserService userService, IUserIdentityContext identityContext)
        {
            this.userService = userService;
            this.identityContext = identityContext;
        }

        public async Task<Unit> Handle(ResetUserActivationCommand request, CancellationToken cancellationToken)
        {
            await userService.ResetActivationCodeAsync(request.UserId ?? identityContext.ExternalId);

            return Unit.Value;
        }
    }
}