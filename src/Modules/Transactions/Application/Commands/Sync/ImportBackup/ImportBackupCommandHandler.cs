using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.ImportBackup
{
    internal class ImportBackupCommandHandler : ICommandHandler<ImportBackupCommand>
    {
        private readonly ISyncService syncService;

        internal ImportBackupCommandHandler(ISyncService syncService)
        {
            this.syncService = syncService;
        }

        public async Task<Unit> Handle(ImportBackupCommand request, CancellationToken cancellationToken)
        {
            await this.syncService.ImportBackUpAsync(request.Backup);

            return Unit.Value;
        }
    }
}