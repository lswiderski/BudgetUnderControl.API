using BudgetUnderControl.Common.Enums;
using BudgetUnderControl.CommonInfrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.CommonInfrastructure
{
    public interface ISyncRequestBuilder
    {
        Task<SyncRequest> CreateSyncRequestAsync(SynchronizationComponent source, SynchronizationComponent target);
    }
}
