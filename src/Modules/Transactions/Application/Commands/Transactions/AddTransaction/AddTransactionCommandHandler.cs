using BudgetUnderControl.CommonInfrastructure;
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
            var commandDTO = new CommonInfrastructure.Commands.AddTransaction
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                CategoryId = request.CategoryId,
                Comment = request.Comment,
                Date = request.Date,
                FileGuid = request.FileGuid,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Name = request.Name,
                Rate = request.Rate,
                Tags = request.Tags,
                TransferAccountId = request.TransferAccountId,
                TransferAmount = request.TransferAmount,
                TransferDate = request.TransferDate,
                Type = request.Type
            };
            await transactionService.AddTransactionAsync(commandDTO);

            return commandDTO.ExternalId;
        }
    }
}
