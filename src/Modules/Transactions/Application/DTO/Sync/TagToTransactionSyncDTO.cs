using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetUnderControl.Common.Contracts
{
    public class TagToTransactionSyncDTO
    {
        public int Id { get; set; }
        public Guid TagId { get; set; }
        public Guid TransactionId { get; set; }
    }
}
