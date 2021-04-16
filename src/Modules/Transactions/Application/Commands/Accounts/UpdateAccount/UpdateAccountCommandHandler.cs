using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.UpdateAccount
{
    internal class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
    {
        private readonly IAccountService accountService;

        internal UpdateAccountCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            await this.accountService.EditAccountAsync(request);

            return Unit.Value;
        }
    }
}