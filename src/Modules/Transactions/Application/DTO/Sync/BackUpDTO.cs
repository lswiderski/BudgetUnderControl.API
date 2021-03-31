using System.Collections.Generic;

namespace BudgetUnderControl.Modules.Transactions.Application.DTO
{
    public class BackUpDTO
    {
        public List<CurrencySyncDTO> Currencies { get; set; }
        public List<AccountSyncDTO> Accounts { get; set; }
        public List<TransactionSyncDTO> Transactions { get; set; }
        public List<TransferSyncDTO> Transfers { get; set; }
        public List<TagSyncDTO> Tags { get; set; }
        public List<TagToTransactionSyncDTO> TagsToTransactions { get; set; }
        public List<ExchangeRateSyncDTO> ExchangeRates { get; set; } 

    }
}
