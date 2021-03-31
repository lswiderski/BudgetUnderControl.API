using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.AddTransaction
{
    internal class AddTransactionCommandHandler : ICommandHandler<AddTransactionCommand, Guid>
    {
        private readonly ITransactionService transactionService;

        internal AddTransactionCommandHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<Guid> Handle(AddTransactionCommand request, CancellationToken cancellationToken)
        {

            await transactionService.AddTransactionAsync(request);

            return request.ExternalId;
        }
    }
}
