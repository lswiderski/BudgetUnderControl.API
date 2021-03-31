using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.Sync
{
    public class SyncCommand : CommandBase<SyncRequest>
    {
        public SyncCommand(SyncRequest request)
        {
            Request = request;
        }

        public SyncRequest Request { get; }
    }
}