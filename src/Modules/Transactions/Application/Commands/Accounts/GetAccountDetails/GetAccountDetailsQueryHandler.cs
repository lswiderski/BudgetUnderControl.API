using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccountDetails
{
    internal class GetAccountDetailsQueryHandler : IQueryHandler<GetAccountDetailsQuery, AccountDetailsDTO>
    {
        private readonly IAccountService accountService;

        public GetAccountDetailsQueryHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<AccountDetailsDTO> Handle(GetAccountDetailsQuery request, CancellationToken cancellationToken)
        {
            var account = await this.accountService.GetAccountDetailsAsync(new TransactionsFilterDTO { AccountsExternalIds = new List<Guid> { request.AccountId } });
            return account;
        }
    }
}
