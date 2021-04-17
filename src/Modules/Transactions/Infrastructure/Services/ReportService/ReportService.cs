using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Domain;
using BudgetUnderControl.Domain.Repositiories;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MoreLinq.Extensions.MinByExtension;
using BudgetUnderControl.Modules.Transactions.Application.Services;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly ITransactionService transactionService;
        private readonly ICurrencyRepository currencyRepository;
        private readonly IAccountService accountService;
        private readonly IExpensesReportService expensesReportService;

        public ReportService(ITransactionService transactionService 
            ,ICurrencyRepository currencyRepository
            , IAccountService accountService
            , IExpensesReportService expensesReportService)
        {
            this.transactionService = transactionService;
            this.currencyRepository = currencyRepository;
            this.accountService = accountService;
            this.expensesReportService = expensesReportService;
        }

        public async Task<ICollection<MovingSumItemDTO>> MovingSum(TransactionsFilterDTO filter = null)
        {
            var transactions = await this.transactionService.GetTransactionsAsync(filter);
           
            var movingSumCollection = new List<MovingSumItemDTO>();
            decimal sum = 0;
            var recalculateCurrencies = false;
            var selectedCurrencyCode = "PLN";
            List<ExchangeRate> exchangeRates = null;

            if (recalculateCurrencies)
            {
                 exchangeRates = (await this.currencyRepository.GetExchangeRatesAsync()).ToList();
            }

            ((transactions.OrderBy(x => x.Date))).ForEach(x =>
            {
                var diff = 0m;
                if (recalculateCurrencies)
                {
                   diff = (x.CurrencyCode == selectedCurrencyCode ? x.Value : this.GetValueInCurrency(exchangeRates, x.CurrencyCode, selectedCurrencyCode, x.Value, x.JustDate));
                }
                else {
                    diff = (x.CurrencyCode == selectedCurrencyCode ? x.Value : 0m);
                }
                sum += diff;
                var movingSum = new MovingSumItemDTO
                {
                    Date = x.JustDate,
                    Currency = x.CurrencyCode,
                    Value = sum,
                    Diff = diff,
                };
                movingSumCollection.Add(movingSum);
            });

            return movingSumCollection;
        }

        public async Task<DashboardDTO> GetDashboard()
        {
            var dashboard = new DashboardDTO();
            string userMainCurrency = "PLN";
            var now = DateTime.UtcNow;
            var previousMonth = now.AddMonths(-1);
            var recalculateCurrencies = true;
            List<ExchangeRate> exchangeRates = null;

            var transactions = await this.transactionService.GetTransactionsAsync(new TransactionsFilterDTO { FromDate = now.AddMonths(-6),  IncludeTransfers = false });
            var accounts = await accountService.GetAccountsWithBalanceAsync();

            if (recalculateCurrencies)
            {
                exchangeRates = (await this.currencyRepository.GetExchangeRatesAsync()).ToList();
            }

            var thisMonthStartDate = new DateTime(now.Year, now.Month, 1);
            var thisMonthEndDate = thisMonthStartDate.AddMonths(1);
            for (int i = 4; i >= 0; i--)
            {
                var date = new DateTime(now.Year, now.Month, 1).AddMonths(-i);
                var firstDayInNextMonth = date.AddMonths(1);
                dashboard.Incomes.Add(new SummaryDTO { From = date, To = firstDayInNextMonth, Value = this.GetTotalExpsenseOrIncome(date, firstDayInNextMonth, transactions, userMainCurrency, exchangeRates, true) });
                dashboard.Expenses.Add(new SummaryDTO { From = date, To = firstDayInNextMonth, Value = this.GetTotalExpsenseOrIncome(date, firstDayInNextMonth, transactions, userMainCurrency, exchangeRates) });
            }

            dashboard.Transactions = transactions.ToList();
            dashboard.ThisMonthCategoryChart = this.GetCategoriesShareInThePeriodOfTime(thisMonthStartDate, thisMonthEndDate, transactions, userMainCurrency, exchangeRates);
            dashboard.LastMonthCategoryChart = this.GetCategoriesShareInThePeriodOfTime(new DateTime(previousMonth.Year, previousMonth.Month, 1), new DateTime(previousMonth.Year, previousMonth.Month,DateTime.DaysInMonth(previousMonth.Year, previousMonth.Month)), transactions, userMainCurrency, exchangeRates);
            dashboard.ActualStatus = this.CalculateActualStatus(accounts);
            dashboard.Total = this.CalculateTotalSum(dashboard.ActualStatus, exchangeRates, userMainCurrency);

            dashboard.ExpensesChart = await this.GetExpensesChartDataAsync(new TransactionsFilterDTO { FromDate = now.AddDays(-3) });

            return dashboard;
        }

        public async Task<List<ExpensesColumnChartSeriesDto>> GetExpensesChartDataAsync(TransactionsFilterDTO filter)
        {
            var report = await this.expensesReportService.GetExpensesChartDataAsync(filter);
            return report;
        }

        private decimal CalculateTotalSum(List<CurrencyStatusDTO> actualStatus, List<ExchangeRate> exchangeRates, string userMainCurrency)
        {
            decimal sum = 0;

            foreach (var currency in actualStatus)
            {
                sum +=  this.GetValueInCurrency(exchangeRates, currency.Currency, userMainCurrency, currency.Balance, DateTime.Now);
            }
            return sum;
        }

        private List<CurrencyStatusDTO> CalculateActualStatus(ICollection<AccountListItemDTO> accounts)
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

            return dict.Select(x => new CurrencyStatusDTO { Balance = x.Value, Currency = x.Key }).ToList() ;
        }

        private decimal GetTotalExpsenseOrIncome(DateTime from, DateTime to, ICollection<TransactionListItemDTO> transactions, string currencyCode, List<ExchangeRate> exchangeRates, bool isIncome = false)
        {
            var selectedTransactions = transactions
                .Where(x => x.JustDate >= from && x.JustDate <= to
                     && !x.IsTransfer.Value);

            decimal sum = 0;
            selectedTransactions.ForEach(x =>
            {
                if (isIncome ? x.Value > 0 : x.Value < 0)
                {
                    sum += this.GetValueInCurrency(exchangeRates, x.CurrencyCode, currencyCode, x.Value, x.JustDate);
                }

            });

            return sum;
        }

        private List<CategoryShareDTO> GetCategoriesShareInThePeriodOfTime(DateTime from, DateTime to,  ICollection<TransactionListItemDTO> transactions, string currencyCode, List<ExchangeRate> exchangeRates, bool isIncome = false)
        {
            var result = transactions
                .Where(x => x.JustDate >= from && x.JustDate <= to 
                && x.CurrencyCode == currencyCode
                && !x.IsTransfer.Value
                && (( isIncome && x.Value > 0 ) || (!isIncome && x.Value < 0)))
                .GroupBy(x => x.Category)
                .Select(x => new CategoryShareDTO
                {
                    Category = x.Key,
                    Value = x.Sum(y => this.GetValueInCurrency(exchangeRates, y.CurrencyCode, currencyCode, y.Value, y.JustDate) )
                }).ToList();

            return result;
        }

        private decimal GetValueInCurrency(IList<ExchangeRate> rates, string currentCurrency, string targetCurrency, decimal value, DateTime date)
        {
            if (currentCurrency == targetCurrency)
            {
                return value;
            }

            var exchangeRate = rates.Where(x => x.ToCurrency.Code == currentCurrency || x.FromCurrency.Code == currentCurrency)
                                   .Where(x => x.ToCurrency.Code == targetCurrency || x.FromCurrency.Code == targetCurrency)
                                   .MinBy(x => Math.Abs((x.Date - date).Ticks))
                                    .FirstOrDefault();
            decimal result = 0;

            if (exchangeRate == null)
            {
                result = 0;
            }
            else if (exchangeRate.FromCurrency.Code == currentCurrency)
            {
                result = value * (decimal)exchangeRate.Rate;
            }
            else if (exchangeRate.ToCurrency.Code == currentCurrency)
            {
                result = value / ((decimal)exchangeRate.Rate != 0 ? (decimal)exchangeRate.Rate : 1);
            }

            return result;
        }

    }
}
