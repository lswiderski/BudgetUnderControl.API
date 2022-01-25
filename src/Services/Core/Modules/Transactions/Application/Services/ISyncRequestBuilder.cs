using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface ISyncRequestBuilder
    {
        Task<SyncRequest> CreateSyncRequestAsync(SynchronizationComponent source, SynchronizationComponent target);
    }
}
