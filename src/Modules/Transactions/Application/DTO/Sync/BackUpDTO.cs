using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Common.Contracts
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
