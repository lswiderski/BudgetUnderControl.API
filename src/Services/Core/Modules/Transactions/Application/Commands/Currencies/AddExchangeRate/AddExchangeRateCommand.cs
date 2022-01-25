using BudgetUnderControl.Shared.Application.CQRS.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Currencies.AddExchangeRate
{
    public class AddExchangeRateCommand : CommandBase<Guid>
    {
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public double Rate { get; set; }
        public DateTime Date { get; set; }
        public Guid ExternalId { get;  }

        public AddExchangeRateCommand()
        {
            this.ExternalId = Guid.NewGuid();
        }
    }
}
