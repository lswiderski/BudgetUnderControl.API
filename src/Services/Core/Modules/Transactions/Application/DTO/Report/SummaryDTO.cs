using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class SummaryDTO
    {
        public decimal Value { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
