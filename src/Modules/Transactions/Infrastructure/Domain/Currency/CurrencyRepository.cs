using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Infrastructure.Services;
using BudgetUnderControl.Shared.Abstractions.Contexts;

namespace BudgetUnderControl.Infrastructure
{

    public class CurrencyRepository :  ICurrencyRepository
    {
        private readonly IContext context;
        private readonly TransactionsContext transactionsContext;

        public CurrencyRepository(TransactionsContext transactionsContext, IContext context) 
        {
            this.context= context;
            this.transactionsContext = transactionsContext;
        }

        public async Task AddCurrencyAsync(Currency currency)
        {
            this.transactionsContext.Currencies.Add(currency);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task<ICollection<Currency>> GetCurriencesAsync()
        {
            var list = await this.transactionsContext.Currencies.ToListAsync();

            return list;
        }

        public async Task<Currency> GetCurrencyAsync(int id)
        {
            var currency = await this.transactionsContext.Currencies.FirstOrDefaultAsync(x => x.Id == id);

            return currency;
        }

        public async Task<Currency> GetCurrencyAsync(string code)
        {
            var currency = await this.transactionsContext.Currencies.FirstOrDefaultAsync(x => x.Code == code);

            return currency;
        }

        public async Task AddExchangeRateAsync(ExchangeRate rate)
        {
            this.transactionsContext.ExchangeRates.Add(rate);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task UpdateExchangeRateAsync(ExchangeRate rate)
        {
            this.transactionsContext.ExchangeRates.Update(rate);
            await this.transactionsContext.SaveChangesAsync();
        }

        public async Task<ICollection<ExchangeRate>> GetExchangeRatesAsync()
        {
            var collection = await this.transactionsContext.ExchangeRates
                .Where(x => x.OwnerId == null || x.OwnerId == context.Identity.Id)
                .Include(x => x.ToCurrency)
                .Include(x => x.FromCurrency)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
            return collection;
        }

        public async Task<ICollection<ExchangeRate>> GetExchangeRatesAsync(int currencyId)
        {
            var collection = await this.transactionsContext.ExchangeRates
                .Where(x => x.OwnerId == null || x.OwnerId == context.Identity.Id)
                .Where(x => x.ToCurrencyId == currencyId
           || x.FromCurrencyId == currencyId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
            return collection;
        }

        public async Task<ExchangeRate> GetExchangeRateAsync(int exchangeRateId)
        {
            var rate = await this.transactionsContext.ExchangeRates.Where(x => x.Id == exchangeRateId && (x.OwnerId == null || x.OwnerId == context.Identity.Id)).FirstOrDefaultAsync();
            return rate;
        }

        public async Task<ExchangeRate> GetLatestExchangeRateAsync(int fromCurrencyId, int toCurrencyId)
        {
            var rate = await this.transactionsContext.ExchangeRates
                .Where(x => x.ToCurrencyId == toCurrencyId
            && x.FromCurrencyId == fromCurrencyId
            && (x.OwnerId == null || x.OwnerId == context.Identity.Id))
            .OrderByDescending(x => x.Date)
            .FirstOrDefaultAsync();

            if (rate == null)
            {
                rate = await this.transactionsContext.ExchangeRates
                .Where(x => x.ToCurrencyId == fromCurrencyId
                 && x.FromCurrencyId == toCurrencyId
                 && (x.OwnerId == null || x.OwnerId == context.Identity.Id))
                .OrderByDescending(x => x.Date)
                .FirstOrDefaultAsync();
            }

            return rate;
        }
    }
}
