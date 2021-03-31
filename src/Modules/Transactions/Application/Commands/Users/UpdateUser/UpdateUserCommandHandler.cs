using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.UpdateUser
{
    internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly IUserService userService;

        internal UpdateUserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            await this.userService.EditUserAsync(request);

            return Unit.Value;
        }
    }
}