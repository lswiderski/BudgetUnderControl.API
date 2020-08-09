using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.CommonInfrastructure.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetUnderControl.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ReportsController : ApiControllerBase
    {
        private readonly IReportService reportService;
        public ReportsController(IReportService reportService, ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            this.reportService = reportService;
        }

        [HttpGet("movingsum")]
        public async Task<IActionResult> MovingSum([FromQuery] TransactionsFilter filter)
        {
            var days = await this.reportService.MovingSum(filter);
            return Ok(days.ToList());
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var dashboard = await this.reportService.GetDashboard();
            return Ok(dashboard);
        }

        [HttpGet("expenseschart")]
        public async Task<IActionResult> ExpensesChart([FromQuery] TransactionsFilter filter)
        {
            var days = await this.reportService.GetExpensesChartDataAsync(filter);
            return Ok(days.ToList());
        }
    }
}
