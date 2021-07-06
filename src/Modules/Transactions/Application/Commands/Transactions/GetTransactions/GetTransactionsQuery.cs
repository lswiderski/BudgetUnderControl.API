using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransactions
{
    public class GetTransactionsQuery : QueryBase<TransactionListDataSource>
    {
        public GetTransactionsQuery(TransactionsFilterDTO filters)
        {
            Filters = filters;
        }

        public TransactionsFilterDTO Filters { get;}
    }
}
