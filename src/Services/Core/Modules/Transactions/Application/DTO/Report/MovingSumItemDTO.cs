using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class MovingSumItemDTO
    {
        public DateTime Date { get; set; }

        public decimal Value { get; set; }

        public decimal Diff { get; set; }

        public string Currency { get; set; }
    }
}
