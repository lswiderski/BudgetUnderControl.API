using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Sync.GetCSV
{
    internal class GetCSVQueryHandler : IQueryHandler<GetCSVQuery, IEnumerable<string>>
    {
        private readonly ISyncService syncService;

        public GetCSVQueryHandler(ISyncService syncService)
        {
            this.syncService = syncService;
        }
        public async Task<IEnumerable<string>> Handle(GetCSVQuery request, CancellationToken cancellationToken)
        {
            var csv = await this.syncService.GenerateCSV();
            return csv;
        }
    }
}