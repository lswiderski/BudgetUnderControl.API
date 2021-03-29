using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts
{
    public class DashboardDTO
    {
        public List<TransactionListItemDTO> Transactions { get; set; }

        public List<CurrencyStatusDTO> ActualStatus { get; set; }

        public List<CategoryShareDTO> ThisMonthCategoryChart { get; set; }

        public List<CategoryShareDTO> LastMonthCategoryChart { get; set; }

        public decimal Total { get; set; }

        public List<SummaryDTO> Incomes { get; set; }

        public List<SummaryDTO> Expenses { get; set; }

        public List<ExpensesColumnChartSeriesDto> ExpensesChart { get; set; }

        public DashboardDTO()
        {
            this.Transactions = new List<TransactionListItemDTO>();
            this.Incomes = new List<SummaryDTO>();
            this.Expenses = new List<SummaryDTO>();
            this.ThisMonthCategoryChart = new List<CategoryShareDTO>();
            this.LastMonthCategoryChart = new List<CategoryShareDTO>();
            this.ActualStatus = new List<CurrencyStatusDTO>();
            this.ExpensesChart = new List<ExpensesColumnChartSeriesDto>();
        }
    }
}
