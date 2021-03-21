using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.GetTransaction
{
    internal class GetTransactionQueryHandler : IQueryHandler<GetTransactionQuery, EditTransactionDTO>
    {
        private readonly ITransactionService transactionService;

        public GetTransactionQueryHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }
        public async Task<EditTransactionDTO> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
        {
            var transaction = await this.transactionService.GetTransactionAsync(request.TransactionId);
            return transaction;
        }
    }
}
