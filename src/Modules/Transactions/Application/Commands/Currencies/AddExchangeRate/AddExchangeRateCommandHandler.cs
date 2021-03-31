using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Currencies.AddExchangeRate
{
    internal class AddExchangeRateCommandHandler : ICommandHandler<AddExchangeRateCommand, Guid>
    {
        private readonly ICurrencyService currencyService;

        internal AddExchangeRateCommandHandler(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        public async Task<Guid> Handle(AddExchangeRateCommand request, CancellationToken cancellationToken)
        {
            await this.currencyService.AddExchangeRateAsync(request);

            return request.ExternalId;
        }
    }
}