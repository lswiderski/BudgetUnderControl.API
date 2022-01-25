using System.Collections.Generic;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Transactions.GetTransactionsToExport
{
    public class GetTransactionsToExportQuery : QueryBase<ICollection<TransactionExportItemDto>>
    {
        public GetTransactionsToExportQuery(TransactionsFilterDTO filters)
        {
            Filters = filters;
        }

        public TransactionsFilterDTO Filters { get; }
    }
}