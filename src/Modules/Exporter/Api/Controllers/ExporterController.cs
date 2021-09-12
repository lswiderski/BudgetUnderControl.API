using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Application;
using BudgetUnderControl.Modules.Exporter.Application.Commands.Transactions;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetUnderControl.Modules.Exporter.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExporterController: ControllerBase
    {
        private readonly IExporterModule exporterModule;

        public ExporterController(IExporterModule exporterModule)
        {
            this.exporterModule = exporterModule;
        }
        
        [HttpGet("transactions")]
        public async Task<ActionResult> GetTransactionsReport([FromQuery] TransactionsExportRequest request)
        {

            var report = await this.exporterModule.ExecuteQueryAsync(new GetTransactionsQuery(request));

            if (report == null || report.Stream == null)
            {
                return NotFound();
            }
            return File(report.Stream.ToArray(), report.ContentType, report.Name);
        }
        
    }
}