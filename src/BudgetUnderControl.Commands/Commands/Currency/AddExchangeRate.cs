using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.CommonInfrastructure.Commands
{
    public class AddExchangeRate
    {
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
        public Guid ExternalId { get; set; }

        public AddExchangeRate()
        {
            this.ExternalId = Guid.NewGuid();
        }
    }
}
