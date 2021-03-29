using BudgetUnderControl.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.ApiInfrastructure.Services
{
    public interface IExpensesReportService
    {
        Task<List<ExpensesColumnChartSeriesDto>> GetExpensesChartDataAsync(TransactionsFilterDTO filter);
    }
}
