using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Transaction;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransactions
{
    internal class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, TransactionListDataSource>
    {
        private readonly ITransactionService transactionService;
        private readonly IBalanceService balanceService;

        public GetTransactionsQueryHandler(ITransactionService transactionService, IBalanceService balanceService)
        {
            this.transactionService = transactionService;
            this.balanceService = balanceService;
        }

        public async Task<TransactionListDataSource> Handle(GetTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            var transactions = await this.transactionService.GetTransactionsAsync(request.Filters);
            var balance = await this.balanceService.GetBalanceAsync(transactions);
            var result = new TransactionListDataSource
            {
                Transactions = transactions,
                NumberOfTransactions = request.Filters.AccountsIds != null && request.Filters.AccountsIds.Any()
                    ? transactions.Count()
                    : transactions.Where(x => x.IsTransfer == false).Count(),
                Balance = balance
            };
            return result;
        }
    }
}