using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts
{
    public class ExchangeRateDTO
    {
        public int Id { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }

        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }

        public bool CanDelete { get; set; }
    }
}
