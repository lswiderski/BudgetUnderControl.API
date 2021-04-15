
using BudgetUnderControl.Modules.Users.Application.DTO;
using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Users.GetUser
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
