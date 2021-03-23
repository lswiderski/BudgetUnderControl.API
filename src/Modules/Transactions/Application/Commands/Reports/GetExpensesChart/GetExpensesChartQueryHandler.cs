using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetExpensesChart
{
    internal class GetExpensesChartQueryHandler : IQueryHandler<GetExpensesChartQuery, List<ExpensesColumnChartSeriesDto>>
    {
        private readonly IReportService reportService;

        public GetExpensesChartQueryHandler(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public async Task<List<ExpensesColumnChartSeriesDto>> Handle(GetExpensesChartQuery request, CancellationToken cancellationToken)
        {
            var chartsDatasource = await this.reportService.GetExpensesChartDataAsync(request.Filters);
            return chartsDatasource.ToList();
        }
    }
}