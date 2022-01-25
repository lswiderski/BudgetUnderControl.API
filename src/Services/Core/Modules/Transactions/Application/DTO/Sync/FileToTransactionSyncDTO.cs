using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class FileToTransactionSyncDTO
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public int TransactionId { get; set; }

        public DateTime? ModifiedOn { get; set; }
        public Guid ExternalId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
