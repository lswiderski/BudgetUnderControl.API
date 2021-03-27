using BudgetUnderControl.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface ICurrencyRepository
    {
        Task<ICollection<Currency>> GetCurriencesAsync();
        Task AddCurrencyAsync(Currency currency);
        Task<Currency> GetCurrencyAsync(int id);
        Task<Currency> GetCurrencyAsync(string code);
        Task AddExchangeRateAsync(ExchangeRate rate);
        Task UpdateExchangeRateAsync(ExchangeRate rate);
        Task<ICollection<ExchangeRate>> GetExchangeRatesAsync();
        Task<ICollection<ExchangeRate>> GetExchangeRatesAsync(int currencyId);
        Task<ExchangeRate> GetExchangeRateAsync(int exchangeRateId);
        Task<ExchangeRate> GetLatestExchangeRateAsync(int fromCurrencyId, int toCurrencyId);
    }
}
