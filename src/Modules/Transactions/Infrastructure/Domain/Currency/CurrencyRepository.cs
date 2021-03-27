using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Infrastructure
{

    public class CurrencyRepository : BaseModel, ICurrencyRepository
    {
        private readonly IUserIdentityContext userIdentityContext;

        public CurrencyRepository(IContextFacade context,  IUserIdentityContext userIdentityContext) : base(context)
        {
            this.userIdentityContext = userIdentityContext;
        }

        public async Task AddCurrencyAsync(Currency currency)
        {
            this.Context.Currencies.Add(currency);
            await this.Context.SaveChangesAsync();
        }

        public async Task<ICollection<Currency>> GetCurriencesAsync()
        {
            var list = await this.Context.Currencies.ToListAsync();

            return list;
        }

        public async Task<Currency> GetCurrencyAsync(int id)
        {
            var currency = await this.Context.Currencies.FirstOrDefaultAsync(x => x.Id == id);

            return currency;
        }

        public async Task<Currency> GetCurrencyAsync(string code)
        {
            var currency = await this.Context.Currencies.FirstOrDefaultAsync(x => x.Code == code);

            return currency;
        }

        public async Task AddExchangeRateAsync(ExchangeRate rate)
        {
            this.Context.ExchangeRates.Add(rate);
            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateExchangeRateAsync(ExchangeRate rate)
        {
            this.Context.ExchangeRates.Update(rate);
            await this.Context.SaveChangesAsync();
        }

        public async Task<ICollection<ExchangeRate>> GetExchangeRatesAsync()
        {
            var collection = await this.Context.ExchangeRates
                .Where(x => x.UserId == null || x.UserId == userIdentityContext.UserId)
                .Include(x => x.ToCurrency)
                .Include(x => x.FromCurrency)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
            return collection;
        }

        public async Task<ICollection<ExchangeRate>> GetExchangeRatesAsync(int currencyId)
        {
            var collection = await this.Context.ExchangeRates
                .Where(x => x.UserId == null || x.UserId == userIdentityContext.UserId)
                .Where(x => x.ToCurrencyId == currencyId
           || x.FromCurrencyId == currencyId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
            return collection;
        }

        public async Task<ExchangeRate> GetExchangeRateAsync(int exchangeRateId)
        {
            var rate = await this.Context.ExchangeRates.Where(x => x.Id == exchangeRateId && (x.UserId == null || x.UserId == userIdentityContext.UserId)).FirstOrDefaultAsync();
            return rate;
        }

        public async Task<ExchangeRate> GetLatestExchangeRateAsync(int fromCurrencyId, int toCurrencyId)
        {
            var rate = await this.Context.ExchangeRates
                .Where(x => x.ToCurrencyId == toCurrencyId
            && x.FromCurrencyId == fromCurrencyId
            && (x.UserId == null || x.UserId == userIdentityContext.UserId))
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync();

            if (rate == null)
            {
                rate = await this.Context.ExchangeRates
                .Where(x => x.ToCurrencyId == fromCurrencyId
                 && x.FromCurrencyId == toCurrencyId
                 && (x.UserId == null || x.UserId == userIdentityContext.UserId))
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync();
            }

            return rate;
        }
    }
}
