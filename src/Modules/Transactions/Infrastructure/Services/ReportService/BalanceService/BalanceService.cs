using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Domain.Repositiories;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.DTO.Report.Balance;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Core.ValueObjects;
using static MoreLinq.Extensions.MinByExtension;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services.BalanceService
{
   public class BalanceService : IBalanceService
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ICurrencyRepository currencyRepository;
        private readonly IAccountService accountService;
        private readonly ITransactionService transactionService;
        private readonly ICurrencyService currencyService;

        public BalanceService(ITransactionRepository transactionRepository, ICurrencyRepository currencyRepository,
            IAccountService accountService, ITransactionService transactionService, ICurrencyService currencyService)
        {
            this.transactionRepository = transactionRepository;
            this.currencyRepository = currencyRepository;
            this.accountService = accountService;
            this.transactionService = transactionService;
            this.currencyService = currencyService;
        }

        public async Task<BalanceResultDto> GetBalanceAsync(ICollection<TransactionListItemDTO> transactions)
        {
            return await this.GetBalanceAsync(transactions, "PLN");
        }

        public async Task<BalanceResultDto> GetBalanceAsync(ICollection<TransactionListItemDTO> transactions, string mainCurrency)
        {
            var result = new BalanceResultDto();
           var exchangeRates = (await this.currencyRepository.GetExchangeRatesAsync())
                .Select(x => new ExchangeRateDTO
                {
                    ToCurrencyCode = x.ToCurrency.Code,
                    FromCurrencyCode = x.FromCurrency.Code,
                    Date = x.Date,
                    Rate = x.Rate
                }).ToList();

            result.Expenses = this.GetExpenseOrIncome(transactions, false).Select(x => new BalanceDto { Currency = x.Key, Value = x.Value, IsExchanged = false }).ToList();
            result.Incomes = this.GetExpenseOrIncome(transactions, false, true).Select(x => new BalanceDto { Currency = x.Key, Value = x.Value, IsExchanged = false }).ToList();

            var totalExpenses = await this.GetTotalExpenseOrIncomeAsync(transactions, mainCurrency, exchangeRates, false);
            var totalIncomes = await this.GetTotalExpenseOrIncomeAsync(transactions, mainCurrency, exchangeRates, false, true);

            result.Expenses.Add(new BalanceDto { Currency = mainCurrency, Value = totalExpenses, IsExchanged = true });
            result.Incomes.Add(new BalanceDto { Currency = mainCurrency, Value = totalIncomes, IsExchanged = true });
            return result;
        }

        public async Task<BalanceResultDto> GetBalanceAsync(TransactionsFilterDTO filter, string mainCurrency)
        {
            var transactions = await this.transactionService.GetTransactionsAsync(filter);

            var result = await this.GetBalanceAsync(transactions, mainCurrency);
            return result;
        }

        public async Task<BalanceResultDto> GetTotalCurrentBalanceAsync()
        {
            return await this.GetTotalCurrentBalanceAsync("PLN");
        }

        public async Task<BalanceResultDto> GetTotalCurrentBalanceAsync(string mainCurrency)
        {
            var accounts = await accountService.GetAccountsWithBalanceAsync(false);
            var exchangeRates = (await this.currencyRepository.GetExchangeRatesAsync())
                .Select(x => new ExchangeRateDTO
            {
                ToCurrencyCode = x.ToCurrency.Code,
                FromCurrencyCode = x.FromCurrency.Code,
                Date = x.Date,
                Rate = x.Rate
            }).ToList();

            var currentStatus = this.CalculateActualStatus(accounts);
            var total = await this.CalculateTotalSumAsync(currentStatus, exchangeRates, mainCurrency);

            var result = new BalanceResultDto
            {
                Total = currentStatus
            };

            result.Total.Add(new BalanceDto { Currency = mainCurrency, IsExchanged = true, Value = total });

            return result;
        }

        private Dictionary<string, decimal> GetExpenseOrIncome(ICollection<TransactionListItemDTO> transactions, bool includeTransfers, bool isIncome = false)
        {
            var selectedTransactions = transactions
                .Where(x => !x.IsTransfer.Value);
            Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
            selectedTransactions.ToList().ForEach(x =>
            {
                if (isIncome ? x.Value > 0 : x.Value < 0)
                {
                    if (!dict.ContainsKey(x.CurrencyCode))
                    {
                        dict.Add(x.CurrencyCode, x.Value);
                    }
                    else
                    {
                        dict[x.CurrencyCode] += x.Value;
                    }
                    
                }

            });

            return dict;
        }

        private async Task<decimal> GetTotalExpenseOrIncomeAsync(ICollection<TransactionListItemDTO> transactions, string currencyCode, List<ExchangeRateDTO> exchangeRates, bool includeTransfers, bool isIncome = false)
        {
            var selectedTransactions = transactions
                .Where(x => !x.IsTransfer.Value);

            decimal sum = 0;
            selectedTransactions.ToList().ForEach(async x => 
            {
                if (isIncome ? x.Value > 0 : x.Value < 0)
                {
                    sum += await this.currencyService.GetValueInCurrencyAsync(exchangeRates, x.CurrencyCode, currencyCode, x.Value, x.JustDate);
                }

            });

            return sum;
        }

        private List<BalanceDto> CalculateActualStatus(ICollection<AccountListItemDTO> accounts)
        {
            Dictionary<string, decimal> dict = new Dictionary<string, decimal>();
            foreach (var account in accounts)
            {
                if (!account.ParentAccountId.HasValue)
                {
                    if (!dict.ContainsKey(account.Currency))
                    {
                        dict.Add(account.Currency, account.Balance);
                    }
                    else
                    {
                        dict[account.Currency] += account.Balance;
                    }
                }

            }

            return dict.Select(x => new BalanceDto { Value = x.Value, Currency = x.Key }).ToList();
        }

        private async Task<decimal> CalculateTotalSumAsync(List<BalanceDto> currentStatus, List<ExchangeRateDTO> exchangeRates, string userMainCurrency)
        {
            decimal sum = 0;

            foreach (var currency in currentStatus)
            {
                sum += await this.currencyService.GetValueInCurrencyAsync(exchangeRates, currency.Currency, userMainCurrency, currency.Value, DateTime.Now);
            }
            return sum;
        }

    }
}
