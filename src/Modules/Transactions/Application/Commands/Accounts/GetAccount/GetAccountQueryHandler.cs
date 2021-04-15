using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccount
{
    internal class GetAccountQueryHandler : IQueryHandler<GetAccountQuery, EditAccountDTO>
    {
        private readonly IAccountService accountService;

        public GetAccountQueryHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<EditAccountDTO> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await this.accountService.GetAccountAsync(request.AccountId);
            return account;
        }
    }
}
