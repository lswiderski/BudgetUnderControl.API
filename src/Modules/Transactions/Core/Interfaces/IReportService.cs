using BudgetUnderControl.Common.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface IReportService
    {
        Task<ICollection<MovingSumItemDTO>> MovingSum(TransactionsFilter filter = null);

        Task<List<ExpensesColumnChartSeriesDto>> GetExpensesChartDataAsync(TransactionsFilter filter);

        Task<DashboardDTO> GetDashboard();
    }
}
