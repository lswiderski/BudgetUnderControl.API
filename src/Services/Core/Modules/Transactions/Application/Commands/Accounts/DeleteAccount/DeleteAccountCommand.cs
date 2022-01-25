using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.DeleteAccount
{
    public class DeleteAccountCommand : CommandBase
    {
        public DeleteAccountCommand(Guid accountId)
        {
            AccountId = accountId;
        }
        public Guid AccountId { get; }
    }
}