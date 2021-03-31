using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Currencies.GetCurrency
{
    internal class GetCurrencyQueryHandler : IQueryHandler<GetCurrencyQuery, CurrencyDTO>
    {
        private readonly ICurrencyService currencyService;

        public GetCurrencyQueryHandler(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }
        public async Task<CurrencyDTO> Handle(GetCurrencyQuery request, CancellationToken cancellationToken)
        {
            var currency = await this.currencyService.GetCurrencyAsync(request.CurencyId);
            return currency;
        }
    }
}