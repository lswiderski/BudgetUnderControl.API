using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccount
{
    public class GetAccountQuery : QueryBase<EditAccountDTO>
    {
        public GetAccountQuery(Guid accountId)
        {
            AccountId = accountId;
        }

        public Guid AccountId { get; }
    }
}
