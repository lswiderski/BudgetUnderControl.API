using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;

namespace BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions
{
    public interface ITransactionsApiClient
    {
        Task<TransactionListDataSource> GetTransactionsAsync(TransactionsFilterDTO filters);
    }
}