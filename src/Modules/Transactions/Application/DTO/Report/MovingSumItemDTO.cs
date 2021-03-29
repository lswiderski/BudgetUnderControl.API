using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts
{
    public class MovingSumItemDTO
    {
        public DateTime Date { get; set; }

        public decimal Value { get; set; }

        public decimal Diff { get; set; }

        public string Currency { get; set; }
    }
}
