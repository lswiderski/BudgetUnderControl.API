using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using BudgetUnderControl.Modules.Exporter.Core.DTO;
using BudgetUnderControl.Shared.Abstractions.Enums.Export;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;

namespace BudgetUnderControl.Modules.Exporter.Application.Commands.Transactions
{
    public class GetTransactionsQuery : QueryBase<TransactionsReport>
    {
        public GetTransactionsQuery(TransactionsExportRequest request)
        {
            Filters = request.Filters;
            Type = request.Type;
        }

        public TransactionsFilterDTO Filters { get; }
        
        public ExportFileType Type { get; }
    }
}
