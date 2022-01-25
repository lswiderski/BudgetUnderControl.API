using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface IReportService
    {
        Task<ICollection<MovingSumItemDTO>> MovingSum(TransactionsFilterDTO filter = null);

        Task<List<ExpensesColumnChartSeriesDto>> GetExpensesChartDataAsync(TransactionsFilterDTO filter);

        Task<DashboardDTO> GetDashboard();
    }
}
