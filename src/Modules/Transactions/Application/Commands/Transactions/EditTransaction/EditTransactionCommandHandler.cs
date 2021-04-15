using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.EditTransaction
{
    internal class EditTransactionCommandHandler : ICommandHandler<EditTransactionCommand>
    {
        private readonly ITransactionService transactionService;

        internal EditTransactionCommandHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<Unit> Handle(EditTransactionCommand request, CancellationToken cancellationToken)
        {
            await transactionService.EditTransactionAsync(request);

            return Unit.Value;
        }

    }
}
