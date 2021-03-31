using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.CreateAccount
{
    internal class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, Guid>
    {
        private readonly IAccountService accountService;

        internal CreateAccountCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            await this.accountService.AddAccountAsync(request);

            return request.ExternalId;
        }
    }
}