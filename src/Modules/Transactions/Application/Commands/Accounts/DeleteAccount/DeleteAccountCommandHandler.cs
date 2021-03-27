using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.DeleteAccount
{
    internal class DeleteAccountCommandHandler : ICommandHandler<DeleteAccountCommand>
    {
        private readonly IAccountService accountService;

        internal DeleteAccountCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<Unit> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await this.accountService.DeleteAccountAsync(request.AccountId);

            return Unit.Value;
        }
    }
}
