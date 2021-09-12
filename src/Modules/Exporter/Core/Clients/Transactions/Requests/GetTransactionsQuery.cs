using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;

namespace BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.Requests
{
    public class GetTransactionsQuery
    {
        public GetTransactionsQuery(TransactionsFilterDTO filters)
        {
            this.Filters = filters;
        }
        
        public TransactionsFilterDTO Filters { get;}
    }
}