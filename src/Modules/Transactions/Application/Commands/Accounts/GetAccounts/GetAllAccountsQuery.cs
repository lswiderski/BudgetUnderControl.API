using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System.Collections.Generic;

using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.GetAccounts
{
    public class GetAllAccountsQuery : QueryBase<List<AccountListItemDTO>>
    {
    }
}
