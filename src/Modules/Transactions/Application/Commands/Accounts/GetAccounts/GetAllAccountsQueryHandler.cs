using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccounts
{
    internal class GetAllAccountsQueryHandler : IQueryHandler<GetAllAccountsQuery, List<AccountListItemDTO>>
    {
        private readonly IAccountService accountService;

        public GetAllAccountsQueryHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<List<AccountListItemDTO>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
        {
            var accounts = await this.accountService.GetAccountsWithBalanceAsync();
            return accounts.ToList();
        }
    }
}