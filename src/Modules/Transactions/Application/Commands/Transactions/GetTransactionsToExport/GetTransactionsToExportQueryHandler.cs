using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransactions;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Transactions.GetTransactionsToExport
{
    internal class GetTransactionsToExportQueryHandler: IQueryHandler<GetTransactionsToExportQuery, ICollection<TransactionExportItemDto>>
    {
        private readonly ITransactionService transactionService;

        public GetTransactionsToExportQueryHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<ICollection<TransactionExportItemDto>> Handle(GetTransactionsToExportQuery request,
            CancellationToken cancellationToken)
        {
            var transactions = await this.transactionService.GetTransactionsToExportAsync(request.Filters);
            
            return transactions;
        }
    }
}