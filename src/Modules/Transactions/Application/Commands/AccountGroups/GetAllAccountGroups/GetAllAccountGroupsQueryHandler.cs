using BudgetUnderControl.Common.Contracts;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.AccountGroups.GetAllAccountGroups
{
    internal class GetAllAccountGroupsQueryHandler : IQueryHandler<GetAllAccountGroupsQuery, List<AccountGroupItemDTO>>
    {
        private readonly IAccountGroupService accountGroupService;

        public GetAllAccountGroupsQueryHandler(IAccountGroupService accountGroupService)
        {
            this.accountGroupService = accountGroupService;
        }

        public async Task<List<AccountGroupItemDTO>> Handle(GetAllAccountGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await this.accountGroupService.GetAccountGroupsAsync();
            return groups.ToList();
        }
    }
}