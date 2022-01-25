using System;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class TagToTransactionSyncDTO
    {
        public int Id { get; set; }
        public Guid TagId { get; set; }
        public Guid TransactionId { get; set; }
    }
}
