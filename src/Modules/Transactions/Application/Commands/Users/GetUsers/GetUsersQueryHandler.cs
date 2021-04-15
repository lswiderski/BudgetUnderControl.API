using BudgetUnderControl.Modules.Transactions.Application.DTO;
using BudgetUnderControl.Modules.Transactions.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.GetUsers
{
    internal class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserListItemDTO>>
    {
        private readonly IUserAdminService userAdminService;

        internal GetUsersQueryHandler(IUserAdminService userAdminService)
        {
            this.userAdminService = userAdminService;
        }
        public async Task<IEnumerable<UserListItemDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await this.userAdminService.GetUsersAsync();
            return users;
        }
    }
}