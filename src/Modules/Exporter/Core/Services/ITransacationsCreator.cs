using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using BudgetUnderControl.Modules.Exporter.Core.DTO;

namespace BudgetUnderControl.Modules.Exporter.Core.Services
{
    public interface ITransacationsReportCreator
    {
        Task<TransactionsReport> CreateReportAsync(ICollection<TransactionDTO> transactions);
    }
}