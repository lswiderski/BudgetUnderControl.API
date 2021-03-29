using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts
{
    public class SummaryDTO
    {
        public decimal Value { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
