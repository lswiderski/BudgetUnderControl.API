using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.CreateAccount
{
    public class CreateAccountCommand : CommandBase<Guid>
    {
        public CreateAccountCommand()
        {
            this.ExternalId = Guid.NewGuid();
        }

        public int CurrencyId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncludedInTotal { get; set; }
        public string Comment { get; set; }
        public AccountType Type { get; set; }
        public int? ParentAccountId { get; set; }
        public int Order { get; set; }
        public Guid ExternalId { get; }

    }
}