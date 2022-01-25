using BudgetUnderControl.Modules.Transactions.Application.DTO;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Services
{
    public interface ISynchroniser
    {
        Task SynchroniseAsync(SyncRequest syncRequest);
    }
}
