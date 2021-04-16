using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Currencies.AddExchangeRate;
using BudgetUnderControl.Shared.Abstractions.Contexts;
using static MoreLinq.Extensions.MinByExtension;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository currencyRepository;
        private readonly IContext context;

        public CurrencyService(ICurrencyRepository currencyRepository, IContext context)
        {
            this.currencyRepository = currencyRepository;
            this.context = context;
        }

        public async Task<ICollection<CurrencyDTO>> GetCurriencesAsync()
        {
            var currencies = await this.currencyRepository.GetCurriencesAsync();       
            var dtos = currencies.Select(x => new CurrencyDTO
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.FullName,
                Number = x.Number,
                Symbol = x.Symbol,
            }).ToList();

            return dtos;
        }

        public async Task<CurrencyDTO> GetCurrencyAsync(int id)
        {
            var currency = await this.currencyRepository.GetCurrencyAsync(id);

            var dto = new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                Name = currency.FullName,
                Number = currency.Number,
                Symbol = currency.Symbol,
            };

            return dto;
        }

        public async Task<CurrencyDTO> GetCurrencyAsync(string code)
        {
            var currency = await this.currencyRepository.GetCurrencyAsync(code);

            var dto = new CurrencyDTO
            {
                Id = currency.Id,
                Code = currency.Code,
                Name = currency.FullName,
                Number = currency.Number,
                Symbol = currency.Symbol,
            };

            return dto;
        }

        public async Task<bool> IsValidAsync(int currencyId)
        {
            var currencies = await this.currencyRepository.GetCurriencesAsync();
            return currencies.Any(x => x.Id == currencyId);
        }

        public async Task<IEnumerable<ExchangeRateDTO>> GetExchangeRatesAsync()
        {
            var rates = (await this.currencyRepository.GetExchangeRatesAsync())
                .Select(x => new ExchangeRateDTO
                {
                    Id = x.Id,
                    Rate = x.Rate,
                    Date = x.Date,
                    FromCurrencyId = x.FromCurrencyId,
                    ToCurrencyId = x.ToCurrencyId,
                    FromCurrencyCode = x.FromCurrency.Code,
                    ToCurrencyCode = x.ToCurrency.Code,
                    CanDelete = x.OwnerId == this.context.Identity.Id,
                })
                .ToList();

            return rates;
        }

        public async Task AddExchangeRateAsync(AddExchangeRateCommand command)
        {
            var rate = ExchangeRate.Create(command.FromCurrencyId, command.ToCurrencyId, command.Rate, context.Identity.Id, command.ExternalId, false, command.Date);

            await this.currencyRepository.AddExchangeRateAsync(rate);
        }

        public async Task<decimal> TransformAmountAsync(decimal amount, int fromCurrencyId, int toCurrencyId)
        {
            var exchangeRate = await this.currencyRepository.GetLatestExchangeRateAsync(fromCurrencyId, toCurrencyId);

            var result = this.GetValueInDifferentCurrency(amount, fromCurrencyId, exchangeRate);

            return result;
        }

        private decimal GetValueInDifferentCurrency(decimal amount, int fromCurrencyId, ExchangeRate exchangeRate)
        {
            var result = amount;
            if (exchangeRate == null)
            {
                result = amount;
            }
            else if (exchangeRate.FromCurrencyId == fromCurrencyId)
            {
                result = amount * (decimal)exchangeRate.Rate;
            }
            else if (exchangeRate.ToCurrencyId == fromCurrencyId)
            {
                result = amount / ((decimal)exchangeRate.Rate != 0 ? (decimal)exchangeRate.Rate : 1);
            }

            return result;
        }

        public async Task<decimal> TransformAmountAsync(decimal amount, string fromCurrencyCode, string toCurrencyCode)
        {
            var fromCurrency = await this.currencyRepository.GetCurrencyAsync(fromCurrencyCode);
            var toCurrency = await this.currencyRepository.GetCurrencyAsync(toCurrencyCode);
            return await this.TransformAmountAsync(amount, fromCurrency.Id, toCurrency.Id);
        }

        public async Task<decimal> GetValueInCurrencyAsync(IList<ExchangeRateDTO> rates, string currentCurrency, string targetCurrency, decimal value, DateTime date)
        {
            if (rates == null)
            {
                rates = (await this.currencyRepository.GetExchangeRatesAsync())
                    .Select(x => new ExchangeRateDTO
                    {
                        ToCurrencyCode = x.ToCurrency.Code,
                        FromCurrencyCode = x.FromCurrency.Code,
                        Date = x.Date,
                        Rate = x.Rate
                    }).ToList();
            }
            if (currentCurrency == targetCurrency)
            {
                return value;
            }

            var exchangeRate = rates.Where(x => x.ToCurrencyCode == currentCurrency || x.FromCurrencyCode == currentCurrency)
                                   .Where(x => x.ToCurrencyCode == targetCurrency || x.FromCurrencyCode == targetCurrency)
                                   .MinBy(x => Math.Abs((x.Date - date).Ticks))
                                    .FirstOrDefault();
            decimal result = 0;

            if (exchangeRate == null)
            {
                result = 0;
            }
            else if (exchangeRate.FromCurrencyCode == currentCurrency)
            {
                result = value * (decimal)exchangeRate.Rate;
            }
            else if (exchangeRate.ToCurrencyCode == currentCurrency)
            {
                result = value / ((decimal)exchangeRate.Rate != 0 ? (decimal)exchangeRate.Rate : 1);
            }

            return result;
        }
    }
}
