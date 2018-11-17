﻿using BudgetUnderControl.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure.Commands.Transaction
{
    public class DeleteTransactionHandler : ICommandHandler<DeleteTransaction>
    {
        private readonly ITransactionService transactionService;
        public DeleteTransactionHandler(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        public async Task HandleAsync(DeleteTransaction command)
        {
            await this.transactionService.DeleteTransactionAsync(command);
        }
    }
}
