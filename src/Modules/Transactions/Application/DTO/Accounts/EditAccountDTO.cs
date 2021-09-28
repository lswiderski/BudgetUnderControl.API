using BudgetUnderControl.Common.Enums;
using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class EditAccountDTO
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public int CurrencyId { get; set; }
        public string Currency { get; set; }
        public string CurrencySymbol { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncludedInTotal { get; set; }
        public bool IsActive { get; set; }
        public string Comment { get; set; }

        public AccountType Type { get; set; }
        public int? ParentAccountId { get; set; }
        public int Order { get; set; }

        public string AmountWithCurrency
        {
            get
            {
                return string.Format("{0}{1}", Amount, CurrencySymbol);
            }
        }
    }
}
