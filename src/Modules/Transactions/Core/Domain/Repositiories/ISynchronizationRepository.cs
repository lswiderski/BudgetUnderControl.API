using BudgetUnderControl.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Domain.Repositiories
{
    public interface ISynchronizationRepository
    {
        Task<IEnumerable<Synchronization>> GetSynchronizationsAsync();
        Task AddSynchronizationAsync(Synchronization synchronization);
        Task<Synchronization> GetSynchronizationAsync(SynchronizationComponent component, Guid componentId, int userId);
        Task UpdateAsync(Synchronization synchronization);
        Task ClearSynchronizationAsync();
    }
}
