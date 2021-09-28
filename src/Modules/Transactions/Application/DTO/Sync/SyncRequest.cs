using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class SyncRequest 
    {
        public DateTime LastSync { get; set; }

        public Guid UserId { get; set; }
        public SynchronizationComponent Component { get; set; }
        public Guid ComponentId { get; set; }

       //add collections TransactionsToUpdate / TransfersToUpdate / CategoriesToUpdate etc
        public IEnumerable<TransactionSyncDTO> Transactions { get; set; }
        public IEnumerable<TransferSyncDTO> Transfers { get; set; }
        public IEnumerable<AccountSyncDTO> Accounts { get; set; }
        public IEnumerable<UserSyncDTO> Users{ get; set; }
        public IEnumerable<CategorySyncDTO> Categories { get; set; }
        public IEnumerable<TagSyncDTO> Tags { get; set; }
        public IEnumerable<ExchangeRateSyncDTO> ExchangeRates { get; set; }
        public IEnumerable<FileSyncDTO> Files { get; set; }
    }
}
