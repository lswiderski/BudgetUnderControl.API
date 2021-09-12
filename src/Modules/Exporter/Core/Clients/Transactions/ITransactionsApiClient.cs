using System.Collections.Generic;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.Requests;

namespace BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions
{
    public interface ITransactionsApiClient
    {
        Task<TransactionListDataSource> GetTransactionsAsync(GetTransactionsQuery query);
    }
}