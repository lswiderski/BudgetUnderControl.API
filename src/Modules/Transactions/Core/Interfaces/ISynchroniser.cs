using BudgetUnderControl.CommonInfrastructure.Commands;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface ISynchroniser
    {
        Task SynchroniseAsync(SyncRequest syncRequest);
    }
}
