using BudgetUnderControl.Modules.Users.Application.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Users.Application.Commands.Login.AuthenticateAdmin
{
    internal class AuthenticateAdminCommandHandler : ICommandHandler<AuthenticateAdminCommand, string>
    {
        private readonly IUserService userService;

        internal AuthenticateAdminCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<string> Handle(AuthenticateAdminCommand request, CancellationToken cancellationToken)
        {
            var userId = await this.userService.ValidateLoginAsync(request.Username, request.Password);
            if (userId is { })
            {
                var isAdmin = this.userService.CreateUserIdentityContext(userId.Value.ToString()).IsAdmin;

                if (!isAdmin) return null;

                var token = await this.userService.CreateAccessTokenAsync(userId.Value);
                return token;
            }
            else
            {
                return null;
            }
        }
    }
}
