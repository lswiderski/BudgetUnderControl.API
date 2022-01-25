using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.Services
{
    public interface IExpensesReportService
    {
        Task<List<ExpensesColumnChartSeriesDto>> GetExpensesChartDataAsync(TransactionsFilterDTO filter);
    }
}
