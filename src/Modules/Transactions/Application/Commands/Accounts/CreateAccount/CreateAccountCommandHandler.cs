using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.CreateAccount
{
    internal class CreateAccountCommandHandler : ICommandHandler<CreateAccountCommand, Guid>
    {
        private readonly IAccountService accountService;

        internal CreateAccountCommandHandler(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public async Task<Guid> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var commandDTO = new CommonInfrastructure.Commands.AddAccount
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
                Type = request.Type
            };
            await this.accountService.AddAccountAsync(commandDTO);

            return commandDTO.ExternalId;
        }
    }
}