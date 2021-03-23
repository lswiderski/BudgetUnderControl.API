using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.UpdateAccount
{
    internal class UpdateAccountCommandHandler : ICommandHandler<UpdateAccountCommand>
    {
        private readonly IAccountService accountService;

        internal UpdateAccountCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<Unit> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            await this.accountService.EditAccountAsync(new CommonInfrastructure.Commands.EditAccount
            {
                AccountGroupId = request.AccountGroupId,
                Amount = request.Amount,
                Comment = request.Comment,
                CurrencyId = request.CurrencyId,
                ExternalId = request.ExternalId,
                IsIncludedInTotal = request.IsIncludedInTotal,
                Name = request.Name,
                Order = request.Order,
                ParentAccountId = request.ParentAccountId,
                Type = request.Type,
                Currency = request.Currency,
                CurrencySymbol = request.CurrencySymbol,
                Id = request.Id,
                IsActive = request.IsActive,
            });

            return Unit.Value;
        }
    }
}