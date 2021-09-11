using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using BudgetUnderControl.Modules.Exporter.Core.DTO;
using BudgetUnderControl.Modules.Exporter.Core.Services;

namespace BudgetUnderControl.Modules.Exporter.Targets.Excel.Builders.Transactions
{
    public class TransactionsExcelCreator :  ITransacationsReportCreator
    {
        public async Task<TransactionsReport> CreateReportAsync(ICollection<TransactionDTO> transactions)
        {
            await Task.CompletedTask;
            return new TransactionsReport
            {
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                Name = "Report.xlsx",
            };
        }
    }
}