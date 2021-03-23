using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetDashboard
{
    internal class GetDashboardQueryHandler : IQueryHandler<GetDashboardQuery, DashboardDTO>
    {
        private readonly IReportService reportService;

        public GetDashboardQueryHandler(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public async Task<DashboardDTO> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var dashboard = await this.reportService.GetDashboard();
            return dashboard;
        }
    }
}