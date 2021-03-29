using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts
{
    public class CurrencyStatusDTO
    {
        public string Currency { get; set; }

        public decimal Balance { get; set; }
    }
}
