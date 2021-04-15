using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransactions
{
    internal class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, List<TransactionListItemDTO>>
    {
        private readonly ITransactionService transactionService;

        public GetTransactionsQueryHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<List<TransactionListItemDTO>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await this.transactionService.GetTransactionsAsync(request.Filters);
            return transactions.ToList();
        }
    }
}
