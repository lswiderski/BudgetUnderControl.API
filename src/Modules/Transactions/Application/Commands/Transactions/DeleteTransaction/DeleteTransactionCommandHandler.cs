﻿using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Transactions.DeleteTransaction
{
    internal class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand>
    {
        private readonly ITransactionService transactionService;

        internal DeleteTransactionCommandHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task<Unit> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            await this.transactionService.DeleteTransactionAsync(new CommonInfrastructure.Commands.DeleteTransaction { ExternalId = request.TransactionId });

            return Unit.Value;
        }
    }
}