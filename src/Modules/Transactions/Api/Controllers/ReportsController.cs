using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetDashboard;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetExpensesChart;
using BudgetUnderControl.Modules.Transactions.Application.Commands.Reports.GetMovingSum;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ITransactionsModule _transactionsModule;

        public ReportsController(ITransactionsModule transactionsModule)
        {
            _transactionsModule = transactionsModule;
        }

        [HttpGet("movingsum")]
        [ProducesResponseType(typeof(List<MovingSumItemDTO>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> MovingSum([FromQuery] TransactionsFilterDTO filter)
        {
            var days = await this._transactionsModule.ExecuteQueryAsync(new GetMovingSumQuery(filter));
            return Ok(days);
        }

        [HttpGet("dashboard")]
        [ProducesResponseType(typeof(DashboardDTO), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Dashboard()
        {

            var dashboard = await this._transactionsModule.ExecuteQueryAsync(new GetDashboardQuery());
            return Ok(dashboard);
        }

        [HttpGet("expenseschart")]
        [ProducesResponseType(typeof(List<ExpensesColumnChartSeriesDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ExpensesChart([FromQuery] TransactionsFilterDTO filter)
        {
            var days = await this._transactionsModule.ExecuteQueryAsync(new GetExpensesChartQuery(filter));
            return Ok(days);
        }
    }
}
