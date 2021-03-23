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
            var commandDTO = new CommonInfrastructure.Commands.EditTransaction
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
                ExtendedType = request.ExtendedType,
                ExternalId = request.ExternalId,
                Id = request.Id,
                IsDeleted = request.IsDeleted,
                TransferId = request.TransferId,
                TransferTransactionId = request.TransferTransactionId
            };
            await transactionService.EditTransactionAsync(commandDTO);

            return Unit.Value;
        }

    }
}
