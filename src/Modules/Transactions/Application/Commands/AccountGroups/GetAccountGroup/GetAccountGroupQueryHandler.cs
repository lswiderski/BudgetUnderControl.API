using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Transactions.Application.Services;

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