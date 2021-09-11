using System.Collections.Generic;

namespace BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO
{
    public class TransactionListDataSource
    {
        public ICollection<TransactionDTO> Transactions { get; set; }

    }
}