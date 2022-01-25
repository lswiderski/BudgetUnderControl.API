using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccountDetails
{
    public class GetAccountDetailsQuery : QueryBase<AccountDetailsDTO>
    {
        public GetAccountDetailsQuery(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; }

    }
}
