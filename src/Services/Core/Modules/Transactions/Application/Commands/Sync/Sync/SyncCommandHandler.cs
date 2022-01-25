using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.Sync
{
    internal class SyncCommandHandler : ICommandHandler<SyncCommand, SyncRequest>
    {
        private readonly ISyncService syncService;

        internal SyncCommandHandler(ISyncService syncService)
        {
            this.syncService = syncService;
        }

        public async Task<SyncRequest> Handle(SyncCommand request, CancellationToken cancellationToken)
        {
            var response = await this.syncService.SyncAsync(request.Request);

            return response;
        }
    }
}