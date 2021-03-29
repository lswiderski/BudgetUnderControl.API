using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.CommonInfrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface ISyncService
    {
        Task ImportBackUpAsync(BackUpDTO backupDto);
        Task CleanDataBaseAsync();
        Task<BackUpDTO> GetBackUpAsync();
        Task<IEnumerable<string>> GenerateCSV();

        Task<SyncRequest> SyncAsync(SyncRequest request);
    }
}
