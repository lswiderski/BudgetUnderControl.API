using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransactions
{
    public class GetTransactionsQuery : QueryBase<List<TransactionListItemDTO>>
    {
        public GetTransactionsQuery(TransactionsFilterDTO filters)
        {
            Filters = filters;
        }

        public TransactionsFilterDTO Filters { get;}
    }
}
