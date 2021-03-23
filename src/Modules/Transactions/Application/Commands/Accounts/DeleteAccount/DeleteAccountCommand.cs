using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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