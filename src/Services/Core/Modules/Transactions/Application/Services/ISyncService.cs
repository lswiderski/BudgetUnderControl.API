using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface ISyncService
    {
        Task ImportBackUpAsync(BackUpDTO backupDto);
        Task CleanDataBaseAsync();
        Task<BackUpDTO> GetBackUpAsync();
        Task<SyncRequest> SyncAsync(SyncRequest request);
    }
}
