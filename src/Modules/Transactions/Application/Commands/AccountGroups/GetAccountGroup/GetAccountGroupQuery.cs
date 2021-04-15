using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.AccountGroups.GetAccountGroup
{
    public class GetAccountGroupQuery : QueryBase<AccountGroupItemDTO>
    {
        public GetAccountGroupQuery(Guid accountGroupId)
        {
            AccountGroupId = accountGroupId;
        }

        public Guid AccountGroupId { get; }
    }
}