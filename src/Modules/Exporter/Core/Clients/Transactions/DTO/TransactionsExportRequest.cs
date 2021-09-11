using BudgetUnderControl.Shared.Abstractions.Enums.Export;

namespace BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO
{
    public class TransactionsExportRequest
    {
        public TransactionsFilterDTO Filters { get; set; }

        public ExportFileType Type { get; set; }
    }
}