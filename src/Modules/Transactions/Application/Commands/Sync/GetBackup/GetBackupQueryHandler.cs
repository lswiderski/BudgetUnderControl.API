using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.GetBackup
{
    internal class GetBackupQueryHandler : IQueryHandler<GetBackupQuery, BackUpDTO>
    {
        private readonly ISyncService syncService;

        public GetBackupQueryHandler(ISyncService syncService)
        {
            this.syncService = syncService;
        }
        public async Task<BackUpDTO> Handle(GetBackupQuery request, CancellationToken cancellationToken)
        {
            var backup = await this.syncService.GetBackUpAsync();
            return backup;
        }
    }
}