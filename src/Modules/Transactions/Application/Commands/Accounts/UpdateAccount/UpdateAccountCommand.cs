using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Accounts.UpdateAccount
{
    public class UpdateAccountCommand : CommandBase
    {
        public UpdateAccountCommand()
        {

        }
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public int AccountGroupId { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncludedInTotal { get; set; }
        public bool IsActive { get; set; }
        public string Comment { get; set; }
        public Guid ExternalId { get; set; }

        public AccountType Type { get; set; }
        public int? ParentAccountId { get; set; }
        public int Order { get; set; }
    }
}
