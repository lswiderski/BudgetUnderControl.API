using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Currencies.GetAllCurrencies
{
    internal class GetAllCurrenciesQueryHandler : IQueryHandler<GetAllCurrenciesQuery, List<CurrencyDTO>>
    {
        private readonly ICurrencyService currencyService;

        public GetAllCurrenciesQueryHandler(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }
        public async Task<List<CurrencyDTO>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currency = await this.currencyService.GetCurriencesAsync();
            return currency.ToList();
        }
    }
}