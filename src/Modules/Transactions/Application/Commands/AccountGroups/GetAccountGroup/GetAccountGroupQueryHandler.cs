using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.AccountGroups.GetAccountGroup
{
    internal class GetAccountGroupQueryHandler : IQueryHandler<GetAccountGroupQuery, AccountGroupItemDTO>
    {
        private readonly IAccountGroupService accountGroupService;

        public GetAccountGroupQueryHandler(IAccountGroupService accountGroupService)
        {
            this.accountGroupService = accountGroupService;
        }

        public async Task<AccountGroupItemDTO> Handle(GetAccountGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await this.accountGroupService.GetAccountGroupAsync(request.AccountGroupId);
            return group;
        }
    }
}