using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetMovingSum
{
    internal class GetMovingSumQueryHandler : IQueryHandler<GetMovingSumQuery, List<MovingSumItemDTO>>
    {
        private readonly IReportService reportService;

        public GetMovingSumQueryHandler(IReportService reportService)
        {
            this.reportService = reportService;
        }

        public async Task<List<MovingSumItemDTO>> Handle(GetMovingSumQuery request, CancellationToken cancellationToken)
        {
            var movingSum = await this.reportService.MovingSum(request.Filters);
            return movingSum.ToList();
        }
    }
}