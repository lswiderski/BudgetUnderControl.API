using BudgetUnderControl.Common.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface IReportService
    {
        Task<ICollection<MovingSumItemDTO>> MovingSum(TransactionsFilterDTO filter = null);

        Task<List<ExpensesColumnChartSeriesDto>> GetExpensesChartDataAsync(TransactionsFilterDTO filter);

        Task<DashboardDTO> GetDashboard();
    }
}
