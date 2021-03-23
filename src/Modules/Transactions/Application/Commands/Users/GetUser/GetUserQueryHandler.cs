using BudgetUnderControl.Common.Contracts.User;
using BudgetUnderControl.CommonInfrastructure;
using BudgetUnderControl.CommonInfrastructure.Interfaces;
using BudgetUnderControl.Modules.Transactions.Application.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Transactions.Application.Commands.Users.GetUser
{
    internal class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDTO>
    {
        private readonly IUserAdminService userAdminService;

        internal GetUserQueryHandler(IUserAdminService userAdminService)
        {
            this.userAdminService = userAdminService;
        }
        public async Task<UserDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await this.userAdminService.GetUserAsync(request.UserId);
            return user;
        }
    }
}
